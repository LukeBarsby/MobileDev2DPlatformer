using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavingSystem : Singleton<SavingSystem>
{
    public void SaveData()
    {
        SaveSystem.SavePlayerData(PlayerController.Instance);
    }
    public void LoadData()
    {
        PlayerData data = SaveSystem.LoadPlayerData();

        PlayerController.Instance.goldCount = data.goldCount;

        PlayerController.Instance.spokenToOldMan = data.spokenToOldMan;
        PlayerController.Instance.lastRewardTakenTime = data.lastRewardTakenTime;
        PlayerController.Instance.newRewardTime = data.newRewardTime;
        PlayerController.Instance.checkForNewReward = data.checkForNewReward;
        PlayerController.Instance.rewaradBeenTaken = data.rewaradBeenTaken;
        PlayerController.Instance.dayCounter = data.dayCounter;

        PlayerController.Instance.musicVol = data.musicVol;
        PlayerController.Instance.sfxVol = data.sfxVol;


        for (int i = 0; i < data.itemList.Count; i++)
        {
            PlayerController.Instance.inventory.AddItem(data.itemList[i]);
        }
    }

    public void DeleteData()
    {
        SaveSystem.DeleteSavedData();
        PlayerController.Instance.ClearData();
        SaveSystem.SavePlayerData(PlayerController.Instance);
    }
}
