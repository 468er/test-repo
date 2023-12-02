using System;
using System.Collections.Generic;
using UnityEngine;

public class TileInMemory : IComparable<TileInMemory>
{
    public float f;
    public float g;
    public float h;
    public float z;
    public int[] fillLocation;
    public int x;
    public int y;
    public int layer;
    public int[] locationAsArr;
    public Vector3 filllocationAsVector3;
    public Vector3 locationAsVector3;
    public List<TileInMemory> children;
    public TileInMemory(float fCost, float gCost, int[] filllocation, int xLoc, int yLoc, int layerLoc, float zcost)
    {
        f = fCost;
        g = gCost;
        z = zcost;
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
public class ONETWOTHREE
{
    public List<TileInMemory> StoredTiles;
    public List<TileInMemory> PotentialTiles;
    public bool[,,] walkPath;
    public ONETWOTHREE(List<TileInMemory> storedTiles, List<TileInMemory> potentialTiles, bool[,,] WalkPath)
    {
        StoredTiles = storedTiles;
        PotentialTiles = potentialTiles;
        walkPath = new bool[WalkPath.GetLength(0), WalkPath.GetLength(1), WalkPath.GetLength(2)];
        for (int x = 0; x < WalkPath.GetLength(0); x++)
        {
            for (int y = 0; y < WalkPath.GetLength(1); y++)
            {
                for (int z = 0; z < WalkPath.GetLength(2); z++)
                {
                    walkPath[x, y, z] = WalkPath[x, y, z];
                }
            }
        }
    }
}

