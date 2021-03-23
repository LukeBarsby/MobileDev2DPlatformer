using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldSelectScript : MonoBehaviour
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

    // Update is called once per frame
    void Update()
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

        for (int i = 0; i < pos.Length; i++)
        {
            if (scrollPos < pos[i] + (distance / 2) && scrollPos > pos[i] - (distance / 2))
            {
                transform.GetChild(i).localScale = Vector2.Lerp(transform.GetChild(i).localScale, new Vector2(1, 1), 0.1f);
                for (int j = 0; j < pos.Length; j++)
                {
                    if (j != i)
                    {
                        transform.GetChild(j).localScale = Vector2.Lerp(transform.GetChild(j).localScale, new Vector2(0.8f, 0.8f), 0.1f);
                    }
                }
            }
        }

        if (Input.touchCount < 1)
        {
            for (int i = 0; i < pos.Length; i++)
            {
                transform.GetChild(i).localScale = new Vector2(1, 1);
            }
        }

    }

    public void LoadLevel()
    {
        for (int i = 0; i < pos.Length; i++)
        {
            if (scrollPos < pos[i] + (distance / 2) && scrollPos > pos[i] - (distance / 2))
            {
                switch (i)
                {
                    case 0:
                        GameSceneManager.Instance.LoadaLevel1();
                        break;
                    case 1:
                        Debug.Log("level 2");
                        break;
                    case 2:
                        Debug.Log("level 3");
                        break;
                    case 3:
                        Debug.Log("level 4");
                        break;
                    default:
                        break;
                }
            }
        }
    }
}

