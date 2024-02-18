using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.CloudSave;
using Unity.Services.Core;
using UnityEngine.UI;
using Unity.Services.CloudCode;
using Newtonsoft.Json;
public class SaveLoadManager : MonoBehaviour
{
    public GameObject player;
    public List<Unit> Units = new List<Unit>();
    public List<UnitSavOBJ> UnitsAfter = new List<UnitSavOBJ>();
    public List<Building> Buildings = new List<Building>();
    public List<BuildingSaveOBJ> BuildingsAfter = new List<BuildingSaveOBJ>();
    public string output;
    public List<ResourceDep> Resources = new List<ResourceDep>();
    public List<ResourceSaveOJB> ResourcesAFter = new List<ResourceSaveOJB>();

    public List<GameObject> UnitPrefabs = new List<GameObject>();
    public List<GameObject> BuildingPrefabs = new List<GameObject>();
    public List<GameObject> ResourcePRefabs = new List<GameObject>();
    public async void Save()
    {

        GameObject[] GameUnits;
        GameUnits = GameObject.FindGameObjectsWithTag("Unit");
        List<Building> Buildings = new List<Building>();
        List<ResourceDep> ResourceDeps = new List<ResourceDep>();
        List<UnitSaveFileOBJ> SaveUnits = new List<UnitSaveFileOBJ>();
        List<BuildingSaveFileOBJ> SaveBuildings = new List<BuildingSaveFileOBJ>();
        List<ResourceDep> SaveResourceDeps = new List<ResourceDep>();
        foreach (GameObject Obj in GameUnits)
        {
            Unit unit = Obj.GetComponent<Unit>();
            UnitSaveFileOBJ obj = new UnitSaveFileOBJ(unit.position, unit.Health, unit.GetComponent<Unit>().Indentifier, unit.transform.position);
            SaveUnits.Add(obj);
        } 
        //foreach (Building unit in Buildings)
        //{
        //    UnitSaveFileOBJ obj = new UnitSaveFileOBJ(unit.GetComponent<Pathing>().position, unit.Health);
 
        //} 
        //foreach (ResourceDep unit in ResourceDeps)
        //{
        //    UnitSaveFileOBJ obj = new UnitSaveFileOBJ(unit.GetComponent<Pathing>().position, unit.Health);
 
        //}
        SaveFileOBJ baseSaveFile = new SaveFileOBJ(SaveUnits, SaveBuildings, SaveResourceDeps);
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
          GameObject create = Instantiate(UnitPrefabs.Find(x => x.GetComponent<UnitUnpackager>().Indentifier == unit.unitType), unit.realPosition, Quaternion.identity);
            create.transform.position = unit.realPosition;
            create.GetComponent<UnitUnpackager>().Load(unit.GetPos());
            create.GetComponent<Unit>().Health = unit.getHealth();
        }
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
        foreach(Building unit in Buildings)
        {
            BuildingSaveOBJ obj = new BuildingSaveOBJ();
            obj.moveSpeed = unit.moveSpeed;
            obj.Health = unit.Health;
            obj._type = unit._type;
            BuildingsAfter.Add(obj);
        }
        //UnitSaveObject a = new UnitSaveObject(player.GetComponent<SpriteRenderer>().color, player.transform.position);
        //a.color = new Color(1, 1, 1);
        string json = JsonConvert.SerializeObject(BuildingsAfter);
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
    public void SaveResourceTypes()
    {
        foreach(ResourceDep unit in Resources)
        {
            ResourceSaveOJB obj = new ResourceSaveOJB();
            obj.MaxHealth = unit.MaxHealth;
            obj.Amount = unit.Amount;
            obj._Type = unit._Type;
            ResourcesAFter.Add(obj);
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
