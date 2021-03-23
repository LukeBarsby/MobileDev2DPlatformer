using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : Singleton<GameSceneManager>
{
    public enum Scenes
    {
        MainMenu,
        Level1,
        AudioManager
    }

    void Start()
    {
        SceneManager.LoadScene("AudioManagerScene", LoadSceneMode.Additive);
        SceneManager.LoadScene("ObjectPoolerScene", LoadSceneMode.Additive);
        SceneManager.LoadScene("Player", LoadSceneMode.Additive);
        SceneManager.LoadScene("Menu", LoadSceneMode.Additive);
    }

    void Update()
    {
        
    }

    public void ReturnToMenu()
    {
        SceneManager.UnloadSceneAsync("Map_01"); 
        SceneManager.LoadSceneAsync("Menu", LoadSceneMode.Additive); 
    }

    public void LoadaLevel1()
    {
        SceneManager.UnloadSceneAsync("Menu");
        SceneManager.LoadSceneAsync("Map_01", LoadSceneMode.Additive);
    }
}
