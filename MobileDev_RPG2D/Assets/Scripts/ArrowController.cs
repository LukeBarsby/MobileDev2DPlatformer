using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour, IPooledObject
{
    float speed;
    float damage;
    Vector3 direction;
    Rigidbody2D rb;
    [SerializeField] bool playerArrow = default;
    [SerializeField] bool fireball = default;
    LayerMask collisionLayer;

    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        transform.rotation = q;

        rb.AddForce(direction * speed);
    }

    public void AddValues(float speedPar, float damagePar, Vector3 dir, LayerMask layer)
    {
        speed = speedPar;
        damage = damagePar;
        direction = dir;
        collisionLayer = layer;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (playerArrow)
        {
            if (collision.transform.tag == "Enemy" )
            {
                collision.transform.GetComponent<Enemy>().TakeDamage(PlayerController.Instance.rangeDamage);
            }

            if (collision.transform.tag != "Player")
            {
            }
        }
        else if (!playerArrow)
        {
            if (collision.transform.tag == "Player")
            {
                collision.transform.GetComponent<PlayerController>().TakeDamage(damage);
            }

            if (collision.transform.tag != "Enemy")
            {
            }
        }
        
    }

    public void OnObjectSpwn()
    {
        if (playerArrow)
        {
            AddValues(5, PlayerController.Instance.rangeDamage, (PlayerController.Instance.closestEnemy.transform.position - PlayerController.Instance.transform.position).normalized, 9);
        }
        else
        {
            AddValues(5, 15, (PlayerController.Instance.transform.position - transform.position).normalized, 9);
        }
    }
}
