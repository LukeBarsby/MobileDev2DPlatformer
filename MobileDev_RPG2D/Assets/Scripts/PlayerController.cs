using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : Singleton<PlayerController>
{
    #region Components
    Rigidbody2D rb;
    CircleCollider2D cp;
    #endregion

    #region UI Elements
    [Header("UI Elements")]
    [SerializeField] Joystick joystick = default;
    [SerializeField] Button button_Special = default;
    [SerializeField] Button button_Switch = default;
    [SerializeField] Slider healthBar;
    [SerializeField] Slider blockBar;
    [SerializeField] Slider specialBar;
    #endregion

    #region weapon / armour
    [Header("Weapon / Armour Slots")]
    [SerializeField] GameObject swordSlot  = default;
    [SerializeField] GameObject bowSlot    = default;
    [SerializeField] GameObject shieldSlot = default;
    [SerializeField] GameObject headSlot = default;
    [SerializeField] GameObject chestSlot = default;
    [SerializeField] GameObject legsSlot = default;
    [SerializeField] GameObject feetSlot = default;
    [SerializeField] Material armourMat;
    [SerializeField] Material standardMat;
    bool hasMeleeWeapon;
    bool hasBowWeapon;
    bool hasShield;

    bool hasHeadArmour;
    bool hasBodyArmour;
    bool hasLegArmour;
    bool hasFeetArmour;

    //Animators and Rennderes
    Animator headAnimator;
    Animator bodyAnimator;
    Animator legsAnimator;
    Animator feetAnimator;

    Animator swordAnimator;
    Animator bowAnimator;
    Animator shieldAnimator;

    SpriteRenderer headSpriteRenderer;
    SpriteRenderer chestSpriteRenderer;
    SpriteRenderer legsSpriteRenderer;
    SpriteRenderer feetSpriteRenderer;
    
    SpriteRenderer swordSpriteRenderer;
    SpriteRenderer shieldSpriteRenderer;
    SpriteRenderer bowSpriteRenderer;


    [SerializeField] Material skinMat = default;

    #endregion

    #region Player Vals
    [Header("Player Vals")]
    [SerializeField] float speed = default;
    float orignalSpeed;
    [SerializeField] float combatSpeed = default;
    [SerializeField] float meleeAttackRate = default;
    [SerializeField] float rangedAttackRate = default;
    [SerializeField] float maxHealth = default;
    [SerializeField] float meleeAttackRange = default;
    [SerializeField] float rangeAttackRange = default;
    List<GameObject> enemyCount = new List<GameObject>();
    bool meleeAttacking;
    float currentHealth;
    [SerializeField] float dragTime = default;
    [SerializeField] float meleeDamage = default;
    [SerializeField] float defenceBuff = default;
    [SerializeField] public float rangeDamage = default;
    [SerializeField] public float rangeKnockBack = default;
    #endregion

    #region Block stuff
    [Header("Block Vals")]
    [SerializeField] float blockTime = default;
    float blockTimer;
    bool canBlock;
    #endregion

    #region Special Stuff
    [Header("Special Vals")]
    [SerializeField] float specialTime = default;
    float specialTimer;
    bool canUseSpecial;
    #endregion

    #region Attack stuff
    [SerializeField] CircleCollider2D attackRangeTrig = default;
    public GameObject closestEnemy = null;
    bool attacking = false;
    float attackTimer;
    [SerializeField] GameObject arrow = default;
    #endregion

    #region Movement Stuff
    //movement
    Vector2 movement;
    float timer;
    float horizontal;
    float vertical;
    #endregion

    #region Inventory Stuff
    [SerializeField] GameObject InventoryGameObject = default;
    bool invenOpen = false;
    bool chestUiOpen = false;
    Inventory inventory;
    [SerializeField] UI_Inventory uiInventory = default;
    [SerializeField] GameObject ChestInventoryPanel = default;
    [SerializeField] GameObject ChestOpenPanel = default;
    Item item;
    GameObject chest;
    #endregion

    #region TicDamage
    [SerializeField] float laserDamage = default;
    #endregion

    void Awake()
    {
        rb   = GetComponent<Rigidbody2D>();
        cp   = GetComponent<CircleCollider2D>();

        headAnimator = headSlot.GetComponent<Animator>();
        bodyAnimator = chestSlot.GetComponent<Animator>();
        legsAnimator = legsSlot.GetComponent<Animator>();
        feetAnimator = feetSlot.GetComponent<Animator>();

        swordAnimator = swordSlot.GetComponent<Animator>();
        shieldAnimator = shieldSlot.GetComponent<Animator>();
        bowAnimator = bowSlot.GetComponent<Animator>();

        headSpriteRenderer = headSlot.GetComponent<SpriteRenderer>();
        chestSpriteRenderer = chestSlot.GetComponent<SpriteRenderer>();
        legsSpriteRenderer = legsSlot.GetComponent<SpriteRenderer>();
        feetSpriteRenderer = feetSlot.GetComponent<SpriteRenderer>();

        swordSpriteRenderer = swordSlot.GetComponent<SpriteRenderer>();
        shieldSpriteRenderer = shieldSlot.GetComponent<SpriteRenderer>();
        bowSpriteRenderer = legsSlot.GetComponent<SpriteRenderer>();

        inventory = new Inventory(UseItem, RemoveItem);
        uiInventory.SetInventory(inventory);
    }

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.value = currentHealth / 100;
        orignalSpeed = speed;

        hasMeleeWeapon = false;
        meleeAttacking = true;

    }

    private void UseItem(Item item)
    {
        switch (item.itemType)
        {
            case Item.ItemType.Sword:
                EquipGear(swordSlot, item);
                break;
            case Item.ItemType.Shield:
                EquipGear(shieldSlot, item);
                break;
            case Item.ItemType.Bow:
                EquipGear(bowSlot, item);
                break;
            case Item.ItemType.HealthPotion:
                
                break;
            case Item.ItemType.Helmet:
                EquipGear(headSlot, item);
                break;
            case Item.ItemType.ChestPiece:
                EquipGear(chestSlot, item);
                break;
            case Item.ItemType.Legs:
                EquipGear(legsSlot, item);
                break;
            case Item.ItemType.Feet:
                EquipGear(feetSlot, item);
                break;
            default:
                break;
        }
    }
    private void RemoveItem(Item item)
    {
        switch (item.itemType)
        {
            case Item.ItemType.Sword:
                UnequipGear(swordSlot, item);
                break;
            case Item.ItemType.Shield:
                UnequipGear(shieldSlot, item);
                break;
            case Item.ItemType.Bow:
                UnequipGear(bowSlot, item);
                break;
            case Item.ItemType.HealthPotion:

                break;
            case Item.ItemType.Helmet:
                UnequipGear(headSlot, item);
                break;
            case Item.ItemType.ChestPiece:
                UnequipGear(chestSlot, item);
                break;
            case Item.ItemType.Legs:
                UnequipGear(legsSlot, item);
                break;
            case Item.ItemType.Feet:
                UnequipGear(feetSlot, item);
                break;
            default:
                break;
        }
    }

    void EquipGear(GameObject slot, Item item)
    {
        slot.GetComponent<EquipedItem>().SetItem(item);
        uiInventory.SetEquipment(item);

        GearCheck(item, item.material, true);
    }
    void UnequipGear(GameObject slot, Item item)
    {
        slot.GetComponent<EquipedItem>().RemoveItem();
        uiInventory.UnsetEquipment(item);

        GearCheck(item, skinMat, false);
    }

    private void GearCheck(Item item, Material mat, bool hasGear)
    {
        switch (item.itemType)
        {
            case Item.ItemType.Sword:
                if (swordSlot.GetComponent<EquipedItem>().m_Item != null)
                {
                    hasMeleeWeapon = true;
                    meleeDamage = swordSlot.GetComponent<EquipedItem>().ReturnItem().damage;
                    swordSpriteRenderer.material = mat;
                }
                else
                {
                    hasMeleeWeapon = false;
                }
                break;
            case Item.ItemType.Shield:
                if (shieldSlot.GetComponent<EquipedItem>().m_Item != null)
                {
                    hasShield = true;
                    swordSpriteRenderer.material = mat;
                }
                else
                {
                    hasShield = false;
                }
                break;
            case Item.ItemType.Bow:
                if (bowSlot.GetComponent<EquipedItem>().m_Item != null)
                {
                    hasBowWeapon = true;
                    rangeDamage = bowSlot.GetComponent<EquipedItem>().ReturnItem().damage;
                    bowSpriteRenderer.material = mat;
                }
                else
                {
                    hasBowWeapon = false;
                }
                break;
            case Item.ItemType.HealthPotion:
                break;
            case Item.ItemType.Helmet:
                headAnimator.SetBool("hasHeadArmour", hasGear);
                headSpriteRenderer.material = mat;
                break;
            case Item.ItemType.ChestPiece:
                bodyAnimator.SetBool("hasBodyArmour", hasGear);
                chestSpriteRenderer.material = mat;
                break;
            case Item.ItemType.Legs:
                legsAnimator.SetBool("hasLegArmour", hasGear);
                legsSpriteRenderer.material = mat;
                break;
            case Item.ItemType.Feet:
                feetAnimator.SetBool("hasFeetArmour", hasGear);
                feetSpriteRenderer.material = mat;
                break;
            default:
                Debug.Log(item.itemType + "cant be found");
                break;
        }
    }

    void Update()
    {
        CheckRange();
        ChangeWeapon();
        SpecialTime();


        healthBar.value = currentHealth / maxHealth;
        blockBar.value =  blockTimer / blockTime;
        specialBar.value =  specialTimer / specialTime;
    }


    private void FixedUpdate()
    {
        Movement();
        Attack();
        Block();
        Anim();
    }


    void Block()
    {
        if (blockTimer <= 0)
        {
            canBlock = true;
            attacking = true;
        }
        if (blockTimer > 0)
        {
            blockTimer -= Time.deltaTime;
            canBlock = false;
        }
    }

    void SpecialTime()
    {
        if (specialTimer <= 0)
        {
            canUseSpecial = true;
        }
        if (specialTimer > 0)
        {
            specialTimer -= Time.deltaTime;
            canUseSpecial = false;
        }
    }

    public void TakeDamage(float damage)
    {
        if (canBlock && Input.touchCount == 0)
        {
            attacking = false;
            shieldAnimator.Play("ShieldBlend");
            blockTimer = blockTime;
            
        }
        else if (!canBlock || Input.touchCount > 0)
        {
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }
    public void TakeTicDamage(float damage)
    {
        currentHealth -= damage * Time.deltaTime;
    }
    void Die()
    {
        SceneManager.LoadScene(0);
    }

    private void ChangeWeapon()
    {
        if (meleeAttacking)
        {
            attackRangeTrig.radius = meleeAttackRange - .5f;
        }
        else if (!meleeAttacking)
        {

            attackRangeTrig.radius = rangeAttackRange - .5f;
        }
    }


    void Attack()
    {
        if (enemyCount.Count <= 0 && meleeAttacking)
        {
            swordSlot.SetActive(false);
        }

        if (!meleeAttacking)
        {
            if (Input.touchCount <= 0 && hasBowWeapon)
            {
                if (enemyCount.Count > 0)
                {
                    if (closestEnemy.GetComponent<Enemy>() != null && attackTimer <= 0)
                    {
                        if (!bowAnimator.GetCurrentAnimatorStateInfo(0).IsName("BowBlend"))
                        {
                            bowAnimator.PlayInFixedTime("BowBlend");
                        }
                        FireArrow();
                        attackTimer = 0;
                        attackTimer = rangedAttackRate;
                    }
                }
                else
                {
                    speed = orignalSpeed;
                }
                attackTimer -= Time.deltaTime;
            }
        }

        else if (meleeAttacking)
        {
            if (Input.touchCount > 0 && hasMeleeWeapon)
            {
                if (enemyCount.Count > 0)
                {
                    swordSlot.SetActive(true);
                    speed = combatSpeed;

                    if (closestEnemy.GetComponent<Enemy>() != null && attackTimer <= 0)
                    {
                        if (!swordAnimator.GetCurrentAnimatorStateInfo(0).IsName("SwordBlend"))
                        {
                            swordAnimator.PlayInFixedTime("SwordBlend");
                        }
                        closestEnemy.GetComponent<Enemy>().TakeDamage(meleeDamage);
                        attackTimer = 0;
                        attackTimer = meleeAttackRate;
                    }
                }
                speed = orignalSpeed;
                attackTimer -= Time.deltaTime;
            }
            else
            {
                swordSlot.SetActive(false);
                return;
            }
        } 
    }

    private void FireArrow()
    {
        ObjectPooler.Instance.SpawnFromPool("PlayerArrow", transform.position, quaternion.identity);
    }

    void CheckRange()
    {
        float attackRange;
        if (meleeAttacking)
        {
            attackRange = meleeAttackRange;
        }
        else
        {
            attackRange = rangeAttackRange;
        }
        if (enemyCount == null)
        {
            return;
        }
        for (int i = 0; i < enemyCount.Count; i++)
        {
            GameObject testEnemy = enemyCount[i];
            if (Vector2.Distance(transform.position, testEnemy.transform.position) > attackRange)
            {
                continue;
            }
            if (closestEnemy == null)
            {
                closestEnemy = testEnemy;
            }
            else
            {
                if (Vector2.Distance(transform.position, testEnemy.transform.position) < Vector2.Distance(transform.position, closestEnemy.transform.position))
                {
                    closestEnemy = testEnemy;

                }
            }
        }
    }

    void UpdateRange()
    {
        float attackRange;
        if (meleeAttacking)
        {
            attackRange = meleeAttackRange;
        }
        else
        {
            attackRange = rangeAttackRange;
        }
        for (int i = 0; i < enemyCount.Count; i++)
        {
            if (Vector2.Distance(transform.position, enemyCount[i].transform.position) < attackRange)
            {
                enemyCount.Remove(enemyCount[i]);
            }
        }
    }

    public void SwitchWeapon()
    {
        meleeAttacking = !meleeAttacking;
        enemyCount.Clear();
        UpdateRange();
        if (meleeAttacking)
        {
            swordSlot.SetActive(true);
            bowSlot.SetActive(false);
        }
        else if (!meleeAttacking)
        {
            swordSlot.SetActive(false);
            bowSlot.SetActive(true);
        }
        Debug.Log("melee = ture, Ramged = false : " + meleeAttacking);
    }

    public void SpecialAttack()
    {
        if (canUseSpecial)
        {
            //slot -> get special item script -> use function in script
            currentHealth += 50;
            specialTimer = specialTime;
            if (currentHealth >= maxHealth)
            {
                currentHealth = maxHealth;
            }
        }
    }

    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            enemyCount.Add(collision.gameObject);
        }

        if (collision.tag == "BossRush")
        {
            TakeDamage(30);
        }

        WorldItem worldItem = collision.GetComponent<WorldItem>();
        if (worldItem != null)
        {
            inventory.AddItem(worldItem.GetItem());
            worldItem.DestroySelf();
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        enemyCount.Remove(collision.gameObject);
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Laser")
        {
            TakeTicDamage(laserDamage);
        }    
    }

    private void Anim()
    {
        movement.x = joystick.Horizontal;
        movement.y = joystick.Vertical;

        if (swordSlot.activeSelf)
        {
            swordAnimator.SetFloat("x", movement.x);
            swordAnimator.SetFloat("y", movement.y);
        }
        if (bowSlot.activeSelf)
        {
            bowAnimator.SetFloat("x", movement.x);
            bowAnimator.SetFloat("y", movement.y);
        }

        shieldAnimator.SetFloat("x", movement.x);
        shieldAnimator.SetFloat("y", movement.y);
    }

    private void Movement()
    {
        movement = new Vector2(horizontal, vertical);

        if (Input.touchCount > 0)
        {
            horizontal = joystick.Horizontal;
            vertical = joystick.Vertical;
            rb.velocity = movement * speed;


            headAnimator.SetFloat("x", movement.x);
            headAnimator.SetFloat("y", movement.y);

            bodyAnimator.SetFloat("x", movement.x);
            bodyAnimator.SetFloat("y", movement.y);

            legsAnimator.SetFloat("x", movement.x);
            legsAnimator.SetFloat("y", movement.y);

            feetAnimator.SetFloat("x", movement.x);
            feetAnimator.SetFloat("y", movement.y);


            headAnimator.SetBool("isMoving", true);

            bodyAnimator.SetBool("isMoving", true);

            legsAnimator.SetBool("isMoving", true);

            feetAnimator.SetBool("isMoving", true);

            timer = 0;
            timer += dragTime;
        }
        else if (timer > 0)
        {
            rb.velocity = movement * (speed / 2);
        }
        else if (timer <= 0)
        {
            rb.velocity = Vector2.zero;

            headAnimator.SetBool("isMoving", false);
            headAnimator.Play("IdleHead");
            bodyAnimator.SetBool("isMoving", false);
            bodyAnimator.Play("IdleChest");
            legsAnimator.SetBool("isMoving", false);
            legsAnimator.Play("IdleLegs");
            feetAnimator.SetBool("isMoving", false);
            feetAnimator.Play("IdleFeet");
        }
        timer -= Time.deltaTime;
    }

    public void OpenInven()
    {
        invenOpen = !invenOpen;
        if (invenOpen)
        {
            InventoryGameObject.SetActive(true);
        }
        else
        {
            InventoryGameObject.SetActive(false);
        }
        AudioManager.Instance.PlaySound(AudioManager.Instance.sfx, "ping");
    }

    public void OpenChestUI(Item p_item, GameObject p_chest)
    {
        chestUiOpen = !chestUiOpen;
        if (chestUiOpen)
        {
            item = p_item;
            chest = p_chest;
            ChestOpenPanel.SetActive(true);
        }
        else
        {
            ChestOpenPanel.SetActive(false);
        }
    }
    public void CloseChestOpen()
    {
        ChestOpenPanel.SetActive(false);
    }

    public void OpenChestItemScreen()
    {
        ChestInventoryPanel.SetActive(true);
        ChestInventoryPanel.GetComponent<ChestItemScript>().SetItem(item, inventory, chest);
    }

    public void CloseChestItemScreen()
    {
        item = null;
        chest = null;
        ChestInventoryPanel.SetActive(false);
    }
}
