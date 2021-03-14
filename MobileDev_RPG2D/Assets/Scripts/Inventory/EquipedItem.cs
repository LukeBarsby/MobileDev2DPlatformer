using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipedItem : MonoBehaviour
{
    public Item m_Item;

    public void SetItem(Item item)
    {
        m_Item = null;
        m_Item = item;
    }
    public void RemoveItem()
    {
        m_Item = null;
    }

    public Item ReturnItem()
    {
        return m_Item;
    }
}
