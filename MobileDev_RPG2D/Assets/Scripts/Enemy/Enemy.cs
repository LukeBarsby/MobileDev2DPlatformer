using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public abstract class Enemy : MonoBehaviour
{
    #region Enemy Vals
    protected float damage;
    protected float maxHealth;
    protected float currentHealth;
    protected float idleMaxDistnce;
    protected float attackSpeed;
    protected float attackRange;
    protected float sightRange;
    protected bool inSightRange;
    protected bool inAttackRange;
    protected bool isAttacking;
    bool hit;
    float hitTimer;
    #endregion

    #region Timer
    protected float timer;
    #endregion

    #region Player Ref
    protected GameObject target;
    #endregion

    #region NavMesh
    protected NavMeshAgent agent;
    protected Vector3 goal;
    #endregion

    #region Components
    protected CircleCollider2D circleCol;
    protected Rigidbody2D rb;
    protected SpriteRenderer spriteRenderer;
    #endregion

    #region Effects
    protected Material standardMat;
    protected Material hitMaterial;
    #endregion

    void Start()
    {
        inSightRange = false;
        inAttackRange = false;
        hit = false;

        target = PlayerController.Instance.gameObject;
    }

    public void FixedUpdate()
    {
        CheckRange();

        if (!inSightRange && !inAttackRange)
        {
            isAttacking = false;
            RandomMove();
        }
        else if (inSightRange)
        {
            isAttacking = false;
            MoveToTarget();
        }
        else if (inAttackRange)
        {
            isAttacking = true;
            AttackTarget();
        }

        if (hitTimer <= 0 && hit)
        {
            spriteRenderer.material = standardMat;
            hit = false;
        }
        hitTimer -= Time.deltaTime;
    }
    public virtual void AttackTarget()
    {
        if (timer <= 0)
        {
            agent.isStopped = true;
            PlayerController.Instance.TakeDamage(damage);

            timer = 0;
            timer = attackSpeed;
        }
        timer -= Time.deltaTime;
    }

    public virtual void MoveToTarget()
    {
        agent.destination = target.transform.position;
        if (agent.remainingDistance < .1f)
        {
            agent.isStopped = true;
        }
        else
        {
            agent.isStopped = false;
        }
    }
    public virtual void RandomMove()
    {
        if (timer <= 0)
        {
            agent.ResetPath();
            Vector3 randomDir = Random.insideUnitSphere * idleMaxDistnce;
            randomDir += transform.position;
            NavMeshHit hit;
            // -1 = all layers
            NavMesh.SamplePosition(randomDir, out hit, idleMaxDistnce, -1);
            goal = hit.position;
            timer = 0;
            timer = 5;
        }
        
        if (agent.remainingDistance < .05f)
        {
            agent.isStopped = true;
        }
        else
        {
            agent.isStopped = false;
        }
        timer -= Time.deltaTime;
        agent.updateRotation = false;
        agent.destination = goal;
    }

    void CheckRange()
    {
        float dis = Vector2.Distance(agent.transform.position, target.transform.position);
        if (dis <= attackRange)
        {
            inAttackRange = true;
            inSightRange = false;
        }
        else if (dis <= sightRange)
        {
            inSightRange = true;
            inAttackRange = false;
        }
        else
        {
            inSightRange = false;
            inAttackRange = false;
        }
    }




    public virtual void TakeDamage(float meleeDamage)
    {
        spriteRenderer.material = hitMaterial;
        hit = true;
        hitTimer = 0;
        hitTimer = .25f;
        AudioManager.Instance.PlaySound(AudioManager.Instance.sfx, "EnemyDamage");
        currentHealth -= meleeDamage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        gameObject.SetActive(false);
        AudioManager.Instance.PlaySound(AudioManager.Instance.sfx, "EnemyDie");
    }
}
