using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int goldCount;
    public List<Item> itemList;

    public PlayerData(PlayerController player )
    {
        goldCount = player.goldCount;

        itemList = player.inventory.GetItemList();
    }
}
