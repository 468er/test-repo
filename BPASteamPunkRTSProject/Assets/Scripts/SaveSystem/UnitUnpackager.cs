using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.CloudCode;
using Newtonsoft.Json;

public class UnitUnpackager : MonoBehaviour
{
    public string name;
    // Start is called before the first frame update
    public async void Load()
    {
        var test = await CloudCodeService.Instance.CallEndpointAsync("Units");

        int startIndex = test.IndexOf(":") + 3;
        int endIndex = test.Length - (4 + startIndex);
        string data = test.Substring(startIndex, endIndex);
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
            var Attempt = JsonConvert.DeserializeObject<List<UnitSavOBJ>>(pureJson);
            foreach (UnitSavOBJ obj in Attempt)
            {
                if (name == obj.name)
                {
                    Unit unit = gameObject.AddComponent<Unit>();
                    unit.name = obj.name;
                    unit.Health = obj.Health;
                    unit.MaxHealth = obj.MaxHealth;
                    unit.Range = obj.Range;
                    unit.moveSpeed = obj.moveSpeed;
                    unit._Type = obj._Type;
                    unit.Damage = obj.Damage;
                }
            }
        }

    }

