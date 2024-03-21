using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.CloudSave;
using Unity.Services.Core;
using UnityEngine.UI;
using Unity.Services.CloudCode;
using Newtonsoft.Json;
using TMPro;
public class SaveLoadManager : MonoBehaviour
{
    public GameObject player;
    public List<Unit> Units = new List<Unit>();
    public List<UnitSavOBJ> UnitsAfter = new List<UnitSavOBJ>();
    public List<Building> Buildings = new List<Building>();
    public string output;
    public List<ResourceDep> Resources = new List<ResourceDep>();
    public List<ResourceSaveOJB> ResourcesAFter = new List<ResourceSaveOJB>();

    public List<GameObject> UnitPrefabs = new List<GameObject>();
    public List<GameObject> BuildingPrefabs = new List<GameObject>();
    public List<GameObject> ResourcePRefabs = new List<GameObject>();

    public TMP_InputField jSonOutput;
    public async void Save()
    {

        GameObject[] GameUnits;
        GameObject[] Buildingss;
        GameObject[] ResourceDepss;
        GameUnits = GameObject.FindGameObjectsWithTag("Unit");
        Buildingss = GameObject.FindGameObjectsWithTag("Building");
        ResourceDepss = GameObject.FindGameObjectsWithTag("ResourceDep");
        List<UnitSaveFileOBJ> SaveUnits = new List<UnitSaveFileOBJ>();
        List<BuildingSaveFileOBJ> SaveBuildings = new List<BuildingSaveFileOBJ>();
        List<ResourceSaveOJB> SaveResourceDeps = new List<ResourceSaveOJB>();
        foreach (GameObject Obj in GameUnits)
        {
            Unit unit = Obj.GetComponent<Unit>();
            UnitSaveFileOBJ obj = new UnitSaveFileOBJ(unit.position, unit.Health, unit.GetComponent<Unit>().Indentifier, new CustomVector3( unit.transform.position.x, unit.transform.position.y, unit.transform.position.z));
            SaveUnits.Add(obj);
        } 
        foreach (GameObject Obj in Buildingss)
        {
            Building unit = Obj.GetComponent<Building>();
            BuildingSaveFileOBJ obj = new BuildingSaveFileOBJ(unit.GetComponent<Pathing>().position, unit.Health, unit.GetComponent<Building>()._type, new CustomVector3( unit.transform.position.x, unit.transform.position.y, unit.transform.position.z), unit.moveSpeed, unit.isEnemy, unit.FireInterval, unit.Manufacturing);
            SaveBuildings.Add(obj);
        } 
        foreach (GameObject Obj in ResourceDepss)
        {
            ResourceDep unit = Obj.GetComponent<ResourceDep>();
            ResourceSaveOJB obj = new ResourceSaveOJB( unit.GetComponent<ResourceDep>().Amount, unit.Health, unit.GetComponent<ResourceDep>()._Type, new CustomVector3(unit.transform.position.x, unit.transform.position.y, unit.transform.position.z));
            SaveResourceDeps.Add(obj);
        } 
        //foreach (Building unit in Buildings)
        //{
        //    UnitSaveFileOBJ obj = new UnitSaveFileOBJ(unit.GetComponent<Pathing>().position, unit.Health);
 
        //} 
        //foreach (ResourceDep unit in ResourceDeps)
        //{
        //    UnitSaveFileOBJ obj = new UnitSaveFileOBJ(unit.GetComponent<Pathing>().position, unit.Health);
 
        //}
        SaveFileOBJ baseSaveFile = new SaveFileOBJ(SaveUnits, SaveBuildings, SaveResourceDeps, player.GetComponent<Inventory>().keyValuePairs);
        //UnitSaveObject a = new UnitSaveObject(player.GetComponent<SpriteRenderer>().color, player.transform.position);
        //a.color = new Color(1, 1, 1);
        var json = JsonConvert.SerializeObject(baseSaveFile);
       
        
        char[] characters = json.ToCharArray();
        List<char> list = new List<char>();
        foreach (char item in characters)
        {
            if (item == '"')
            {
                list.Add('\\');
                list.Add(item);
            }
            else
            {
                list.Add(item);
            }
        }
        char[] listAsArr = list.ToArray();
        string pureJson = new string(listAsArr);
        print(pureJson);
       
        print(json);
        var data = new Dictionary<string, object> { { "FileJson", pureJson } };
        await CloudSaveService.Instance.Data.Player.SaveAsync(data);
        //var test = await CloudCodeService.Instance.CallEndpointAsync("Test");
        //    UnitSaveObject Attempt = JsonUtility.FromJson<UnitSaveObject>(test) ;
        //    print(test);
    }  
    public async void Load()
    {

        var test = await CloudSaveService.Instance.Data.Player.LoadAsync(new HashSet<string> { "FileJson" });
        string stringData = test["FileJson"].Value.GetAsString();
        //jSonOutput.text = stringData;
        int endIndex = stringData.Length - (4 + 0);
        string data = stringData;
        char[] characters = data.ToCharArray();
        List<char> list = new List<char>();
        foreach (char item in characters)
        {
            if (item == '\\')
            {

            }
            else
            {
                list.Add(item);

            }
        }
        char[] listAsArr = list.ToArray();
        string pureJson = new string(listAsArr);
        print("List:" + pureJson);
        var Attempt = JsonConvert.DeserializeObject<SaveFileOBJ>(pureJson);
        foreach(var unit in Attempt.SaveUnits)
        {
          GameObject create = Instantiate(UnitPrefabs.Find(x => x.GetComponent<UnitUnpackager>().Indentifier == unit.unitType), new Vector3(unit.realPosition.x, unit.realPosition.y, unit.realPosition.z), Quaternion.identity);
            create.transform.position = new Vector3(unit.realPosition.x, unit.realPosition.y, unit.realPosition.z) ;
            create.GetComponent<UnitUnpackager>().Load(unit.GetPos());
            create.GetComponent<Unit>().Health = unit.getHealth();
        }  
        foreach(var unit in Attempt.SaveBuildings)
        {
          GameObject create = Instantiate(UnitPrefabs.Find(x => x.GetComponent<BuildingUnpackager>().Indentifier == unit.unitType), new Vector3(unit.realPosition.x, unit.realPosition.y, unit.realPosition.z), Quaternion.identity);
            create.transform.position = new Vector3(unit.realPosition.x, unit.realPosition.y, unit.realPosition.z) ;
            create.GetComponent<BuildingUnpackager>().Load(null);
            create.GetComponent<Unit>().Health = unit.getHealth();
        }       
        foreach(var unit in Attempt.SaveUnits)
        {
          GameObject create = Instantiate(UnitPrefabs.Find(x => x.GetComponent<UnitUnpackager>().Indentifier == unit.unitType), new Vector3(unit.realPosition.x, unit.realPosition.y, unit.realPosition.z), Quaternion.identity);
            create.transform.position = new Vector3(unit.realPosition.x, unit.realPosition.y, unit.realPosition.z) ;
            create.GetComponent<UnitUnpackager>().Load(unit.GetPos());
            create.GetComponent<Unit>().Health = unit.getHealth();
        }
        player.GetComponent<Inventory>().keyValuePairs = Attempt.keyValuePairs;
        print(test);
    }
    private void Awake()
    {
        try
        {
            GameObject.Find("Button").GetComponent<Button>().onClick.AddListener(() =>
            {
                SaveUnitTypes();
            });
            GameObject.Find("Button (1)").GetComponent<Button>().onClick.AddListener(() =>
            {
                SaveBuildingTypes();
            });
            GameObject.Find("Button (2)").GetComponent<Button>().onClick.AddListener(() =>
            {
                SaveResourceTypes();
            });
        }
        catch (System.Exception test)
        {
            Debug.LogError("Error assigning Save Button Listeners + " + test);
        }
    }
    public void SaveUnitTypes()
    {
        foreach(Unit unit in Units)
        {
            UnitSavOBJ obj = new UnitSavOBJ();
            obj.Indentifier = unit.Indentifier;
            obj.IntervalBetweenFiring = unit.IntervalBetweenFiring;
            obj.nameAsString = unit.nameAsString;
            obj.MaxHealth = unit.MaxHealth;
            obj.moveSpeed = unit.moveSpeed;
            obj.Damage = unit.Damage;
            obj.Range = unit.Range;
            obj.Health = unit.Health;
            obj.Ability = unit.ability;
            UnitsAfter.Add(obj);
        }
        //UnitSaveObject a = new UnitSaveObject(player.GetComponent<SpriteRenderer>().color, player.transform.position);
        //a.color = new Color(1, 1, 1);
        string json = JsonConvert.SerializeObject(UnitsAfter);
        char[] characters = json.ToCharArray();
        List<char> list = new List<char>();
        foreach(char item in characters)
        {
            if(item == '"')
            {
                list.Add('\\');
                list.Add(item);
            }
            else
            {
                list.Add(item);
            }
        }
        char[] listAsArr = list.ToArray();
        string pureJson = new string(listAsArr);
        print(pureJson);
        output = pureJson;
        print("Input this into the database!");
    }
    public void SaveBuildingTypes()
    {
        //foreach(Building unit in Buildings)
        //{
        //    BuildingSaveOBJ obj = new BuildingSaveOBJ();
        //    obj.moveSpeed = unit.moveSpeed;
        //    obj.Health = unit.Health;
        //    obj._type = unit._type;
        //    BuildingsAfter.Add(obj);
        //}
        ////UnitSaveObject a = new UnitSaveObject(player.GetComponent<SpriteRenderer>().color, player.transform.position);
        ////a.color = new Color(1, 1, 1);
        //string json = JsonConvert.SerializeObject(BuildingsAfter);
        //char[] characters = json.ToCharArray();
        //List<char> list = new List<char>();
        //foreach(char item in characters)
        //{
        //    if(item == '"')
        //    {
        //        list.Add('\\');
        //        list.Add(item);
        //    }
        //    else
        //    {
        //        list.Add(item);
        //    }
        //}
        //char[] listAsArr = list.ToArray();
        //string pureJson = new string(listAsArr);
        //print(pureJson);
        //output = pureJson;
        //print("Input this into the database!");
    } 
    public void SaveResourceTypes()
    {
        foreach(ResourceDep unit in Resources)
        {
            //ResourceSaveOJB obj = new ResourceSaveOJB();
            //obj.MaxHealth = unit.MaxHealth;
            //obj.Amount = unit.Amount;
            //obj._Type = unit._Type;
            //ResourcesAFter.Add(obj);
        }
        //UnitSaveObject a = new UnitSaveObject(player.GetComponent<SpriteRenderer>().color, player.transform.position);
        //a.color = new Color(1, 1, 1);
        string json = JsonConvert.SerializeObject(ResourcesAFter);
        char[] characters = json.ToCharArray();
        List<char> list = new List<char>();
        foreach(char item in characters)
        {
            if(item == '"')
            {
                list.Add('\\');
                list.Add(item);
            }
            else
            {
                list.Add(item);
            }
        }
        char[] listAsArr = list.ToArray();
        string pureJson = new string(listAsArr);
        print(pureJson);
        output = pureJson;
        print("Input this into the database!");
    }
}
