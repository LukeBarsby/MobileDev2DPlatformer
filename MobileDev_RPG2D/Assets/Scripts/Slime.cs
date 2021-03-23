using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Slime : Enemy
{
    #region Enemy Vals
    [SerializeField] float _damage = default;
    [SerializeField] float _maxHealth = default;
    [SerializeField] float _idleMaxDistnce = default;
    [SerializeField] float _attackSpeed = default;
    [SerializeField] float _attackRange = default;
    [SerializeField] float _sightRange = default;
    bool attacking;
    Animate animate;
    #endregion

    #region UI
    [SerializeField] Slider _slider = default;
    #endregion

    #region Effects
    public Material _standardMat;
    public Material _hitMaterial;
    #endregion

    void Start()
    {
        damage = _damage;
        maxHealth = _maxHealth;
        idleMaxDistnce = _idleMaxDistnce;
        attackSpeed = _attackSpeed;
        attackRange = _attackRange;
        sightRange = _sightRange;
        currentHealth = maxHealth;
        standardMat = _standardMat;
        hitMaterial = _hitMaterial;
        spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();

        agent     = GetComponent<NavMeshAgent>();
        rb        = GetComponent<Rigidbody2D>();
        circleCol = GetComponent<CircleCollider2D>();
        if (FindObjectOfType<PlayerController>() != null)
        {
            target = FindObjectOfType<PlayerController>().gameObject;
        }
        animate = GetComponentInChildren<Animate>();
    }

    private void Update()
    {
        _slider.value = currentHealth / maxHealth;
        attacking = isAttacking;
        animate.SetAttacking(attacking);
    }


}
