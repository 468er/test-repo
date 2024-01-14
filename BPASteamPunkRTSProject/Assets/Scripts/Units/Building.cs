using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public BuildingIdentity building;
    public float moveSpeed = 5f;
    public float Health = 10;
    public  building_Type _type;
    public bool isEnemy;
    public float FireInterval;
    public float lastFired;
    public GameObject manufacturedObject;
    //will block of squares by using the collider2dtrigger method. Without having to do any checking, it will automatically hit every square where it is blocking the majority of the square and 
    //turn off moving in that square.

    private void Update()
    {
        switch (_type)
        {
            case building_Type.factory:
                Manufacture();
                break;
        }
    }
    public void Manufacture()
    {
        if(lastFired + FireInterval <= Time.time)
        {
            lastFired = Time.time;
            GameObject spawn = Instantiate(manufacturedObject, this.transform.position, Quaternion.identity);
            spawn.GetComponent<UnitUnpackager>().Load(this.GetComponent<Pathing>().position);
        }
    }
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
        Debug.Log(this.gameObject + " has been killed");
        GameObject.Destroy(this.gameObject);
    }
}

public static class MappingTools
{
    public static int[] WorldPositionToPositionArray(Vector3 position)
    {
        int[] returnVar;
        returnVar = new int[] { Mathf.RoundToInt(position.x), Mathf.RoundToInt(position.y / 0.86602540378443864676372317075294f), Mathf.RoundToInt(position.z) };
        return returnVar;
    }
    public static Vector3 WorldPositionArrayToVector3(int[] position)
    {
        Vector3 returnVar;
        returnVar = new Vector3(position[0], position[1] * 0.86602540378443864676372317075294f, position[2]);
        return returnVar;
    }
}
public enum building_Type
{
    factory,
}
