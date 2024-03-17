using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSaveFileOBJ 
{

    int[] position;
    float health;
    public CustomVector3 realPosition;
    public UnitType unitType;
    public UnitSaveFileOBJ(int[] Position, float Health, UnitType _unitType, CustomVector3 _realposition)
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
public class CustomVector3
{
    public float x;
    public float y;
    public float z;
    public CustomVector3(float X, float Y, float Z)
    {
        x = X;
        y = Y;
        z = Z;
    }
}
