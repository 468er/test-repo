using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceDep : MonoBehaviour
{
    public float Amount;
    public float MaxHealth;
    public Resource_Type _Type;

    public float Health;
    public void TakeDamage(GameObject attacker)
    {
        if (Health - attacker.GetComponent<Unit>().Damage > 0)
        {
            Health -= attacker.GetComponent<Unit>().Damage;
            Debug.Log(attacker.gameObject + " <- Hit ->" + this.gameObject + "for " + attacker.GetComponent<Unit>().Damage + "reducing the health to " + Health);
        }
        else
        {
            Die();
        }

    }
    public void Die()
    {
        Debug.Log("Enemy has been killed");
        GameObject.Destroy(this.gameObject);
    }
}
