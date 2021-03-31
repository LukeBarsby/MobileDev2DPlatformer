using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.RemoteConfig;

public class RemoteControl : MonoBehaviour
{
    public struct userAttributes { }
    public struct appAttributes { }

    [SerializeField] Text titleText = default;
    [SerializeField] Text versionText = default;

    private void Awake()
    {
        ConfigManager.FetchCompleted += ChangeTitleText;
        ConfigManager.FetchCompleted += ChangeVersionNumber;
        //ignore the attribures and pass in default vals
        ConfigManager.FetchConfigs<userAttributes, appAttributes>(new userAttributes(), new appAttributes());
    }
    void ChangeTitleText(ConfigResponse response)
    {
        titleText.text = ConfigManager.appConfig.GetString("GameTitle");
    }
    void ChangeVersionNumber(ConfigResponse response)
    {
        versionText.text = ConfigManager.appConfig.GetString("VersionNum");
    }

    private void OnDestroy()
    {
        ConfigManager.FetchCompleted -= ChangeTitleText;
        ConfigManager.FetchCompleted -= ChangeVersionNumber;
    }


}
