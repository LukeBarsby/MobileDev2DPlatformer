using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Wizard : StateMachine
{
    #region Enemy Vals
    [SerializeField] float _damage = default;
    [SerializeField] float rushSpeed = default;
    [SerializeField] float _maxHealth = default;
    [SerializeField] float _idleMaxDistnce = default;
    [SerializeField] float _attackSpeed = default;
    GameObject _target;
    #endregion

    #region UI
    [SerializeField] Slider _slider = default;
    #endregion

    #region Timer
    float timer;
    #endregion

    #region Player Ref
    GameObject target;
    #endregion

    #region NavMesh
    NavMeshAgent agent;
    Vector3 goal;
    #endregion

    #region Components
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    public CircleCollider2D hitCollider;
    #endregion

    #region Effects
    Material standardMat;
    Material hitMaterial;
    #endregion

    #region State Assets
    public GameObject[] rushPoints;
    public GameObject[] fireBallLocations;
    public GameObject[] GreenLaserLocations;
    List<GameObject> firebllQueue = new List<GameObject>();
    public GameObject fireBallPrefab;
    float rushPointCounter = 0;
    bool fireballsSpawned = false;
    Vector2 lastKnownPosition;
    bool gotPos = false;
    public GameObject GreenLaserSpin;
    Quaternion q;
    bool gotNum;
    int randNum;

    float gTimer;
    int itemToRemove;
    #endregion

    void Start()
    {
        spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody2D>();
        if (FindObjectOfType<PlayerController>() != null)
        {
            target = FindObjectOfType<PlayerController>().gameObject;
        }


        //SetState(new wStartState(this));
        //System.Array.Reverse(rushPoints);
        StartSate();
        //StartCoroutine(SpawnFireBalls());
        for (int i = 0; i < GreenLaserLocations.Length; i++)
        {
            GreenLaserLocations[i].SetActive(false);
        }

        gotNum = true;
    }

    void Update()
    {
        //RandomMove();
        //RushAttackSate();
        //FireBallState();
        //GreenLazerStateSpin();
        GreenLazerHorizontal();
    }

    void StartSate()
    {
        Debug.Log("Boss appears");
        Debug.Log("Play Scream");
        Debug.Log("Boss Music");
    }

    public  void RandomMove()
    {
        if (timer <= 0)
        {
            Vector3 randomDir = Random.insideUnitSphere * _idleMaxDistnce;
            randomDir += transform.position;

            NavMeshHit hit;
            // -1 = all layers
            NavMesh.SamplePosition(randomDir, out hit, _idleMaxDistnce, -1);
            goal = hit.position;

            timer = 0;
            timer = 3;
        }
        timer -= Time.deltaTime;
        agent.updateRotation = false;
        agent.destination = goal;
    }

    void RushAttackSate()
    {
        hitCollider.enabled = false;
        for (int i = 0; i < rushPoints.Length; i++)
        {
            if (rushPointCounter == i)
            {
                transform.position = Vector2.MoveTowards(transform.position, rushPoints[i].transform.position, rushSpeed * Time.deltaTime);
            }
        }
        
    }


    void EndState()
    {
        Debug.Log("scream");
        Debug.Log("UI screen to exit manually or return to menu");
        Debug.Log("dead sprite");
    }

    void FireBallState()
    {
        if (fireballsSpawned)
        {
            StartCoroutine(ShootFireBalls());
        }
    }

    IEnumerator SpawnFireBalls()
    {
        for (int i = 0; i < fireBallLocations.Length; i++)
        {
            GameObject firebll = Instantiate(fireBallPrefab, fireBallLocations[i].transform.position, Quaternion.identity);
            firebllQueue.Add(firebll);
            yield return new WaitForSeconds(.5f);
        }
        fireballsSpawned = true;
    }
    IEnumerator ShootFireBalls()
    {
        for (int i = 0; i < firebllQueue.Count; i++)
        {
            firebllQueue[i].GetComponent<FireballController>().SetDirection();
            yield return new WaitForSeconds(1f);
        }
    }

    void GreenLazerStateSpin()
    {
        Vector3 direction = transform.position - PlayerController.Instance.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        q = Quaternion.AngleAxis(angle + 90, Vector3.forward);

        GreenLaserSpin.transform.rotation = Quaternion.Lerp(GreenLaserSpin.transform.rotation, q, 2 * Time.deltaTime);


    }

    void GreenLazerHorizontal()
    {
        if (gTimer <= 0)
        {
            GreenLaserLocations[itemToRemove].SetActive(false);
            randNum = Random.Range(0, GreenLaserLocations.Length);
            GreenLaserLocations[randNum].SetActive(true);
            itemToRemove = randNum;
            gTimer = 0;
            gTimer = 5;
            
        }
        if (gTimer > 0)
        {
        }
        
        gTimer -= Time.deltaTime;
    }

    //IEnumerator SpawnRandomLaser()
    //{

            
        
        
        
    //}


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "RushPoint")
        {
            rushPointCounter++;
        }
    }
}
