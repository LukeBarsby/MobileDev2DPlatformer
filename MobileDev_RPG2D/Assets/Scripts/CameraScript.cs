using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Vector3 offset;
    GameObject player;

    void Start()
    {
        player = FindObjectOfType<PlayerController>().gameObject;
    }

    void FixedUpdate()
    {
        Vector3 desPos = player.transform.position + offset;

        transform.position = desPos;
        //transform.LookAt(desPos);

    }
}
