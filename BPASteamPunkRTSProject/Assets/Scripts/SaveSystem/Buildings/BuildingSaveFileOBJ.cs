using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSaveFileOBJ
{

    int[] position;
    float health;
    public CustomVector3 realPosition;
    public building_Type unitType;
    public float moveSpeed = 5f;
    public bool isEnemy;
    public float FireInterval;
    public UnitType Manufacturing;

    public BuildingSaveFileOBJ(int[] Position, float Health, building_Type _unitType, CustomVector3 _realposition, float _movespeed, bool _IsEnemy, float _FireInterval, UnitType _manufacturing)
    {
        position = Position;
        health = Health;
        unitType = _unitType;
        realPosition = _realposition;
        moveSpeed = _movespeed;
        isEnemy = _IsEnemy;
        FireInterval = _FireInterval;
        Manufacturing = _manufacturing;
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
