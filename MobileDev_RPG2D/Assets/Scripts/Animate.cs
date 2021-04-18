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
    bool isAttacking;
    void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponentInParent<NavMeshAgent>();
        target = PlayerController.Instance.gameObject;
    }

    void Update()
    {
        if (isAttacking)//attacking
        {
            if (hasAttackAnim)
            {
                anim.SetBool("Attacking", isAttacking);
            }
            LookAtPlayer();
            anim.SetFloat("Horizontal", attackDir.x);
            anim.SetFloat("Vertical", attackDir.y);
            anim.SetBool("Moving", false);
        }
        if (agent.isStopped)
        {
            anim.SetBool("Moving", false);
        }
        else if (!agent.isStopped)//moving
        {
            if (hasAttackAnim)
            {
                anim.SetBool("Attacking", isAttacking);
            }
            anim.SetBool("Moving", true);
            Change();
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
