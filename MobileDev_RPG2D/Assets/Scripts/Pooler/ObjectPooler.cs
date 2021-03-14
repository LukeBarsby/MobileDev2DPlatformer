using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : Singleton<ObjectPooler>
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }
    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionry;

    void Start()
    {
        poolDictionry = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionry.Add(pool.tag, objectPool);
        }
    }
    public GameObject SpawnFromPool(string tag, Vector3 pos, Quaternion rot)
    {
        if (!poolDictionry.ContainsKey(tag))
        {
            Debug.LogWarning("pool with tag " + tag + "doesn't exist");
            return null;
        }

        GameObject spawnObj =  poolDictionry[tag].Dequeue();

        spawnObj.SetActive(true);
        spawnObj.transform.position = pos;
        spawnObj.transform.rotation = rot;

        IPooledObject pooledObject = spawnObj.GetComponent<IPooledObject>();
        if (spawnObj != null)
        {
            pooledObject.OnObjectSpwn();
        }

        poolDictionry[tag].Enqueue(spawnObj);

        return spawnObj;
    }
}
