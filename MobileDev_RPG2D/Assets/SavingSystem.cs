using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavingSystem : Singleton<SavingSystem>
{
    void Start()
    {
    }

    void Update()
    {
        
    }

    public void SaveData()
    {
        SaveSystem.SavePlayerData(PlayerController.Instance);
    }
    public void LoadData()
    {
        PlayerData data = SaveSystem.LoadPlayerData();

        PlayerController.Instance.goldCount = data.goldCount;

        for (int i = 0; i < data.itemList.Count; i++)
        {
            PlayerController.Instance.inventory.AddItem(data.itemList[i]);
        }
    }
}
