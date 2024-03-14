using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.CloudCode;
using Newtonsoft.Json;
using TMPro;
 public class BuildingUnpackager : MonoBehaviour
{
    public BuildingIdentity Indentifier;
    public bool loadFromDatabase;
    // Start is called before the first frame update
    public async void Load(TMP_InputField jSonOutput)
    {
        if (loadFromDatabase)
        {
            var test = await CloudCodeService.Instance.CallEndpointAsync("Buildings");

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
            var Attempt = JsonConvert.DeserializeObject<List<BuildingSaveOBJ>>(pureJson);
            foreach (BuildingSaveOBJ obj in Attempt)
            {
                if (Indentifier == obj.building)
                {
                    Building OldUnit = gameObject.GetComponent<Building>();
                    if (OldUnit != null)
                    {
                        Destroy(OldUnit);
                    }
                    Building unit = gameObject.AddComponent<Building>();
                    unit.moveSpeed = obj.moveSpeed;
                    unit.Health = obj.Health;
                    unit._type = obj._type;
}
            }
        }
        else
        {
            GetComponent<Building>().enabled = true;
        }
    }


}
