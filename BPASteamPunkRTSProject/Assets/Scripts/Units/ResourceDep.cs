using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceDep : MonoBehaviour
{
    public float Amount;
    public float MaxHealth;
    public Resource_Type _Type;
    public float Health;
    public PlayerController Player1;
    public void TakeDamage(GameObject attacker)
    {
        if (Health - attacker.GetComponent<Unit>().Damage > 0)
        {
            Health -= attacker.GetComponent<Unit>().Damage;
            Debug.Log(attacker.gameObject + " <- Hit ->" + this.gameObject + "for " + attacker.GetComponent<Unit>().Damage + "reducing the health to " + Health);
        }
        else
        {
            attacker.GetComponent<Unit>().inRange = false;
            Die();
        }

    }
    public void Die()
    {
        Debug.Log("Enemy has been killed");
        Inventory PlayerInventory = Player1.GetComponent<Inventory>();
        PlayerInventory.Add(_Type.ToString(), Amount);
        GameObject.Destroy(this.gameObject);
    }
}
