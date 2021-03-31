using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] Text goldCount;
    void Start()
    {
        
    }

    void Update()
    {
        goldCount.text = PlayerController.Instance.goldCount.ToString();
    }
}
