using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceSaveOJB
{
    public float Amount;
    public float MaxHealth;
    public Resource_Type _Type;
    public CustomVector3 Position;
    public int[] ArrayPosition;
    public ResourceSaveOJB(float amount, float maxhp, Resource_Type type, CustomVector3 position)
    {
        Amount = amount;
        MaxHealth = maxhp;
        _Type = type;
        Position = position;
    }
}
