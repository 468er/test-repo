using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSaveObject 
{
    public Vector3 position;
    public Color color;
    public UnitSaveObject(Color Color2, Vector3 pos)
    {
        position = pos;
        color = Color2;
    } 
}
