using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileInMemory :  IComparable<TileInMemory>
{
    public float f;
    public float g;
    public float h;
    public int[] fillLocation;
    public int x;
    public int y;
    public int layer;
    public int[] locationAsArr;
    public Vector3 filllocationAsVector3;
    public Vector3 locationAsVector3;
    public TileInMemory(float fCost, float gCost, int[] filllocation, int xLoc, int yLoc, int layerLoc)
    {
         f = fCost;
         g = gCost;
         h = f + g;
        fillLocation = filllocation;
        x = xLoc;
        y = yLoc;
        layer = layerLoc;
        locationAsArr = new int[] { xLoc, yLoc, layerLoc };
        filllocationAsVector3 = new Vector3(filllocation[0], filllocation[1], filllocation[2]);
        locationAsVector3 = new Vector3(xLoc, yLoc, layerLoc);
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
