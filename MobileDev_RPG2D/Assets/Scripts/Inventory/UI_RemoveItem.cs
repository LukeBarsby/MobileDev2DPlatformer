using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_RemoveItem : MonoBehaviour
{
    Item item;
    Inventory inventory;
    [SerializeField] Image spriteImage = default;
    [SerializeField] Text itemName = default;
    [SerializeField] Text itemDisc = default;

    public void SetItem(Item item, Inventory iven)
    {
        this.item = item;
        this.inventory = iven;
        spriteImage.sprite = item.GetSprite();
        itemName.text = item.itemType.ToString();
        itemDisc.text = item.itemType.ToString();
    }

    public void CloseWindow()
    {
        this.item = null;
        gameObject.SetActive(false);
    }
    public void RemoveItem()
    {
        inventory.RemoveItem(item);
        CloseWindow();
    }
}
