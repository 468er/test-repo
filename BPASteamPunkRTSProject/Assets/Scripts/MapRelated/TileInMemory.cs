using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileInMemory : MonoBehaviour
{
    public float f;
    public float g;
    public float h;
    public TileInMemory(float fCost, float gCost, int[] fillLocation)
    {
         f = fCost;
         g = gCost;
         h = f + g;
    }
    public int CompareTo(TileInMemory comparePart)
    {
        // A null value means that this object is greater.
        if (comparePart == null)
            return 1;

        else
            return this.h.CompareTo(comparePart.h);
    }
}
