using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    public enum NPCs
    {
        OldMan,
    }
    [SerializeField] NPCs npc;
    [SerializeField] string text;
    [SerializeField] Item[] items;
    bool beenOpened;
    PlayerController pc;
    void Start()
    {
        pc = PlayerController.Instance;
        beenOpened = false;
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerHolder" && !beenOpened && pc.spokenToOldMan == false)
        {
            pc.OpenDialogMenu(text);
            for (int i = 0; i < items.Length; i++)
            {
                pc.GiveItem(items[i]);
                pc.spokenToOldMan = true;
            }
            beenOpened = true;
        }
    }
}
