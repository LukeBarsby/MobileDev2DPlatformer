using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    void Start()
    {
        SceneManager.LoadScene("Map_01", LoadSceneMode.Additive);
        SceneManager.LoadScene("AudioManagerScene", LoadSceneMode.Additive);
        SceneManager.LoadScene("ObjectPoolerScene", LoadSceneMode.Additive);
        SceneManager.LoadScene("Player", LoadSceneMode.Additive);
    }
}
