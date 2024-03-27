using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.CloudSave;
using Unity.Services.Core;
using UnityEngine.UI;
using Unity.Services.CloudCode;
using Newtonsoft.Json;
using TMPro;
using UnityEngine.SceneManagement;

public enum SaveTypes
{
    Default,
    SaveFile,
}
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
    public Button SaveButton;

    public SaveTypes WhichLoad = SaveTypes.Default;

    public static SaveLoadManager instance;

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
    public  void Load(SaveTypes Default)
    {
        WhichLoad = Default;
    }
    void Creation(SaveFileOBJ Attempt)
    {
        foreach (var unit in Attempt.SaveUnits)
        {
            GameObject create = Instantiate(UnitPrefabs.Find(x => x.GetComponent<UnitUnpackager>().Indentifier == unit.unitType), new Vector3(unit.realPosition.x, unit.realPosition.y, unit.realPosition.z), Quaternion.identity);
            create.transform.position = new Vector3(unit.realPosition.x, unit.realPosition.y, unit.realPosition.z);
            create.GetComponent<UnitUnpackager>().Load(unit.GetPos());
            create.GetComponent<Unit>().Health = unit.getHealth();
        }
        foreach (var unit in Attempt.SaveBuildings)
        {
            GameObject create = Instantiate(BuildingPrefabs.Find(x => x.GetComponent<BuildingUnpackager>().Indentifier == unit.unitType), new Vector3(unit.realPosition.x, unit.realPosition.y, unit.realPosition.z), Quaternion.identity);
            create.transform.position = new Vector3(unit.realPosition.x, unit.realPosition.y, unit.realPosition.z);
            create.GetComponent<BuildingUnpackager>().Load(null);
            create.GetComponent<Building>().Health = unit.getHealth();
        }
        foreach (var unit in Attempt.SaveResourceDeps)
        {
            GameObject create = Instantiate(ResourcePRefabs.Find(x => x.GetComponent<ResourceUnpacker>().Indentifier == unit._Type), new Vector3(unit.Position.x, unit.Position.y, unit.Position.z), Quaternion.identity);
            create.transform.position = new Vector3(unit.Position.x, unit.Position.y, unit.Position.z);
            create.GetComponent<ResourceUnpacker>().Load(player.GetComponent<PlayerController>(), null);
            create.GetComponent<ResourceDep>().Health = unit.MaxHealth;
        }
        player.GetComponent<Inventory>().keyValuePairs = Attempt.keyValuePairs;
    }

public async void ChangedActiveScene(Scene current, Scene next)
{
    string currentName = current.name;

    if (currentName == null)
    {
        // Scene1 has been removed
        currentName = "Replaced";
    }

    Debug.Log("Scenes: " + currentName + ", " + next.name);
        player = GameObject.Find("Player1");
        player.GetComponent<PlayerController>().saveLoadManager = this;
        player.GetComponent<PlayerController>().SaveButton.onClick.AddListener(() =>
        {
            Save();
        });
        SaveFileOBJ Attempt = null;
        if (WhichLoad == SaveTypes.Default)
        {
            string pureJson = "{\\\"SaveUnits\\\":[{\\\"realPosition\\\":{\\\"x\\\":6.55,\\\"y\\\":12.95,\\\"z\\\":0.0},\\\"unitType\\\":4},{\\\"realPosition\\\":{\\\"x\\\":7.05,\\\"y\\\":13.86,\\\"z\\\":0.0},\\\"unitType\\\":0},{\\\"realPosition\\\":{\\\"x\\\":2.452,\\\"y\\\":11.29,\\\"z\\\":0.0},\\\"unitType\\\":4}],\\\"SaveBuildings\\\":[{\\\"realPosition\\\":{\\\"x\\\":2.452,\\\"y\\\":11.29,\\\"z\\\":0.0},\\\"unitType\\\":0,\\\"moveSpeed\\\":5.0,\\\"isEnemy\\\":false,\\\"FireInterval\\\":10.0,\\\"Manufacturing\\\":4}],\\\"SaveResourceDeps\\\":[{\\\"Amount\\\":50.0,\\\"MaxHealth\\\":5.0,\\\"_Type\\\":0,\\\"Position\\\":{\\\"x\\\":1.01,\\\"y\\\":6.88,\\\"z\\\":0.0439908132},\\\"ArrayPosition\\\":null},{\\\"Amount\\\":20.0,\\\"MaxHealth\\\":5.0,\\\"_Type\\\":1,\\\"Position\\\":{\\\"x\\\":41.48,\\\"y\\\":0.86,\\\"z\\\":0.0439908132},\\\"ArrayPosition\\\":null},{\\\"Amount\\\":50.0,\\\"MaxHealth\\\":5.0,\\\"_Type\\\":0,\\\"Position\\\":{\\\"x\\\":23.54,\\\"y\\\":6.05,\\\"z\\\":0.0439908132},\\\"ArrayPosition\\\":null},{\\\"Amount\\\":50.0,\\\"MaxHealth\\\":5.0,\\\"_Type\\\":0,\\\"Position\\\":{\\\"x\\\":8.0,\\\"y\\\":12.17,\\\"z\\\":0.0439908132},\\\"ArrayPosition\\\":null},{\\\"Amount\\\":20.0,\\\"MaxHealth\\\":5.0,\\\"_Type\\\":2,\\\"Position\\\":{\\\"x\\\":16.07,\\\"y\\\":15.64,\\\"z\\\":0.0439908132},\\\"ArrayPosition\\\":null},{\\\"Amount\\\":20.0,\\\"MaxHealth\\\":5.0,\\\"_Type\\\":2,\\\"Position\\\":{\\\"x\\\":37.09,\\\"y\\\":15.64,\\\"z\\\":0.0439908132},\\\"ArrayPosition\\\":null},{\\\"Amount\\\":50.0,\\\"MaxHealth\\\":5.0,\\\"_Type\\\":0,\\\"Position\\\":{\\\"x\\\":2.05,\\\"y\\\":6.91,\\\"z\\\":0.0439908132},\\\"ArrayPosition\\\":null},{\\\"Amount\\\":20.0,\\\"MaxHealth\\\":5.0,\\\"_Type\\\":1,\\\"Position\\\":{\\\"x\\\":47.07,\\\"y\\\":15.6,\\\"z\\\":0.0439908132},\\\"ArrayPosition\\\":null},{\\\"Amount\\\":50.0,\\\"MaxHealth\\\":5.0,\\\"_Type\\\":3,\\\"Position\\\":{\\\"x\\\":29.5,\\\"y\\\":0.92,\\\"z\\\":0.0439908132},\\\"ArrayPosition\\\":null},{\\\"Amount\\\":20.0,\\\"MaxHealth\\\":5.0,\\\"_Type\\\":2,\\\"Position\\\":{\\\"x\\\":14.02,\\\"y\\\":15.61,\\\"z\\\":0.0439908132},\\\"ArrayPosition\\\":null},{\\\"Amount\\\":50.0,\\\"MaxHealth\\\":5.0,\\\"_Type\\\":0,\\\"Position\\\":{\\\"x\\\":24.06,\\\"y\\\":5.19,\\\"z\\\":0.0439908132},\\\"ArrayPosition\\\":null},{\\\"Amount\\\":20.0,\\\"MaxHealth\\\":5.0,\\\"_Type\\\":1,\\\"Position\\\":{\\\"x\\\":42.04,\\\"y\\\":-0.03,\\\"z\\\":0.0439908132},\\\"ArrayPosition\\\":null},{\\\"Amount\\\":50.0,\\\"MaxHealth\\\":5.0,\\\"_Type\\\":3,\\\"Position\\\":{\\\"x\\\":42.03,\\\"y\\\":15.59,\\\"z\\\":0.0439908132},\\\"ArrayPosition\\\":null},{\\\"Amount\\\":50.0,\\\"MaxHealth\\\":5.0,\\\"_Type\\\":3,\\\"Position\\\":{\\\"x\\\":25.05,\\\"y\\\":3.46,\\\"z\\\":0.0439908132},\\\"ArrayPosition\\\":null},{\\\"Amount\\\":20.0,\\\"MaxHealth\\\":5.0,\\\"_Type\\\":2,\\\"Position\\\":{\\\"x\\\":-0.02,\\\"y\\\":0.03,\\\"z\\\":0.0439908132},\\\"ArrayPosition\\\":null},{\\\"Amount\\\":50.0,\\\"MaxHealth\\\":5.0,\\\"_Type\\\":0,\\\"Position\\\":{\\\"x\\\":5.06,\\\"y\\\":0.0,\\\"z\\\":0.0439908132},\\\"ArrayPosition\\\":null},{\\\"Amount\\\":20.0,\\\"MaxHealth\\\":5.0,\\\"_Type\\\":2,\\\"Position\\\":{\\\"x\\\":9.03,\\\"y\\\":12.19,\\\"z\\\":0.0439908132},\\\"ArrayPosition\\\":null},{\\\"Amount\\\":50.0,\\\"MaxHealth\\\":5.0,\\\"_Type\\\":0,\\\"Position\\\":{\\\"x\\\":13.04,\\\"y\\\":3.54,\\\"z\\\":0.0439908132},\\\"ArrayPosition\\\":null},{\\\"Amount\\\":50.0,\\\"MaxHealth\\\":5.0,\\\"_Type\\\":0,\\\"Position\\\":{\\\"x\\\":34.56,\\\"y\\\":11.23,\\\"z\\\":0.0439908132},\\\"ArrayPosition\\\":null},{\\\"Amount\\\":50.0,\\\"MaxHealth\\\":5.0,\\\"_Type\\\":0,\\\"Position\\\":{\\\"x\\\":12.0,\\\"y\\\":3.51,\\\"z\\\":0.0439908132},\\\"ArrayPosition\\\":null},{\\\"Amount\\\":50.0,\\\"MaxHealth\\\":5.0,\\\"_Type\\\":0,\\\"Position\\\":{\\\"x\\\":33.56,\\\"y\\\":11.23,\\\"z\\\":0.0439908132},\\\"ArrayPosition\\\":null},{\\\"Amount\\\":20.0,\\\"MaxHealth\\\":5.0,\\\"_Type\\\":1,\\\"Position\\\":{\\\"x\\\":46.55,\\\"y\\\":14.71,\\\"z\\\":0.0439908132},\\\"ArrayPosition\\\":null},{\\\"Amount\\\":50.0,\\\"MaxHealth\\\":5.0,\\\"_Type\\\":0,\\\"Position\\\":{\\\"x\\\":10.02,\\\"y\\\":6.94,\\\"z\\\":0.0439908132},\\\"ArrayPosition\\\":null},{\\\"Amount\\\":50.0,\\\"MaxHealth\\\":5.0,\\\"_Type\\\":3,\\\"Position\\\":{\\\"x\\\":25.09,\\\"y\\\":1.73,\\\"z\\\":0.0439908132},\\\"ArrayPosition\\\":null},{\\\"Amount\\\":50.0,\\\"MaxHealth\\\":5.0,\\\"_Type\\\":3,\\\"Position\\\":{\\\"x\\\":42.99,\\\"y\\\":3.46,\\\"z\\\":0.0439908132},\\\"ArrayPosition\\\":null},{\\\"Amount\\\":50.0,\\\"MaxHealth\\\":5.0,\\\"_Type\\\":3,\\\"Position\\\":{\\\"x\\\":41.48,\\\"y\\\":14.71,\\\"z\\\":0.0439908132},\\\"ArrayPosition\\\":null},{\\\"Amount\\\":50.0,\\\"MaxHealth\\\":5.0,\\\"_Type\\\":3,\\\"Position\\\":{\\\"x\\\":33.06,\\\"y\\\":1.73,\\\"z\\\":0.0439908132},\\\"ArrayPosition\\\":null},{\\\"Amount\\\":50.0,\\\"MaxHealth\\\":5.0,\\\"_Type\\\":3,\\\"Position\\\":{\\\"x\\\":43.54,\\\"y\\\":4.38,\\\"z\\\":0.0439908132},\\\"ArrayPosition\\\":null},{\\\"Amount\\\":20.0,\\\"MaxHealth\\\":5.0,\\\"_Type\\\":2,\\\"Position\\\":{\\\"x\\\":30.08,\\\"y\\\":13.93,\\\"z\\\":0.0439908132},\\\"ArrayPosition\\\":null},{\\\"Amount\\\":20.0,\\\"MaxHealth\\\":5.0,\\\"_Type\\\":2,\\\"Position\\\":{\\\"x\\\":30.55,\\\"y\\\":12.94,\\\"z\\\":0.0439908132},\\\"ArrayPosition\\\":null},{\\\"Amount\\\":20.0,\\\"MaxHealth\\\":5.0,\\\"_Type\\\":2,\\\"Position\\\":{\\\"x\\\":35.01,\\\"y\\\":15.64,\\\"z\\\":0.0439908132},\\\"ArrayPosition\\\":null},{\\\"Amount\\\":50.0,\\\"MaxHealth\\\":5.0,\\\"_Type\\\":0,\\\"Position\\\":{\\\"x\\\":8.58,\\\"y\\\":13.03,\\\"z\\\":0.0439908132},\\\"ArrayPosition\\\":null},{\\\"Amount\\\":20.0,\\\"MaxHealth\\\":5.0,\\\"_Type\\\":2,\\\"Position\\\":{\\\"x\\\":2.03,\\\"y\\\":0.03,\\\"z\\\":0.0439908132},\\\"ArrayPosition\\\":null},{\\\"Amount\\\":50.0,\\\"MaxHealth\\\":5.0,\\\"_Type\\\":0,\\\"Position\\\":{\\\"x\\\":9.53,\\\"y\\\":7.8,\\\"z\\\":0.0439908132},\\\"ArrayPosition\\\":null},{\\\"Amount\\\":50.0,\\\"MaxHealth\\\":5.0,\\\"_Type\\\":0,\\\"Position\\\":{\\\"x\\\":6.02,\\\"y\\\":0.0,\\\"z\\\":0.0439908132},\\\"ArrayPosition\\\":null},{\\\"Amount\\\":50.0,\\\"MaxHealth\\\":5.0,\\\"_Type\\\":3,\\\"Position\\\":{\\\"x\\\":-0.4595794,\\\"y\\\":0.843643248,\\\"z\\\":0.0439908132},\\\"ArrayPosition\\\":null},{\\\"Amount\\\":50.0,\\\"MaxHealth\\\":5.0,\\\"_Type\\\":3,\\\"Position\\\":{\\\"x\\\":30.01,\\\"y\\\":0.04,\\\"z\\\":0.0439908132},\\\"ArrayPosition\\\":null},{\\\"Amount\\\":50.0,\\\"MaxHealth\\\":5.0,\\\"_Type\\\":0,\\\"Position\\\":{\\\"x\\\":21.07,\\\"y\\\":0.01,\\\"z\\\":0.0439908132},\\\"ArrayPosition\\\":null}],\\\"keyValuePairs\\\":{\\\"Titanium\\\":0.0,\\\"Ruby\\\":0.0,\\\"Silver\\\":0.0,\\\"Gold\\\":0.0}}";
            int endIndex = pureJson.Length - (4 + 0);
            string data = pureJson;
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
            string NewData = new string(listAsArr);
            print("List:" + pureJson);
            Attempt = Newtonsoft.Json.JsonConvert.DeserializeObject<SaveFileOBJ>(NewData);
        }
        else
        {
            Dictionary<string, Unity.Services.CloudSave.Models.Item> test = await CloudSaveService.Instance.Data.Player.LoadAsync(new HashSet<string> { "FileJson" });
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
            Attempt = JsonConvert.DeserializeObject<SaveFileOBJ>(pureJson);
        }

        Creation(Attempt);
    }
private void Awake()
    {
        SceneManager.activeSceneChanged += ChangedActiveScene;
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance.gameObject);
            instance = this;
        }
        try
        {
          SaveButton.onClick.AddListener(() =>
            {
                //Load();
            });
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
    public void Start()
    {
        DontDestroyOnLoad(this);
       
        //Load();
    }
}
