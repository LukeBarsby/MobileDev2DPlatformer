using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Facebook.Unity;

public class FacebookSharing : MonoBehaviour
{
    

    private void Awake()
    {
        if (!FB.IsInitialized)
        {
            FB.Init(() =>
            {
                if (FB.IsInitialized)
                {
                    FB.ActivateApp();
                }
                else
                {
                    Debug.LogError("Facebook couldn't initialize");
                }
            },
            isGameShown =>
            {
                if (!isGameShown)
                {
                    Time.timeScale = 0;
                }
                else
                {
                    Time.timeScale = 1;
                }
            });
        }
        else
        {
            FB.ActivateApp();
        }
    }

    public void FacebookLogin()
    {
        var permuissions = new List<string>() { "public_profile", "email", "user_friends" };
        FB.LogInWithReadPermissions(permuissions);
    }

    public void FacebookLogout()
    {
        FB.LogOut();
    }


    public void FacebookShare()
    {
        FB.ShareLink(new System.Uri("https://play.google.com/store"), "Check Out Mobile Quest",
            "New Exciting Game", new System.Uri("https://www.gstatic.com/android/market_images/web/play_prism_hlock_2x.png"));
    }
}
