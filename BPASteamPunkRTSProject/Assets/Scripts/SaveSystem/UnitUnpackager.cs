using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.CloudCode;
using Newtonsoft.Json;
using TMPro;
public class UnitUnpackager : MonoBehaviour
{
    public UnitType Indentifier;
    public bool loadFromDatabase;
    // Start is called before the first frame update
    public async void Load(TMP_InputField jSonOutput)
    {
        if (loadFromDatabase)
        {
            int[] position = GetComponent<Pathing>().position;
            var test = await CloudCodeService.Instance.CallEndpointAsync("Units");
            jSonOutput.text = test;

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
                if (Indentifier == obj.Indentifier)
                {
                    Unit OldUnit = gameObject.GetComponent<Unit>();
                    if (OldUnit != null)
                    {
                        Destroy(OldUnit);
                    }
                    Unit unit = gameObject.AddComponent<Unit>();
                    unit.IntervalBetweenFiring = obj.IntervalBetweenFiring;
                    unit.Indentifier = obj.Indentifier;
                    unit.nameAsString = obj.nameAsString;
                    unit.Health = obj.Health;
                    unit.MaxHealth = obj.MaxHealth;
                    unit.Range = obj.Range;
                    unit.moveSpeed = obj.moveSpeed;
                    unit.ability = obj.Ability;
                    unit.Damage = obj.Damage;

                    unit.position = position;
                }
            }
        }
      
            gameObject.GetComponent<Unit>().enabled = true;
       
    } 
    public async void Load(int[] Position)
    {
        if (loadFromDatabase)
        {
            int[] position = Position;
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
                if (Indentifier == obj.Indentifier)
                {
                    Unit OldUnit = gameObject.GetComponent<Unit>();
                    if (OldUnit != null)
                    {
                        Destroy(OldUnit);
                    }
                    Unit unit = gameObject.AddComponent<Unit>();
                    unit.IntervalBetweenFiring = obj.IntervalBetweenFiring;
                    unit.Indentifier = obj.Indentifier;
                    unit.nameAsString = obj.nameAsString;
                    unit.Health = obj.Health;
                    unit.MaxHealth = obj.MaxHealth;
                    unit.Range = obj.Range;
                    unit.moveSpeed = obj.moveSpeed;
                    unit.ability = obj.Ability;
                    unit.Damage = obj.Damage;

                    unit.position = position;
                }
            }
        }
      
            gameObject.GetComponent<Unit>().enabled = true;
       
    }
        

    }

