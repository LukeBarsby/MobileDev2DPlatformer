using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraphicsControl : Singleton<GraphicsControl>
{
    [SerializeField] private Dropdown dropDownBox = default;


    public void SetQuality()
    {
        dropDownBox.value = PlayerController.Instance.quality;
        QualitySettings.SetQualityLevel(PlayerController.Instance.quality);
    }

    public void SetQuality(int quality)
    {
        dropDownBox.value = quality;
        PlayerController.Instance.quality = quality;
        SaveSystem.SavePlayerData(PlayerController.Instance);
    }
}
