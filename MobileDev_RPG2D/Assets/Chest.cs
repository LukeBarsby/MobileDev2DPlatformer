using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public Item m_Item;
    public bool ItembeenTaken = false;
    public Item ReturnItem()
    {
        return m_Item;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !ItembeenTaken)
        {
            PlayerController.Instance.OpenChestUI(m_Item, this.gameObject); 
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !ItembeenTaken)
        {
            PlayerController.Instance.OpenChestUI(m_Item, this.gameObject);
        }
    }

}
