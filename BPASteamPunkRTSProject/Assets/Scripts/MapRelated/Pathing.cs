using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathing : MonoBehaviour
{
    public int[] position;
    void Update()
    {
        Unit unit = GetComponent<Unit>();
        if(unit != null)
        {
            position = unit.position;
        }
        Tile tile = GetComponent<Tile>();
        if (tile != null)
        {
            position = tile.position;
        }
    }
}
