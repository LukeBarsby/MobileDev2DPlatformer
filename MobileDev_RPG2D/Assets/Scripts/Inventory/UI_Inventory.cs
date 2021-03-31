using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Inventory : MonoBehaviour
{
    private Inventory m_inventory;
    [SerializeField] Transform itemSlotTemplate = default;
    [SerializeField] Transform itemSlotContainer = default;
    [SerializeField] Image spriteImage = default;
    [SerializeField] Text itemCount = default;

    //Popup Inventory Item
    [SerializeField] GameObject popupWindow = default;
    //Popup Inventory Remove Item
    [SerializeField] GameObject popUpRemoveWindow = default;
    [Header("Navas Images")]
    [SerializeField] Image helmetImage = default;
    [SerializeField] Image chestImage = default;
    [SerializeField] Image legsImage = default;
    [SerializeField] Image feetImage = default;

    [SerializeField] Image swordImage = default;
    [SerializeField] Image shieldImage = default;
    [SerializeField] Image bowImage = default;
    [SerializeField] Image specialImage = default;
    [SerializeField] Material blankMat = default;
    [SerializeField] Sprite blankSprite = default;

    void OnDisable()
    {
        if (popupWindow.activeInHierarchy)
        {
            popupWindow.SetActive(false);
        }
        if (popUpRemoveWindow.activeInHierarchy)
        {
            popUpRemoveWindow.SetActive(false);
        }
    }

    public void SetInventory(Inventory inventory)
    {
        this.m_inventory = inventory;
        inventory.OnItemListChanged += Inventory_OnItemListChanged;
        RefreshInventoryItems();
    }

    private void Inventory_OnItemListChanged(object sender, EventArgs e)
    {
        RefreshInventoryItems();
    }

    public void RefreshInventoryItems()
    {
        foreach (Transform child in itemSlotContainer)
        {
            if (child == itemSlotTemplate)
            {
                continue;
            }
            else
            {
                Destroy(child.gameObject);
            }
        }
        foreach (Item item in m_inventory.GetItemList())
        {
            RectTransform itemSlotTransform =  Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
            itemSlotTransform.gameObject.SetActive(true);
            spriteImage.sprite = item.GetSprite();
            itemSlotTemplate.GetComponent<ItemIdentifier>().SetItem(item);


            if (item.amount > 1)
            {
                itemCount.text = item.amount.ToString();
            }
            else
            {
                itemCount.text = "";
            }
        }
    }

    public void OpenItemUI(Button button)
    {
        if (button.GetComponentInParent<ItemIdentifier>().m_Item.PopupWindowNeeded())
        {
            popupWindow.SetActive(true);
            Item tempItem = button.GetComponentInParent<ItemIdentifier>().ReturnItem();
            popupWindow.GetComponent<UI_UseItem>().SetItem(tempItem, m_inventory);
        }
    }
    public void OpenItemSlotUI(Button button)
    {
        if (button.GetComponentInParent<ItemIdentifier>().ReturnHasItem())
        {
            popUpRemoveWindow.SetActive(true);
            Item tempItem = button.GetComponentInParent<ItemIdentifier>().ReturnItem();
            popUpRemoveWindow.GetComponent<UI_RemoveItem>().SetItem(tempItem, m_inventory);
        }
    }

    public void SetEquipment(Item item)
    {
        
        switch (item.itemType)
        {
            case Item.ItemType.WoodSword:
                swordImage.sprite = item.GetSprite();
                swordImage.GetComponentInParent<ItemIdentifier>().SetItem(item);
                break;
            case Item.ItemType.IronSword:
                swordImage.sprite = item.GetSprite();
                swordImage.GetComponentInParent<ItemIdentifier>().SetItem(item);
                break;
            case Item.ItemType.WoodShield:
                shieldImage.sprite = item.GetSprite();
                shieldImage.GetComponentInParent<ItemIdentifier>().SetItem(item);
                break;
            case Item.ItemType.IronShield:
                shieldImage.sprite = item.GetSprite();
                shieldImage.GetComponentInParent<ItemIdentifier>().SetItem(item);
                break;
            case Item.ItemType.WoodBow:
                bowImage.sprite = item.GetSprite();
                bowImage.GetComponentInParent<ItemIdentifier>().SetItem(item);
                break;
            case Item.ItemType.IronBow:
                bowImage.sprite = item.GetSprite();
                bowImage.GetComponentInParent<ItemIdentifier>().SetItem(item);
                break;
            case Item.ItemType.LeatherHelmet:
                helmetImage.sprite = item.GetSprite();
                helmetImage.GetComponentInParent<ItemIdentifier>().SetItem(item);
                break;
            case Item.ItemType.IronHelmet:
                helmetImage.sprite = item.GetSprite();
                helmetImage.GetComponentInParent<ItemIdentifier>().SetItem(item);
                break;
            case Item.ItemType.LeatherChestPiece:
                chestImage.sprite = item.GetSprite();
                chestImage.GetComponentInParent<ItemIdentifier>().SetItem(item);
                break;
            case Item.ItemType.IronChestPiece:
                chestImage.sprite = item.GetSprite();
                chestImage.GetComponentInParent<ItemIdentifier>().SetItem(item);
                break;
            case Item.ItemType.LeatherLegs:
                legsImage.sprite = item.GetSprite();
                legsImage.GetComponentInParent<ItemIdentifier>().SetItem(item);
                break;
            case Item.ItemType.IronLegs:
                legsImage.sprite = item.GetSprite();
                legsImage.GetComponentInParent<ItemIdentifier>().SetItem(item);
                break;
            case Item.ItemType.LeatherFeet:
                feetImage.sprite = item.GetSprite();
                feetImage.GetComponentInParent<ItemIdentifier>().SetItem(item);
                break;
            case Item.ItemType.IronFeet:
                feetImage.sprite = item.GetSprite();
                feetImage.GetComponentInParent<ItemIdentifier>().SetItem(item);
                break;
            default:
                break;
        }
    }
    public void UnsetEquipment(Item item)
    {
        switch (item.itemType)
        {
            case Item.ItemType.WoodSword:
                swordImage.sprite = blankSprite;
                swordImage.GetComponentInParent<ItemIdentifier>().RemoveItem();
                break;
            case Item.ItemType.IronSword:
                swordImage.sprite = blankSprite;
                swordImage.GetComponentInParent<ItemIdentifier>().RemoveItem();
                break;
            case Item.ItemType.WoodShield:
                shieldImage.sprite = blankSprite;
                shieldImage.GetComponentInParent<ItemIdentifier>().RemoveItem();
                break;
            case Item.ItemType.IronShield:
                shieldImage.sprite = blankSprite;
                shieldImage.GetComponentInParent<ItemIdentifier>().RemoveItem();
                break;
            case Item.ItemType.WoodBow:
                bowImage.sprite = blankSprite;
                bowImage.GetComponentInParent<ItemIdentifier>().RemoveItem();
                break;
            case Item.ItemType.IronBow:
                bowImage.sprite = blankSprite;
                bowImage.GetComponentInParent<ItemIdentifier>().RemoveItem();
                break;
            case Item.ItemType.LeatherHelmet:
                helmetImage.sprite = blankSprite;
                helmetImage.GetComponentInParent<ItemIdentifier>().RemoveItem();
                break;
            case Item.ItemType.IronHelmet:
                helmetImage.sprite = blankSprite;
                helmetImage.GetComponentInParent<ItemIdentifier>().RemoveItem();
                break;
            case Item.ItemType.LeatherChestPiece:
                chestImage.sprite = blankSprite;
                chestImage.GetComponentInParent<ItemIdentifier>().RemoveItem();
                break;
            case Item.ItemType.IronChestPiece:
                chestImage.sprite = blankSprite;
                chestImage.GetComponentInParent<ItemIdentifier>().RemoveItem();
                break;
            case Item.ItemType.LeatherLegs:
                legsImage.sprite = blankSprite;
                legsImage.GetComponentInParent<ItemIdentifier>().RemoveItem();
                break;
            case Item.ItemType.IronLegs:
                legsImage.sprite = blankSprite;
                legsImage.GetComponentInParent<ItemIdentifier>().RemoveItem();
                break;
            case Item.ItemType.LeatherFeet:
                feetImage.sprite = blankSprite;
                feetImage.GetComponentInParent<ItemIdentifier>().RemoveItem();
                break;
            case Item.ItemType.IronFeet:
                feetImage.sprite = blankSprite;
                feetImage.GetComponentInParent<ItemIdentifier>().RemoveItem();
                break;
            default:
                break;
        }
    }
}
