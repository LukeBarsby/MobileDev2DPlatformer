using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using Unity.Mathematics;

public class EnemyController : MonoBehaviour
{
    // Components
    Rigidbody2D rb;
    CircleCollider2D cp;
    Animator anim;

    [Header("Enemy Vals")]
    [SerializeField] float speed = default;
    [SerializeField] float maxHealth = default;
    float currentHealth;
    [SerializeField] float attackRange = default;
    [SerializeField] float sightRange = default;
    [SerializeField] float attackRate = default;
    bool inSightRange;
    bool inAttackRange;
    float timer;
    float attackTimer;
    Vector3 newPos; 
    Vector2 oldPos;

    [Header("Adjustable Vals")]
    [SerializeField] float movementRange = default;
    [SerializeField] bool slime = default;
    [SerializeField] bool skellyArcher = default;
    [SerializeField] bool Wizard = default;

    [Header("SkellyArcher")]
    [SerializeField] GameObject arrow = default;
    [SerializeField] float arrowDamage = default;
    [SerializeField] float arrowSpeed  = default;

    [Header("Slime")]
    [SerializeField] float slimeDamage = default;


    [Header("UI")]
    [SerializeField] Slider slider;

    //target 
    GameObject target;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cp = GetComponent<CircleCollider2D>();
        anim = GetComponent<Animator>();

        if (FindObjectOfType<PlayerController>() != null)
        {
            target = FindObjectOfType<PlayerController>().gameObject;
        }

        currentHealth = maxHealth;
        slider.value = currentHealth / 100;
        

        inSightRange = false;
        inAttackRange = false;
    }

    void Update()
    {
        CheckRange();
        Anim();
        slider.value = currentHealth / 100;

        if (!inSightRange && !inAttackRange)
        {
            RandomMove();
        }
        else if (inSightRange)
        {
            MoveToTarget();
        }
        else if (inAttackRange)
        {
            AttackTarget();
        }

    }


    void CheckRange()
    {
        float dis = Vector2.Distance(transform.position, target.transform.position);
        if (dis <= attackRange)
        {
            inAttackRange = true;
            inSightRange = false;
            timer = 0;
        }
        else if (dis <= sightRange)
        {
            inSightRange = true;
            inAttackRange = false;
            timer = 0;
        }
        else
        {
            inSightRange = false;
            inAttackRange = false;
        }
    }
    void RandomMove()
    {
        if (timer <= 0)
        {
            float randX = UnityEngine.Random.Range(movementRange, -movementRange);
            float randY = UnityEngine.Random.Range(movementRange, -movementRange);
            newPos = new Vector3(transform.position.x + randX, transform.position.y + randY, transform.position.z);
            oldPos = transform.position;
            timer = 0;
            timer += 5f;
        }
        transform.position = Vector2.MoveTowards(transform.position, newPos, speed * Time.deltaTime);
        timer -= Time.deltaTime;
    }
    public void TakeDamage(float damage, float knockback)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
        Vector3 dir =  transform.position - PlayerController.Instance.transform.position;
        transform.position += dir * knockback;
    }

    private void Die()
    {
        //play death
        Destroy(gameObject);
    }

    void MoveToTarget()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
    }

    void AttackTarget()
    {
        if (attackTimer <= 0)
        {
            //Debug.Log("Enemy Attacking");
            if (slime)
            {
                target.GetComponent<PlayerController>().TakeDamage(slimeDamage);
            }
            if (skellyArcher)
            {
                GameObject arrowObj = (GameObject)Instantiate(arrow, transform.position, quaternion.identity);
                arrowObj.GetComponent<ArrowController>().AddValues(arrowSpeed, arrowDamage, (target.transform.position - transform.position).normalized, 8);
            }



            attackTimer = 0;
            attackTimer += attackRate;
        }
        attackTimer -= Time.deltaTime;
    }



    private void Anim()
    {
        Vector2 pos = transform.position;
        pos.Normalize();
        if (skellyArcher)
        {
            anim.SetFloat("Horizontal", transform.position.x - oldPos.x);
            anim.SetFloat("Vertical", transform.position.y - oldPos.y);
        }


        if (slime)
        {
            anim.SetFloat("Horizontal", transform.position.x - oldPos.x);
            anim.SetFloat("Vertical", transform.position.y - oldPos.y);
        }

        if (Wizard)
        {
            anim.SetFloat("Horizontal", transform.position.x - oldPos.x);
        }
    }
}
