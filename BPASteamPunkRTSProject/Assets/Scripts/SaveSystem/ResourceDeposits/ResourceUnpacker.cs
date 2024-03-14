using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.CloudCode;
using Newtonsoft.Json;
using TMPro;
public class ResourceUnpacker : MonoBehaviour
{
    public Resource_Type Indentifier;
     bool loadFromDatabase = false;
    // Start is called before the first frame update
    public async void Load(PlayerController player, TMP_InputField jSonOutput)
    {
        if (loadFromDatabase)
        {
            var test = await CloudCodeService.Instance.CallEndpointAsync("ResourceDeps");
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
            var Attempt = JsonConvert.DeserializeObject<List<ResourceSaveOJB>>(pureJson);
            foreach (ResourceSaveOJB obj in Attempt)
            {
                if (Indentifier == obj._Type)
                {
                    ResourceDep OldUnit = gameObject.GetComponent<ResourceDep>();
                    if (OldUnit != null)
                    {
                        Destroy(OldUnit);
                    }
                    ResourceDep unit = gameObject.AddComponent<ResourceDep>();
                    unit.Amount = obj.Amount;
                    unit.MaxHealth = obj.MaxHealth;
                    unit._Type = obj._Type;
                  unit.Player1 = player;
                }
            }
        }
        GetComponent<ResourceDep>().Player1 = player;
    }

}
