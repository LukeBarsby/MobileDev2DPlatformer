using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Animate : MonoBehaviour
{
    [SerializeField] bool hasAttackAnim;
    Vector3 attackDir;
    Animator anim;
    NavMeshAgent agent;
    GameObject target;
    bool isMoving;
    bool isAttacking;
    void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponentInParent<NavMeshAgent>();
        target = PlayerController.Instance.gameObject;
        isMoving = false;
        
    }

    void Update()
    {
        if (isAttacking)
        {
            if (hasAttackAnim)
            {
                anim.SetBool("Attacking", isAttacking);
            }
            LookAtPlayer();
            anim.SetFloat("Horizontal", attackDir.x);
            anim.SetFloat("Vertical", attackDir.y);
        }
        else if (!agent.isStopped)
        {
            if (hasAttackAnim)
            {
                anim.SetBool("Attacking", isAttacking);
            }
            isMoving = true;
            anim.SetBool("Moving", isMoving);
            Change();
        }
        else
        {
            isMoving = false;
            anim.SetBool("Moving", isMoving);
        }
    }

    void Change()
    {
        anim.SetFloat("Horizontal", agent.velocity.x);
        anim.SetFloat("Vertical", agent.velocity.y);
    }

    void LookAtPlayer()
    {
        attackDir = target.transform.position - transform.position;
    }

    public void SetAttacking(bool attacking)
    {
        isAttacking = attacking;
    }
}
