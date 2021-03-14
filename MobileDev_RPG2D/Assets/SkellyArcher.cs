using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using System;

public class SkellyArcher : Enemy
{
    #region Enemy Vals
    [SerializeField] float _damage = default;
    [SerializeField] float _maxHealth = default;
    [SerializeField] float _idleMaxDistnce = default;
    [SerializeField] float _attackSpeed = default;
    [SerializeField] float _attackRange = default;
    [SerializeField] float _sightRange = default;
    [SerializeField] float _stoppingDistance = default;
    GameObject _target;
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

        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody2D>();
        circleCol = GetComponent<CircleCollider2D>();

        if (FindObjectOfType<PlayerController>() != null)
        {
            target = FindObjectOfType<PlayerController>().gameObject;
        }
    }

    private void Update()
    {
        _slider.value = currentHealth / 100;
    }

    public override void AttackTarget()
    {
        if (timer <= 0)
        {
            FireArrow();

            timer = 0;
            timer = attackSpeed;
        }
        timer -= Time.deltaTime;
    }

    private void FireArrow()
    {
        ObjectPooler.Instance.SpawnFromPool("EnemyArrow", transform.position, Quaternion.identity);
    }
}
