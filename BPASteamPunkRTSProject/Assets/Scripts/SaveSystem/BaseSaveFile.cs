using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSaveFile
{
    int[] position;
     int health;
    public BaseSaveFile(int[] Position, int Health)
    {
        position = Position;
        health = Health;
    }
    public int[] GetPos()
    {
        return position;
    }
    public int getHealth()
    {

        return health;
    }
}
