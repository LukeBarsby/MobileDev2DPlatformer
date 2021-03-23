using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamageScript : MonoBehaviour
{

    public enum Types
    {
        Slime,
        SkellyArcher,
        Wizard,
        vase
    }
    public Types enemyType;

    public void TakeDamage(float damage)
    {
        switch (enemyType)
        {
            case Types.Slime:
            case Types.SkellyArcher:
                gameObject.GetComponent<Enemy>().TakeDamage(damage);
                break;
            case Types.Wizard:
                gameObject.GetComponent<Wizard>().TakeDamage(damage);
                break;
            case Types.vase:
                gameObject.GetComponent<VarseController>().TakeDamage(damage);
                break;
            default:
                break;
        }
    }
}
