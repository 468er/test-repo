using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileInMemory
{
    public float f;
    public float g;
    public float h;
    public int[] fillLocation;
    public int x;
    public int y;
    public int layer;
    public TileInMemory(float fCost, float gCost, int[] filllocation, int xLoc, int yLoc, int layerLoc)
    {
         f = fCost;
         g = gCost;
         h = f + g;
        fillLocation = filllocation;
        x = xLoc;
        y = yLoc;
        layer = layerLoc;
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
