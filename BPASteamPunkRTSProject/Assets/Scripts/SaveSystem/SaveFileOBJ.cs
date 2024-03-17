using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveFileOBJ 
{
   public List<UnitSaveFileOBJ> SaveUnits = new List<UnitSaveFileOBJ>();
   public List<BuildingSaveFileOBJ> SaveBuildings = new List<BuildingSaveFileOBJ>();
   public List<ResourceSaveOJB> SaveResourceDeps = new List<ResourceSaveOJB>();
    public IDictionary<string, float> keyValuePairs = new Dictionary<string, float>();
    public SaveFileOBJ(List<UnitSaveFileOBJ> SaveUnits2, List<BuildingSaveFileOBJ> SaveBuildings2, List<ResourceSaveOJB> SaveResourceDeps2, IDictionary<string, float> keyValuePairs2)
    {
        SaveUnits = SaveUnits2;
        SaveBuildings = SaveBuildings2;
        SaveResourceDeps = SaveResourceDeps2;
        keyValuePairs = keyValuePairs2;
    }
}
