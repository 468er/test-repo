using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSaveFileOBJ
{

    int[] position;
    float health;
    public CustomVector3 realPosition;
    public building_Type unitType;
    public BuildingSaveFileOBJ(int[] Position, float Health, building_Type _unitType, CustomVector3 _realposition)
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
