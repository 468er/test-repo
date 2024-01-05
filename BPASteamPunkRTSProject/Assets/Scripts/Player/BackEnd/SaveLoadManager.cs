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
    public string output;
    //public  async void Save()
    //{
    //    foreach(Unit unit in Units)
    //    {
    //        UnitSavOBJ obj = new UnitSavOBJ();
    //        obj.name = unit.name;
    //        obj.MaxHealth = unit.MaxHealth;
    //        obj.moveSpeed = unit.moveSpeed;
    //        obj.Damage = unit.Damage;
    //        obj.Range = unit.Range;
    //        obj.Health = unit.Health;
    //        obj._Type = unit._Type;
    //        UnitsAfter.Add(obj);
    //    }
    //    //UnitSaveObject a = new UnitSaveObject(player.GetComponent<SpriteRenderer>().color, player.transform.position);
    //    //a.color = new Color(1, 1, 1);
    //    string json = JsonUtility.ToJson(UnitsAfter);
    //    print(json);
    //    var data = new Dictionary<string, object> { { "Units", json } };
    //    await CloudSaveService.Instance.Data.Player.SaveAsync(data);
    //    var test = await CloudCodeService.Instance.CallEndpointAsync("Test");
    //    UnitSaveObject Attempt = JsonUtility.FromJson<UnitSaveObject>(test) ;
    //    print(test);
    //}    
    public void SaveUnitTypes()
    {
        foreach(Unit unit in Units)
        {
            UnitSavOBJ obj = new UnitSavOBJ();
            obj.name = unit.name;
            obj.MaxHealth = unit.MaxHealth;
            obj.moveSpeed = unit.moveSpeed;
            obj.Damage = unit.Damage;
            obj.Range = unit.Range;
            obj.Health = unit.Health;
            obj._Type = unit._Type;
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
}
