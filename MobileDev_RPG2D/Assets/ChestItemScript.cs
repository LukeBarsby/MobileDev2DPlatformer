using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestItemScript : MonoBehaviour
{
    Item item;
    GameObject chest;
    Inventory inventory;
    [SerializeField] Image spriteImage = default;
    [SerializeField] Text itemName = default;
    [SerializeField] Text itemDisc = default;

    public void SetItem(Item item, Inventory iven, GameObject chest)
    {
        this.item = item;
        this.inventory = iven;
        this.chest = chest;
        spriteImage.sprite = item.GetSprite();
        itemName.text = item.itemType.ToString();
        itemDisc.text = item.itemType.ToString();
    }

    public void CloseWindow()
    {
        this.item = null;
        gameObject.SetActive(false);
    }
    public void TakeItem()
    {
        inventory.AddItem(item);
        chest.GetComponent<Chest>().ItembeenTaken = true;
        PlayerController.Instance.CloseChestOpen();
        CloseWindow();
    }
}
