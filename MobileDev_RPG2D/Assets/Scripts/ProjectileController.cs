using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour, IPooledObject
{
    public enum ProjectileType
    {
        PlayerArrow,
        EnemyArrow,
        EnemyFireball
    }
    Rigidbody2D rb;
    [SerializeField] ProjectileType fireMode;
    [SerializeField] float speed = default;
    [SerializeField] float damage = default;
    Vector3 direction;
    bool shoot;
    LayerMask collisionLayer = 9; //all
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        shoot = false;
    }

    void Update()
    {
        CheckReady(fireMode);
    }
    void CheckReady(ProjectileType sendArgs)
    {
        switch (sendArgs)
        {
            case ProjectileType.PlayerArrow:
                Fire();
                break;
            case ProjectileType.EnemyArrow:
                Fire();
                break;
            case ProjectileType.EnemyFireball:
                if (shoot)
                {
                    AddValues(speed, damage, (PlayerController.Instance.transform.position - transform.position).normalized, collisionLayer);
                    Fire();
                }
                break;
            default:
                break;
        }
    }

    void Fire()
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle + 90f, Vector3.forward);
        transform.rotation = q;

        rb.AddForce(direction * speed);
    }

    public void AddValues(float speedPar, float damagePar, Vector3 dir, LayerMask layer)
    {
        speed = speedPar;
        damage = damagePar;
        direction = dir;
        collisionLayer = layer;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (fireMode == ProjectileType.PlayerArrow)
        {
            if (collision.transform.tag == "Enemy")
            {
                if (collision.transform.GetComponent<TakeDamageScript>() != null)
                {
                    collision.transform.GetComponent<TakeDamageScript>().TakeDamage(PlayerController.Instance.rangeDamage);
                    ResetObj();
                }
            }
        }
        else if (fireMode == ProjectileType.EnemyArrow || fireMode == ProjectileType.EnemyFireball)
        {
            if (collision.transform.tag == "PlayerHolder")
            {
                if (collision.transform.GetComponentInParent<PlayerController>() != null)
                {
                    PlayerController.Instance.TakeDamage(damage);
                    ResetObj();
                }
            }
            else if (collision.transform.tag == "Untagged")
            {
                ResetObj();
            }
        }
    }

    void ResetObj()
    {
        AudioManager.Instance.PlaySound(AudioManager.Instance.sfx, "Explosion");
        direction = Vector3.zero;
        shoot = false;
        rb.velocity = Vector3.zero;
        gameObject.SetActive(false);
    }

    public void ShootFireBall()
    {
        shoot = true;
    }

    public void OnObjectSpwn()
    {
        if (fireMode == ProjectileType.PlayerArrow)
        {
            AudioManager.Instance.PlaySound(AudioManager.Instance.sfx, "Fire");
            AddValues(speed, PlayerController.Instance.rangeDamage, (PlayerController.Instance.closestEnemy.transform.position - PlayerController.Instance.transform.position).normalized, collisionLayer);
        }
        if (fireMode == ProjectileType.EnemyArrow)
        {
            AudioManager.Instance.PlaySound(AudioManager.Instance.sfx, "Fire");
            AddValues(speed, damage, (PlayerController.Instance.transform.position - transform.position).normalized, collisionLayer);
        }
    }
}
