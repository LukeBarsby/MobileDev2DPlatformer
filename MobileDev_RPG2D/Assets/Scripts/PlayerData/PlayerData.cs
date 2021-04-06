using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class PlayerData
{
    public int goldCount;
    public bool spokenToOldMan;
    public List<Item> itemList;

    public DateTime lastRewardTakenTime;
    public DateTime newRewardTime;
    public bool checkForNewReward;
    public bool rewaradBeenTaken;
    public int dayCounter = 0;

    //sound
    public float sfxVol;
    public float musicVol;


    //public Item _swordSlot;
    //public Item _bowSlot ;
    //public Item _shieldSlot;
    //public Item _headSlot;
    //public Item _chestSlot;
    //public Item _legsSlot;
    //public Item _feetSlot;

    public PlayerData(PlayerController player )
    {
        goldCount = player.goldCount;
        spokenToOldMan = player.spokenToOldMan;
        itemList = player.inventory.GetItemList();

        lastRewardTakenTime = player.lastRewardTakenTime;
        newRewardTime = player.newRewardTime;
        checkForNewReward = player.checkForNewReward;
        rewaradBeenTaken = player.rewaradBeenTaken;
        dayCounter = player.dayCounter;

        sfxVol = player.sfxVol;
        musicVol = player.musicVol;

    //_swordSlot = player.swordSlot.GetComponent<EquipedItem>().ReturnItem();
    //_bowSlot = player.bowSlot.GetComponent<EquipedItem>().ReturnItem();
    //_shieldSlot = player.shieldSlot.GetComponent<EquipedItem>().ReturnItem();
    //_headSlot = player.headSlot.GetComponent<EquipedItem>().ReturnItem();
    //_chestSlot = player.chestSlot.GetComponent<EquipedItem>().ReturnItem();
    //_legsSlot = player.legsSlot.GetComponent<EquipedItem>().ReturnItem();
    //_feetSlot = player.feetSlot.GetComponent<EquipedItem>().ReturnItem();
    }
}
