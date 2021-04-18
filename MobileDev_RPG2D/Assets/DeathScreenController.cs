using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScreenController : MonoBehaviour
{
    [SerializeField] GameObject addPanel;
    void OnEnable()
    {
        addPanel.SetActive(false);
        Time.timeScale = 0;
    }

    void OnDisable()
    {
        Time.timeScale = 1;
    }

    public void WatchAddToRespawn()
    {
        addPanel.SetActive(true);
    }

    public void Respawn()
    {
        gameObject.SetActive(false);
        addPanel.SetActive(false);
        PlayerController.Instance.currentHealth = 100;
    }
}
