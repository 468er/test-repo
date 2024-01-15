using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FactoryDropDown : MonoBehaviour
{
    public TMP_Dropdown dropdown;
    private void Show()
    {
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        Destroy(this.gameObject);
    }
    public void Initialize(GameObject DD)
    {
        
       Building Factory = GetComponent<UIToWorldPointUpdater>().ParentTile.GetComponent<Building>();
        Factory.DD = DD;
        dropdown.SetValueWithoutNotify(Factory.DDValue);
        switch (Factory.DDValue)
        {
            case 0:
                Factory.Manufacturing = UnitType.Worker;
                break;
            case 1:
                Factory.Manufacturing = UnitType.Bomber;
                break;
            case 2:
                Factory.Manufacturing = UnitType.Scout;
                break;
            case 3:
                Factory.Manufacturing = UnitType.KillerAnt;
                break;
            case 4:
                Factory.Manufacturing = UnitType.Sniper;
                break;
            case 5:
                Factory.Manufacturing = UnitType.Ultralisk;
                break;
        }
    }  
    public void Try()
    {
       Building Factory = GetComponent<UIToWorldPointUpdater>().ParentTile.GetComponent<Building>();
        switch (dropdown.value)
        {
            case 0:
                Factory.Manufacturing = UnitType.Worker;
                break;
            case 1:
                Factory.Manufacturing = UnitType.Bomber;
                break;
            case 2:
                Factory.Manufacturing = UnitType.Scout;
                break;
            case 3:
                Factory.Manufacturing = UnitType.KillerAnt;
                break;
            case 4:
                Factory.Manufacturing = UnitType.Sniper;
                break;
            case 5:
                Factory.Manufacturing = UnitType.Ultralisk;
                break;
        }
        Factory.DDValue = dropdown.value;
    }
}
