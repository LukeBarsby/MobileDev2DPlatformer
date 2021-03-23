using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VarseController : MonoBehaviour
{
    Animator anim;
    BoxCollider2D bc;
    float timer = .25f;
    bool breakPot;
    private void Start()
    {
        anim = GetComponent<Animator>();
        bc = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (breakPot)
        {
            timer -= Time.deltaTime;
        }
        if (timer <= 0)
        {
            bc.enabled = false;
        }
    }

    public void TakeDamage(float damage)
    {
        anim.Play("varseBroken");
        transform.tag = "Untagged"
        breakPot = true;
    }
}
