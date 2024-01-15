using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveFileOBJ 
{
   public List<UnitSaveFileOBJ> SaveUnits = new List<UnitSaveFileOBJ>();
   public List<BuildingSaveFileOBJ> SaveBuildings = new List<BuildingSaveFileOBJ>();
   public List<ResourceDep> SaveResourceDeps = new List<ResourceDep>();

   public SaveFileOBJ(List<UnitSaveFileOBJ> SaveUnits2, List<BuildingSaveFileOBJ> SaveBuildings2, List<ResourceDep> SaveResourceDeps2)
    {
        SaveUnits = SaveUnits2;
        SaveBuildings = SaveBuildings2;
        SaveResourceDeps = SaveResourceDeps2;
    }
}
