using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

public class PlayerController : Singleton<PlayerController>
{
    #region Components
    Rigidbody2D rb;
    #endregion

    #region UI Elements
    [Header("UI Elements")]
    [SerializeField] Joystick joystick = default;
    [SerializeField] Slider healthBar;
    [SerializeField] Slider blockBar;
    [SerializeField] Slider specialBar;
    [SerializeField] GameObject textBox;
    [SerializeField] Text text;    
    [SerializeField] GameObject returnToMenuPanel;
    [SerializeField] GameObject UI;
    [SerializeField] GameObject deathScreen;
    [SerializeField] GameObject endScreen;

    bool settingsOpen;

    #endregion

    #region weapon / armour
    [Header("Weapon / Armour Slots")]
    [SerializeField] public GameObject swordSlot  = default;
    [SerializeField] public GameObject bowSlot    = default;
    [SerializeField] public GameObject shieldSlot = default;
    [SerializeField] public GameObject headSlot = default;
    [SerializeField] public GameObject chestSlot = default;
    [SerializeField] public GameObject legsSlot = default;
    [SerializeField] public GameObject feetSlot = default;
    bool hasMeleeWeapon;
    bool hasBowWeapon;
    bool hasShield;



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
    bool meleeAttacking;
    public float currentHealth;
    [SerializeField] float dragTime = default;
    [SerializeField] float meleeDamage = default;
    [SerializeField] float defenceBuff = default;
    [SerializeField] public float rangeDamage = default;
    [SerializeField] public float rangeKnockBack = default;
    int deathCounter;
    float tickTimer;
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
    public List<GameObject> closestEnemies = new List<GameObject>();
    public List<GameObject> enemyCount = new List<GameObject>();
    bool attacking = false;
    float attackTimer;
    #endregion

    #region Movement Stuff
    Vector2 movement;
    float timer;
    float horizontal;
    float vertical;
    #endregion

    #region Inventory Stuff
    [SerializeField] public GameObject InventoryGameObject = default;
    bool invenOpen = false;
    bool chestUiOpen = false;
    public Inventory inventory;
    [SerializeField] UI_Inventory uiInventory = default;
    [SerializeField] GameObject ChestInventoryPanel = default;
    [SerializeField] GameObject ChestOpenPanel = default;
    Item item;
    GameObject chest;
    #endregion

    #region TicDamage
    [SerializeField] public float laserDamage = default;
    #endregion

    #region SavedData
    [HideInInspector] public int goldCount;
    [HideInInspector] public bool spokenToOldMan;
    [HideInInspector] public DateTime lastRewardTakenTime;
    [HideInInspector] public DateTime newRewardTime;
    [HideInInspector] public bool checkForNewReward;
    [HideInInspector] public bool rewaradBeenTaken;
    [HideInInspector] public bool PlayerLoaded;
    [HideInInspector] public int dayCounter = 0;
    [HideInInspector] public float musicVol;
    [HideInInspector] public float sfxVol;
    [HideInInspector] public List<string> facebookLoginDetails;
    #endregion

    void Awake()
    {
        rb   = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
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
        bowSpriteRenderer = bowSlot.GetComponent<SpriteRenderer>();

        SetUIInven();

        DisableUI();

        currentHealth = maxHealth;
        healthBar.value = currentHealth / 100;
        orignalSpeed = speed;

        hasMeleeWeapon = false;
        meleeAttacking = true;
        SetIdle();
        if (SaveSystem.LoadPlayerData() != null)
        {
            SavingSystem.Instance.LoadData();
        }
        else
        {
            SaveSystem.SavePlayerData(this);
        }
        DailyReward.Instance.startCheck = true;
        GameSceneManager.Instance.playerLoaded = true;
        SoundControl.Instance.SetSoundLevels();
    }

    public void SetUIInven()
    {
        inventory = new Inventory(UseItem, RemoveItem);
        uiInventory.SetInventory(inventory);
    }
    public void UseItem(Item item)
    {
        switch (item.itemType)
        {
            case Item.ItemType.WoodSword:
                EquipGear(swordSlot, item);
                break;
            case Item.ItemType.WoodShield:
                EquipGear(shieldSlot, item);
                break;
            case Item.ItemType.WoodBow:
                EquipGear(bowSlot, item);
                break;
            case Item.ItemType.IronSword:
                EquipGear(swordSlot, item);
                break;
            case Item.ItemType.IronShield:
                EquipGear(shieldSlot, item);
                break;
            case Item.ItemType.IronBow:
                EquipGear(bowSlot, item);
                break;
            case Item.ItemType.LeatherHelmet:
                EquipGear(headSlot, item);
                break;
            case Item.ItemType.LeatherChestPiece:
                EquipGear(chestSlot, item);
                break;
            case Item.ItemType.LeatherLegs:
                EquipGear(legsSlot, item);
                break;
            case Item.ItemType.LeatherFeet:
                EquipGear(feetSlot, item);
                break;    
            case Item.ItemType.IronHelmet:
                EquipGear(headSlot, item);
                break;
            case Item.ItemType.IronChestPiece:
                EquipGear(chestSlot, item);
                break;
            case Item.ItemType.IronLegs:
                EquipGear(legsSlot, item);
                break;
            case Item.ItemType.IronFeet:
                EquipGear(feetSlot, item);
                break;
            case Item.ItemType.HealthPotion:
                currentHealth += 25;
                if (currentHealth >= maxHealth)
                {
                    currentHealth = maxHealth;
                }
                inventory.RemoveItem(new Item { itemType = Item.ItemType.HealthPotion, stackable = true, amount = 1 });
                break;
            default:
                break;
        }
    }
    private void RemoveItem(Item item)
    {
        switch (item.itemType)
        {
            case Item.ItemType.WoodSword:
                UnequipGear(swordSlot, item);
                break;
            case Item.ItemType.WoodShield:
                UnequipGear(shieldSlot, item);
                break;
            case Item.ItemType.WoodBow:
                UnequipGear(bowSlot, item);
                break;
            case Item.ItemType.IronSword:
                UnequipGear(swordSlot, item);
                break;
            case Item.ItemType.IronShield:
                UnequipGear(shieldSlot, item);
                break;
            case Item.ItemType.IronBow:
                UnequipGear(bowSlot, item);
                break;
            case Item.ItemType.LeatherHelmet:
                UnequipGear(headSlot, item);
                break;
            case Item.ItemType.LeatherChestPiece:
                UnequipGear(chestSlot, item);
                break;
            case Item.ItemType.LeatherLegs:
                UnequipGear(legsSlot, item);
                break;
            case Item.ItemType.LeatherFeet:
                UnequipGear(feetSlot, item);
                break;
            case Item.ItemType.IronHelmet:
                UnequipGear(headSlot, item);
                break;
            case Item.ItemType.IronChestPiece:
                UnequipGear(chestSlot, item);
                break;
            case Item.ItemType.IronLegs:
                UnequipGear(legsSlot, item);
                break;
            case Item.ItemType.IronFeet:
                UnequipGear(feetSlot, item);
                break;
            default:
                break;
        }
    }

    public void EquipGear(GameObject slot, Item item)
    {
        if (slot.GetComponent<EquipedItem>().m_Item != item && item != null)
        {
            slot.GetComponent<EquipedItem>().SetItem(item);
            uiInventory.SetEquipment(item);
            AudioManager.Instance.PlaySound(AudioManager.Instance.sfx, "Select");
            GearCheck(item, item.GetMaterial(), true);
        }
    }
    void UnequipGear(GameObject slot, Item item)
    {
        slot.GetComponent<EquipedItem>().RemoveItem();
        uiInventory.UnsetEquipment(item);
        AudioManager.Instance.PlaySound(AudioManager.Instance.sfx, "Cancel");
        GearCheck(item, skinMat, false);
    }

    private void GearCheck(Item item, Material mat, bool hasGear)
    {
        switch (item.itemType)
        {
            case Item.ItemType.WoodSword:
            case Item.ItemType.IronSword:
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
            case Item.ItemType.WoodShield:
            case Item.ItemType.IronShield:
                if (shieldSlot.GetComponent<EquipedItem>().m_Item != null)
                {
                    hasShield = true;
                    shieldSpriteRenderer.material = mat;
                    defenceBuff += item.defence;
                }
                else
                {
                    defenceBuff -= item.defence;
                    hasShield = false;
                }
                break;
            case Item.ItemType.WoodBow:
            case Item.ItemType.IronBow:
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
            case Item.ItemType.LeatherHelmet:
            case Item.ItemType.IronHelmet:
                headAnimator.SetBool("hasHeadArmour", hasGear);
                headSpriteRenderer.material = mat;
                if (headSlot.GetComponent<EquipedItem>().m_Item != null)
                {
                    defenceBuff += item.defence;
                }
                else
                {
                    defenceBuff -= item.defence;
                }
                break;
            case Item.ItemType.LeatherChestPiece:
            case Item.ItemType.IronChestPiece:
                bodyAnimator.SetBool("hasBodyArmour", hasGear);
                chestSpriteRenderer.material = mat;
                if (chestSlot.GetComponent<EquipedItem>().m_Item != null)
                {
                    defenceBuff += item.defence;
                }
                else
                {
                    defenceBuff -= item.defence;
                }
                break;
            case Item.ItemType.LeatherLegs:
            case Item.ItemType.IronLegs:
                legsAnimator.SetBool("hasLegArmour", hasGear);
                legsSpriteRenderer.material = mat;
                if (legsSlot.GetComponent<EquipedItem>().m_Item != null)
                {
                    defenceBuff += item.defence;
                }
                else
                {
                    defenceBuff -= item.defence;
                }
                break;
            case Item.ItemType.LeatherFeet:
            case Item.ItemType.IronFeet:
                feetAnimator.SetBool("hasFeetArmour", hasGear);
                feetSpriteRenderer.material = mat;
                if (feetSlot.GetComponent<EquipedItem>().m_Item != null)
                {
                    defenceBuff += item.defence;
                }
                else
                {
                    defenceBuff -= item.defence;
                }
                break;
            default:
                Debug.Log(item.itemType + "cant be found");
                break;
        }
    }

    void Update()
    {
        if (enemyCount.Count > 0)
        {
            CheckRange();
        }
        ChangeWeapon();
        SpecialTime();
        

        healthBar.value = (currentHealth / maxHealth);
        blockBar.value =  (blockTimer / blockTime);
        specialBar.value =  (specialTimer / specialTime);
    }


    private void FixedUpdate()
    {
        if (!invenOpen && !settingsOpen)
        {
            Movement();
            Attack();
            Block();
            Anim();
        }
    }


    void Block()
    {
        if (blockTimer <= 0 && hasShield)
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
            AudioManager.Instance.PlaySound(AudioManager.Instance.sfx, "Block");
        }
        else if (!canBlock || Input.touchCount > 0)
        {
            if (defenceBuff > 0)
            {
                damage = damage - (damage / defenceBuff);
                currentHealth -= damage;
                AudioManager.Instance.PlaySound(AudioManager.Instance.sfx, "PlayerHurt");
            }
            else
            {
                currentHealth -= damage;
                AudioManager.Instance.PlaySound(AudioManager.Instance.sfx, "PlayerHurt");
            }

            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }

    public void TakeTicDamage(float damage)
    {
        if (defenceBuff > 0)
        {
            damage -= (damage / defenceBuff);
        }
        currentHealth -= damage * Time.deltaTime;

        if (tickTimer <= 0)
        {
            AudioManager.Instance.PlaySound(AudioManager.Instance.sfx, "PlayerHurt");
            tickTimer = 1;
        }
        if (tickTimer > 0)
        {
            tickTimer -= Time.deltaTime;
        }
    }

    void Die()
    {
        AnalyticsResult result = Analytics.CustomEvent("Player Died");
        if (deathCounter < 1)
        {
            deathScreen.SetActive(true);
            deathCounter++;
        }
        else if (deathCounter > 0)
        {
            deathScreen.SetActive(false);
            ReturnToMenu();
        }
        AudioManager.Instance.PlaySound(AudioManager.Instance.sfx, "PlayerDies");
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
                if (enemyCount.Count > 0 && closestEnemy.GetComponent<TakeDamageScript>() != null)
                {
                    if (closestEnemy.GetComponent<TakeDamageScript>().CanUseRange() && attackTimer <= 0)
                    {
                        if (!bowAnimator.GetCurrentAnimatorStateInfo(0).IsName("BowBlend"))
                        {
                            bowAnimator.PlayInFixedTime("BowBlend");
                            FireArrow();
                            attackTimer = 0;
                            attackTimer = rangedAttackRate;
                        }
                    }
                }
                else
                {
                    speed = orignalSpeed;
                }
            }
            attackTimer -= Time.deltaTime;
        }

        else if (meleeAttacking)
        {
            if (Input.touchCount > 0 && hasMeleeWeapon)
            {
                if (enemyCount.Count > 0)
                {
                    swordSlot.SetActive(true);
                    speed = combatSpeed;

                    if (!swordAnimator.GetCurrentAnimatorStateInfo(0).IsName("SwordBlend"))
                    {
                        swordAnimator.PlayInFixedTime("SwordBlend");
                        attackTimer += meleeAttackRate / 2;
                    }
                    if (attackTimer <= 0)
                    {
                        AudioManager.Instance.PlaySound(AudioManager.Instance.sfx, "Sword");
                        for (int z = 0; z < enemyCount.Count; z++)
                        {
                            if (enemyCount[z].GetComponent<TakeDamageScript>() != null)
                            {
                                enemyCount[z].GetComponent<TakeDamageScript>().TakeDamage(meleeDamage);
                            }
                        }
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
        AudioManager.Instance.PlaySound(AudioManager.Instance.sfx, "Arrow");
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
            if (enemyCount.Count > 0)
            {
                GameObject testEnemy = enemyCount[i];
                if (Vector2.Distance(transform.position, testEnemy.transform.position) > attackRange)
                {
                    continue;
                }
                else if (closestEnemy == null )
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
            if (Vector2.Distance(transform.position, enemyCount[i].transform.position) > attackRange)
            {
                enemyCount.Remove(enemyCount[i]);
            }
        }
    }

    public void SwitchWeapon()
    {
        meleeAttacking = !meleeAttacking;
        attackTimer = 0;
        UpdateRange();
        AudioManager.Instance.PlaySound(AudioManager.Instance.sfx, "Select");
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
    }

    public void SpecialAttack()
    {
        if (canUseSpecial)
        {
            AudioManager.Instance.PlaySound(AudioManager.Instance.sfx, "Select");
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


            SetMoveing(true);

            timer = 0;
            timer += dragTime;
        }
        else if (timer > 0)
        {
            rb.velocity = movement * (speed / 2);
            SetIdle();
        }
        else if (timer <= 0)
        {
            rb.velocity = Vector2.zero;

            SetMoveing(false);
        }
        timer -= Time.deltaTime;
    }

    void SetIdle()
    {
        headAnimator.Play("SetIdleHead");
        bodyAnimator.Play("SetIdleChest");
        legsAnimator.Play("SetIdleLegs");
        feetAnimator.Play("SetIdleFeet");
    }

    void SetMoveing(bool b)
    {
        headAnimator.SetBool("isMoving", b);
        bodyAnimator.SetBool("isMoving", b);
        legsAnimator.SetBool("isMoving", b);
        feetAnimator.SetBool("isMoving", b);
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
        chestUiOpen =! chestUiOpen;
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
        chestUiOpen = false;
        ChestOpenPanel.SetActive(false);
    }

    public void OpenChestItemScreen()
    {
        ChestInventoryPanel.SetActive(true);
        ChestInventoryPanel.GetComponent<ChestItemScript>().SetItem(item, inventory, chest);
        AudioManager.Instance.PlaySound(AudioManager.Instance.sfx, "FoundItem");
    }

    public void CloseChestItemScreen()
    {
        ChestInventoryPanel.SetActive(false);
        AudioManager.Instance.PlaySound(AudioManager.Instance.sfx, "Cancel");
    }

    public void OpenDialogMenu(string text)
    {
        textBox.SetActive(true);
        this.text.text = text;
        AudioManager.Instance.PlaySound(AudioManager.Instance.sfx, "Select");
    }

    public void CloseDialogMenu()
    {
        this.text.text = null;
        textBox.SetActive(false);
        AudioManager.Instance.PlaySound(AudioManager.Instance.sfx, "Cancel");
    }

    public void GiveItem(Item item)
    {
        inventory.AddItem(item);
        AudioManager.Instance.PlaySound(AudioManager.Instance.sfx, "Select");
    }

    public void OpenReturnMenu()
    {
        settingsOpen = !settingsOpen;
        if (settingsOpen)
        {
            returnToMenuPanel.SetActive(true);
        }
        else
        {
            returnToMenuPanel.SetActive(false);
        }
        AudioManager.Instance.PlaySound(AudioManager.Instance.sfx, "ping");
    }

    public void LeaveLevel()
    {
        if (FacebookSharing.Instance.ReturnLoggedIn() == true)
        {
            endScreen.SetActive(true);
        }
        else
        {
            endScreen.SetActive(false);
            ReturnToMenu();
        }
    }


    public void ReturnToMenu()
    {
        enemyCount.Clear();
        invenOpen = false;
        settingsOpen = false;
        endScreen.SetActive(false);
        currentHealth = maxHealth;
        deathCounter = 0;
        GameSceneManager.Instance.gold = goldCount;
        SavingSystem.Instance.SaveData();
        DisableUI();
        GameSceneManager.Instance.ReturnToMenu();
    }

    public void ShareToFacebook()
    {
        FacebookSharing.Instance.FacebookShare();
        endScreen.SetActive(false);
        ReturnToMenu();
    }

    public void EnableUI()
    {
        UI.SetActive(true);
        headSpriteRenderer.enabled = true;
        chestSpriteRenderer.enabled = true;
        legsSpriteRenderer.enabled = true;
        feetSpriteRenderer.enabled = true;
    }
    public void DisableUI()
    {
        deathScreen.SetActive(false);
        endScreen.SetActive(false);
        returnToMenuPanel.SetActive(false);
        UI.SetActive(false);
        headSpriteRenderer.enabled = false;
        chestSpriteRenderer.enabled = false;
        legsSpriteRenderer.enabled = false;
        feetSpriteRenderer.enabled = false;
    }

    public void ClearData()
    {
        goldCount = 0;
        spokenToOldMan = false;
        dayCounter = 0;
        newRewardTime = DateTime.Now;
        SetUIInven();
        swordSlot.GetComponent<EquipedItem>().RemoveItem();
        bowSlot.GetComponent<EquipedItem>().RemoveItem();
        shieldSlot.GetComponent<EquipedItem>().RemoveItem();
        headSlot.GetComponent<EquipedItem>().RemoveItem();
        chestSlot.GetComponent<EquipedItem>().RemoveItem();
        legsSlot.GetComponent<EquipedItem>().RemoveItem();
        feetSlot.GetComponent<EquipedItem>().RemoveItem();
    }
}
