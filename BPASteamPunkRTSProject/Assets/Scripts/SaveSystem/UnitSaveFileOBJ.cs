using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSaveFileOBJ 
{

    int[] position;
    float health;
    public Vector3 realPosition;
    public UnitType unitType;
    public UnitSaveFileOBJ(int[] Position, float Health, UnitType _unitType, Vector3 _realposition)
    {
        position = Position;
        health = Health;
        unitType = _unitType;
        realPosition = _realposition;
    }
    public int[] GetPos()
    {
        return position;
    }
    public float getHealth()
    {

        return health;
    }
}