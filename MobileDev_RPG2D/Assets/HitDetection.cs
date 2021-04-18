using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDetection : MonoBehaviour
{
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Laser")
        {
            PlayerController.Instance.TakeTicDamage(PlayerController.Instance.laserDamage);
        }
    }
}
