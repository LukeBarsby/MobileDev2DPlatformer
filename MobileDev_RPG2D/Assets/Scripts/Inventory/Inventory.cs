using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    public event EventHandler OnItemListChanged;

    private List<Item> itemList;
    private Action<Item> useItemAction;
    private Action<Item> removeItemAction;

    public Inventory(Action<Item> useItemAction, Action<Item> removeItemAction)
    {
        this.useItemAction = useItemAction;
        this.removeItemAction = removeItemAction;
        itemList = new List<Item>();
    }

    public void AddItem(Item item)
    {
        if (item.IsStackable())
        {
            bool itemAlreadyInInventory = false;
            foreach (Item invenItem in itemList)
            {
                if (invenItem.itemType == item.itemType)
                {
                    invenItem.amount += item.amount;
                    itemAlreadyInInventory = true;
                    OnItemListChanged?.Invoke(this, EventArgs.Empty);
                }
            }
            if (!itemAlreadyInInventory)
            {
                itemList.Add(item);
                OnItemListChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        else
        {
            itemList.Add(item);
            OnItemListChanged?.Invoke(this, EventArgs.Empty);
        }
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public void RemoveItem(Item item)
    {
        removeItemAction(item);
    }
    public void UseItem(Item item)
    {
        useItemAction(item);
    }

    public List<Item> GetItemList()
    {
        return itemList;
    }

    public int InventorySize()
    {
        int size = itemList.Count;
        return size;
    }
}
