using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public Item m_Item;
    public bool ItembeenTaken = false;
    PlayerController pc;
    Animator anim;
    string _id;

    void Start()
    {
        anim = GetComponent<Animator>();
        pc = PlayerController.Instance; 
    }
    public Item ReturnItem()
    {
        return m_Item;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerHolder" && !ItembeenTaken)
        {
            pc.OpenChestUI(m_Item, this.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "PlayerHolder" && !ItembeenTaken)
        {
            pc.OpenChestUI(m_Item, this.gameObject);
            CloseChest();
        }
        pc.CloseChestItemScreen();
        
    }

    public void TakeItem()
    {
        ItembeenTaken = true;
    }

    public void OpenChest()
    {
        anim.Play("ChestOpen"); 
    }
    public void CloseChest()
    {
        anim.Play("ChestClose");
    }

}
