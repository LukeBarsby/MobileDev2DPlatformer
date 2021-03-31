using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;
using System;

public class DailyReward : Singleton<DailyReward>
{
    [SerializeField] GameObject rewardBox = default;
    [SerializeField] GameObject CloseButton = default;
    [SerializeField] Text rewardAmount = default;
    [SerializeField] Sprite giftClosed = default;
    [SerializeField] Sprite giftOpen = default;
    [SerializeField] Button giftButton = default;
    int[] days = {5,10,15,20,25,30,35,40,45,50,55,60,65,70,75,80,85,90,95,100,105,110,115,120,125,130,135,140,145,150};
    bool rewardBoxOpen = false;
    [HideInInspector] public bool startCheck;

    void Start()
    {

    }
    void Update()
    {
        if (startCheck)
        {
            if (PlayerController.Instance.checkForNewReward)
            {
                ResetDailyAward();
            }
            if (PlayerController.Instance.rewaradBeenTaken)
            {
                giftButton.image.sprite = giftOpen;
            }
        }
    }

    public void ResetDailyAward()
    {
        if (DateTime.Now >= PlayerController.Instance.newRewardTime)
        {
            ResetButton();
            PlayerController.Instance.rewaradBeenTaken = false;
            PlayerController.Instance.checkForNewReward = false;
        }
    }

    private void ResetButton()
    {
        giftButton.image.sprite = giftClosed;
    }

    public void GetReward()
    {
        PlayerController.Instance.goldCount += days[PlayerController.Instance.dayCounter];
        GameSceneManager.Instance.gold += days[PlayerController.Instance.dayCounter];
        PlayerController.Instance.dayCounter++;

        AnalyticsResult result = Analytics.CustomEvent("Taken Daily Reward");
        Debug.Log("Result dailyReward = " + result);

        if (PlayerController.Instance.dayCounter >= 30)
        {
            PlayerController.Instance.dayCounter = 0;
        }
        PlayerController.Instance.lastRewardTakenTime = DateTime.Now;
        PlayerController.Instance.newRewardTime = DateTime.Now.AddMinutes(0.5);
        giftButton.image.sprite = giftOpen;
        CloseRewardBox();
        PlayerController.Instance.rewaradBeenTaken = true;
        PlayerController.Instance.checkForNewReward = true;
        SavingSystem.Instance.SaveData();
    }

    public void CloseRewardBox()
    {
        OpenRewardBox();
    }
    public void OpenRewardBox()
    {
        if (!PlayerController.Instance.rewaradBeenTaken)
        {
            rewardBoxOpen = !rewardBoxOpen;
            if (rewardBoxOpen)
            {
                rewardBox.SetActive(true);
                CloseButton.SetActive(true);
                rewardAmount.text = days[PlayerController.Instance.dayCounter].ToString();
            }
            else if(!rewardBoxOpen)
            {
                rewardBox.SetActive(false);
                CloseButton.SetActive(false);
            }
        }
    }
}
