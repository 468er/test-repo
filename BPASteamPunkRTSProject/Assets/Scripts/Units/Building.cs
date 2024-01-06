using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public BuildingIdentity building;
    public float moveSpeed = 5f;
    public float Health = 10;
    public  building_Type _type;
    //will block of squares by using the collider2dtrigger method. Without having to do any checking, it will automatically hit every square where it is blocking the majority of the square and 
    //turn off moving in that square.
}
public enum building_Type
{
    factory,
}
