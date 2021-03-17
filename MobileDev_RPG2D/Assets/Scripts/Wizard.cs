using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Wizard : StateMachine
{

    #region Enemy Vals
    [SerializeField] float _damage = default;
    [SerializeField] public float rushSpeed = default;
    [SerializeField] public float rushAcceleration = default;
    [SerializeField] float _maxHealth = default;
    [SerializeField] public float _idleMaxDistnce = default;
    [SerializeField] float _attackSpeed = default;
    GameObject _target;
    #endregion

    #region UI
    [SerializeField] Slider _slider = default;
    #endregion

    #region Player Ref
    GameObject target;
    #endregion

    #region NavMesh
    [HideInInspector] public NavMeshAgent agent;
    #endregion

    #region Components
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    public BoxCollider2D hitCollider;
    #endregion

    #region Effects
    Material standardMat;
    Material hitMaterial;
    #endregion

    #region State Assets
    [SerializeField] public GameObject[] rushPoints = default;
    [SerializeField] public GameObject[] fireBallLocations = default;
    [SerializeField] public GameObject[] GreenLaserLocations = default;
    [SerializeField] public GameObject fireBallPrefab = default;
    [SerializeField] public GameObject GreenLaserSpin = default;
    [SerializeField] public Transform bossSpawn = default;
    [HideInInspector] public float rushPointCounter = 0;
    public bool returnedToSpawnPos;
    
    #endregion

    void Start()
    {
        spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        agent = transform.parent.GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody2D>();
        if (FindObjectOfType<PlayerController>() != null)
        {
            target = FindObjectOfType<PlayerController>().gameObject;
        }
        //turn off horizontal beems
        for (int i = 0; i < GreenLaserLocations.Length; i++)
        {
            GreenLaserLocations[i].SetActive(false);
        }
        //turn off spinny laser
        GreenLaserSpin.SetActive(false);
        returnedToSpawnPos = false;


        SetState(new wRandomMove(this));
    }

    void Update()
    {
        UpdateState();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "RushPoint")
        {
            rushPointCounter++;
        }
        if (collision.transform.transform.tag == "BossSpawn")
        {
            returnedToSpawnPos = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.transform.tag == "BossSpawn")
        {
            returnedToSpawnPos = false;
        }
    }
}
