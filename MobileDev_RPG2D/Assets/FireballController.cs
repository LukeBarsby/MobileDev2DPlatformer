using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballController : MonoBehaviour
{
    [SerializeField] float speed = default;
    [SerializeField] float damage = default;
    Vector3 direction;
    Rigidbody2D rb;
    bool shoot = false;

    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (shoot)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(angle + 90f, Vector3.forward);
            transform.rotation = q;

            rb.AddForce(direction * speed);
        }
    }

    public void SetDirection()
    {
        direction = PlayerController.Instance.transform.position - transform.position;
        shoot = true;
    }
}
