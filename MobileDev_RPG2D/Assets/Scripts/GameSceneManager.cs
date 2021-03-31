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
    [HideInInspector] public int gold = 0;
    [HideInInspector] public bool playerLoaded;
    int id;

    void Start()
    {
        SceneManager.LoadScene("ObjectPoolerScene", LoadSceneMode.Additive);
        SceneManager.LoadScene("AudioManagerScene", LoadSceneMode.Additive);
        SceneManager.LoadScene("Player", LoadSceneMode.Additive);
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
        mainMenu.SetActive(false);
        SceneManager.LoadSceneAsync("Map_01", LoadSceneMode.Additive);
    }
    public void ReturnToMenu()
    {
        SceneManager.UnloadSceneAsync("Map_01");
        mainMenu.SetActive(true);
    }

    public void ChangeMusicLevel()
    {

    }

    public void ChangeSFXLevel()
    {

    }

    public void ChangeGraphics()
    {

    }

    public void MicroTransaction(int id)
    {
        this.id = id;
        if (id == 0)
        {
            MicrotransactionPanel.SetActive(true);
            microTransactionText.text = "Are you sure you want to spend £0.69 to get 100 coins?";
        }
        else if (id == 1)
        {
            MicrotransactionPanel.SetActive(true);
            microTransactionText.text = "Are you sure you want to spend £2.5 to get 500 coins?";
        }
        else if (id == 2)
        {
            MicrotransactionPanel.SetActive(true);
            microTransactionText.text = "Are you sure you want to spend £5 to get 1500 coins?";
        }
        else if (id == 3)
        {
            MicrotransactionPanel.SetActive(true);
            microTransactionText.text = "Are you sure you want to spend £10 to get 5000 coins?";
        }
        else if (id == 4)
        {
            MicrotransactionPanel.SetActive(true);
            microTransactionText.text = "Watch an add to get 25 coins?";
        }
    }
    public void CloseMicroTransactionPanel()
    {
        MicrotransactionPanel.SetActive(false);
    }

    public void ClaimMicroTransaction()
    {
        if (id == 0)
        {
            PlayerController.Instance.goldCount += 100;
            SavingSystem.Instance.SaveData();

            AnalyticsResult result = Analytics.CustomEvent("Purchased Micro Transaction", new Dictionary<string, object> { { "Cost", id } });
            Debug.Log("Result mt2 = " + result);

            MicrotransactionPanel.SetActive(false);
        }
        else if (id == 1)
        {
            PlayerController.Instance.goldCount += 500;
            SavingSystem.Instance.SaveData();

            AnalyticsResult result = Analytics.CustomEvent("Purchased Micro Transaction", new Dictionary<string, object> { {"Cost", id} });
            Debug.Log("Result mt2 = " + result);

            MicrotransactionPanel.SetActive(false);
        }
        else if (id == 2)
        {
            PlayerController.Instance.goldCount += 1500;
            SavingSystem.Instance.SaveData();

            AnalyticsResult result = Analytics.CustomEvent("Purchased Micro Transaction", new Dictionary<string, object> { { "Cost", id } });
            Debug.Log("Result mt2 = " + result);

            MicrotransactionPanel.SetActive(false);
        }
        else if (id == 3)
        {
            PlayerController.Instance.goldCount += 5000;
            SavingSystem.Instance.SaveData();

            AnalyticsResult result = Analytics.CustomEvent("Purchased Micro Transaction", new Dictionary<string, object> { { "Cost", id } });
            Debug.Log("Result mt2 = " + result);

            MicrotransactionPanel.SetActive(false);
        }
        else if (id == 4)
        {
            PlayerController.Instance.goldCount += 25;
            AddPanel.SetActive(true);

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
        MicrotransactionPanel.SetActive(false);
        AddPanel.SetActive(false);
    }

}
