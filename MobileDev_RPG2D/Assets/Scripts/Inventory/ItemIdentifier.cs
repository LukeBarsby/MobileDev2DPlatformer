using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemIdentifier : MonoBehaviour
{
    public Item m_Item = null;
    bool hasItem = false;

    public void SetItem(Item item)
    {
        hasItem = true;
        m_Item = item;
    }

    public void RemoveItem()
    {
        hasItem = false;
    }

    public bool ReturnHasItem()
    {
        return hasItem;
    }

    public Item ReturnItem()
    {
        return m_Item;
    }
}
