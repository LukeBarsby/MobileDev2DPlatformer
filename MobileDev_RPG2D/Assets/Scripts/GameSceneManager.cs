using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using UnityEngine.Analytics;
using UnityEngine.Audio;

public class GameSceneManager : Singleton<GameSceneManager>
{

    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject MicrotransactionPanel;
    [SerializeField] Text goldCount;
    [SerializeField] Text microTransactionText;
    [SerializeField] Button microTransactionButtonExit;
    [SerializeField] GameObject AddPanel;
    [SerializeField] public Animator transition;
    [SerializeField] float transitionTime;
    [HideInInspector] public int gold = 0;
    [HideInInspector] public bool playerLoaded;
    int id;

    void Start()
    {
        transition.Play("BlackToClear");
        SceneManager.LoadScene("ObjectPoolerScene", LoadSceneMode.Additive);
        SceneManager.LoadScene("AudioManagerScene", LoadSceneMode.Additive);
        SceneManager.LoadScene("Player", LoadSceneMode.Additive);
        StartCoroutine(StartMusic());
    }

    void Update()
    {
        if (playerLoaded)
        {
            goldCount.text = PlayerController.Instance.goldCount.ToString();
        }
    }

    public void LoadaLevel1()
    {
        AudioManager.Instance.PlaySound(AudioManager.Instance.sfx, "Select");
        AudioManager.Instance.StopSound(AudioManager.Instance.music, "MainMenu");
        AudioManager.Instance.PlaySound(AudioManager.Instance.music, "LevelMusic");
        StartCoroutine(Loadlevel("Map_01"));
    }
    public void ReturnToMenu()
    {
        AudioManager.Instance.PlaySound(AudioManager.Instance.sfx, "Select");
        AudioManager.Instance.StopSound(AudioManager.Instance.music, "LevelMusic");
        AudioManager.Instance.PlaySound(AudioManager.Instance.music, "MainMenu");
        StartCoroutine(LoadMenu("Map_01"));
    }

    IEnumerator Loadlevel(string levelName)
    {
        transition.Play("ClearToBlack");
        yield return new WaitForSeconds(transitionTime / 2);
        mainMenu.SetActive(false);
        yield return new WaitForSeconds(transitionTime / 2);
        SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive);
    }
    IEnumerator LoadMenu(string levelName)
    {
        transition.Play("ClearToBlack");
        yield return new WaitForSeconds(transitionTime);
        mainMenu.SetActive(true);
        SceneManager.UnloadSceneAsync("Map_01");
    }
    IEnumerator StartMusic()
    {
        yield return new WaitForSeconds(transitionTime);
        AudioManager.Instance.PlaySound(AudioManager.Instance.music, "MainMenu");
    }

    public void MicroTransaction(int id)
    {
        this.id = id;
        if (id == 0)
        {
            AudioManager.Instance.PlaySound(AudioManager.Instance.sfx, "Select");
            MicrotransactionPanel.SetActive(true);
            microTransactionText.text = "Are you sure you want to spend £0.69 to get 100 coins?";
        }
        else if (id == 1)
        {
            AudioManager.Instance.PlaySound(AudioManager.Instance.sfx, "Select");
            MicrotransactionPanel.SetActive(true);
            microTransactionText.text = "Are you sure you want to spend £2.5 to get 500 coins?";
        }
        else if (id == 2)
        {
            AudioManager.Instance.PlaySound(AudioManager.Instance.sfx, "Select");
            MicrotransactionPanel.SetActive(true);
            microTransactionText.text = "Are you sure you want to spend £5 to get 1500 coins?";
        }
        else if (id == 3)
        {
            AudioManager.Instance.PlaySound(AudioManager.Instance.sfx, "Select");
            MicrotransactionPanel.SetActive(true);
            microTransactionText.text = "Are you sure you want to spend £10 to get 5000 coins?";
        }
        else if (id == 4)
        {
            AudioManager.Instance.PlaySound(AudioManager.Instance.sfx, "Select");
            MicrotransactionPanel.SetActive(true);
            microTransactionText.text = "Watch an add to get 25 coins?";
        }
    }
    public void CloseMicroTransactionPanel()
    {
        AudioManager.Instance.PlaySound(AudioManager.Instance.sfx, "Cancel");
        MicrotransactionPanel.SetActive(false);
    }

    public void ClaimMicroTransaction()
    {
        if (id == 0)
        {
            PlayerController.Instance.goldCount += 100;
            SavingSystem.Instance.SaveData();
            AudioManager.Instance.PlaySound(AudioManager.Instance.sfx, "Select");
            AnalyticsResult result = Analytics.CustomEvent("Purchased Micro Transaction", new Dictionary<string, object> { { "Cost", id } });
            Debug.Log("Result mt2 = " + result);

            MicrotransactionPanel.SetActive(false);
        }
        else if (id == 1)
        {
            PlayerController.Instance.goldCount += 500;
            SavingSystem.Instance.SaveData();
            AudioManager.Instance.PlaySound(AudioManager.Instance.sfx, "Select");
            AnalyticsResult result = Analytics.CustomEvent("Purchased Micro Transaction", new Dictionary<string, object> { {"Cost", id} });
            Debug.Log("Result mt2 = " + result);

            MicrotransactionPanel.SetActive(false);
        }
        else if (id == 2)
        {
            PlayerController.Instance.goldCount += 1500;
            SavingSystem.Instance.SaveData();
            AudioManager.Instance.PlaySound(AudioManager.Instance.sfx, "Select");
            AnalyticsResult result = Analytics.CustomEvent("Purchased Micro Transaction", new Dictionary<string, object> { { "Cost", id } });
            Debug.Log("Result mt2 = " + result);

            MicrotransactionPanel.SetActive(false);
        }
        else if (id == 3)
        {
            PlayerController.Instance.goldCount += 5000;
            SavingSystem.Instance.SaveData();
            AudioManager.Instance.PlaySound(AudioManager.Instance.sfx, "Select");
            AnalyticsResult result = Analytics.CustomEvent("Purchased Micro Transaction", new Dictionary<string, object> { { "Cost", id } });
            Debug.Log("Result mt2 = " + result);

            MicrotransactionPanel.SetActive(false);
        }
        else if (id == 4)
        {
            PlayerController.Instance.goldCount += 25;
            AddPanel.SetActive(true);
            AudioManager.Instance.PlaySound(AudioManager.Instance.sfx, "Select");
            if (Advertisement.IsReady())
            {
                Advertisement.Show();
            }

            AnalyticsResult result = Analytics.CustomEvent("Watched Rewarded Add");
            Debug.Log("Result add = " + result);

            SavingSystem.Instance.SaveData();
        }
    }

    public void CloseAddPanel()
    {
        AudioManager.Instance.PlaySound(AudioManager.Instance.sfx, "Cancel");
        MicrotransactionPanel.SetActive(false);
        AddPanel.SetActive(false);
    }

    public void BuyItem(int id)
    {
        if (id == 0)
        {
            if (PlayerController.Instance.goldCount >= 100)
            {
                PlayerController.Instance.goldCount -= 100;
                AudioManager.Instance.PlaySound(AudioManager.Instance.sfx, "Select");
                PlayerController.Instance.inventory.AddItem(new Item { itemType = Item.ItemType.IronShield, stackable = false, amount = 1 , isArmour = true, defence = 10, material = Item.Materials.Iron, disription = "Iron Shield"});
                SavingSystem.Instance.SaveData();
            }
            else
            {
                AudioManager.Instance.PlaySound(AudioManager.Instance.sfx, "Cancel");
            }
        }
        else if (id == 1)
        {
            if (PlayerController.Instance.goldCount >= 100)
            {
                PlayerController.Instance.goldCount -= 100;
                AudioManager.Instance.PlaySound(AudioManager.Instance.sfx, "Select");
                PlayerController.Instance.inventory.AddItem(new Item { itemType = Item.ItemType.IronHelmet, stackable = false, amount = 1, isArmour = true, defence = 10, material = Item.Materials.Iron, disription = "Iron Helmet" });
                SavingSystem.Instance.SaveData();
            }
            else
            {
                AudioManager.Instance.PlaySound(AudioManager.Instance.sfx, "Cancel");
            }
        }
    }

}
