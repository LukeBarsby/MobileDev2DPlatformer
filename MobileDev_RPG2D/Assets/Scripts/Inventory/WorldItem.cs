using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldItem : MonoBehaviour
{
    private Item item;
    private SpriteRenderer spriteRenderer;
    GameObject itemHolder;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        itemHolder = GameObject.Find("SpawnedItems");
    }

    public static WorldItem SpawnWorldItem(Vector3 pos, Item item)
    {
        Transform trans = Instantiate(ItemAssets.Instance.worldItem, pos, Quaternion.identity);
        WorldItem worldItem = trans.GetComponent<WorldItem>();
        worldItem.SetItem(item);
        return worldItem;
    }

    public void SetItem(Item item)
    {
        this.item = item;
        spriteRenderer.sprite = item.GetSprite();
        gameObject.transform.parent = itemHolder.transform;
    }

    public Item GetItem()
    {
        return item;
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
