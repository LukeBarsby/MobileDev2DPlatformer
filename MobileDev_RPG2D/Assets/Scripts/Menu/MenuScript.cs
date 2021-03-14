using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public GameObject scrollBar;
    Scrollbar sb;
    float scrollPos = 0;
    float[] pos;
    float distance;


    private void Awake()
    {
        sb = scrollBar.GetComponent<Scrollbar>();
    }
    private void Start()
    {
        pos = new float[transform.childCount];
        distance = 1f / (pos.Length - 1f);
        for (int i = 0; i < pos.Length; i++)
        {
            pos[i] = distance * i;
        }
        scrollPos = sb.value;
    }
    private void Update()
    {
        if (Input.touchCount > 0)
        {
            scrollPos = sb.value;
        }
        else
        {
            for (int i = 0; i < pos.Length; i++)
            {
                if (scrollPos < pos[i] + (distance / 2) && scrollPos > pos[i] - (distance / 2))
                {
                    scrollPos = sb.value = Mathf.Lerp(scrollPos = sb.value, pos[i], 0.1f);
                }
            }
        }
    }
    public void PlayGame()
    {
    }

    public void StorePage()
    {
        sb.value = 0;
    }
    public void InventoryPage()
    {
        sb.value = 0.33f;
    }
    public void PlayPage()
    {
        sb.value = 0.66f;
    }
    public void SettingsPage()
    {
        sb.value = 0.99f;
    }

}
