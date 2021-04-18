using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterPoints : MonoBehaviour
{
    [SerializeField] string id = default;
    LocationSwitcher ls;
    void Start()
    {
        ls = FindObjectOfType<LocationSwitcher>();
        if (ls != null)
        {
            ls.onEnterPointCollisionEnter += OnEnter;
        }
    }

    private void OnEnter(string id)
    {
        ls.ChangeLocation(id);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "PlayerHolder")
        {
            ls.TriggerEnter(id);
        }
    }

    private void OnDestroy()
    {
        ls.onEnterPointCollisionEnter -= OnEnter;
    }
}
