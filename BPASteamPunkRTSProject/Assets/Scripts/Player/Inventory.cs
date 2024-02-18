using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public IDictionary<string, float> keyValuePairs = new Dictionary<string, float>();
    public List<PanelsWithOreType> InventoryItems = new List<PanelsWithOreType>();
    private void Start()
    {
        string[] names = Enum.GetNames(typeof(Resource_Type));
        int count = names.Length;
        
        for(int i = 0; i < count; i++)
        {
            keyValuePairs.Add(names[i], 0);
        }
    }
    public void Add(string Item, float Amount)
    {
        keyValuePairs[Item] += Amount;
        InventoryItems.Find(x => x.Type.ToString() == Item).Item.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = keyValuePairs[Item].ToString();
    } 
    public void Remove(string Item, float Amount)
    {
        if(keyValuePairs[Item] - Amount> 0)
        {
            keyValuePairs[Item] -= Amount;

        }
        else
        {
            throw new System.Exception("Error");
        }
    }
}
