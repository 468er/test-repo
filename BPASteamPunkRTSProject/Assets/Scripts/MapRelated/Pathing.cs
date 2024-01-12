using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathing : MonoBehaviour
{
    public int[] position = new int[] { 0, 0, 0 };
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
        if(tile == null && unit == null)
        {
            if (MappingTools.WorldPositionArrayToVector3(position) != transform.position)
            {
                position = MappingTools.WorldPositionToPositionArray(transform.position);
            }
        }
    }
}
