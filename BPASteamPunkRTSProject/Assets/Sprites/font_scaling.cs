using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class font_scaling : MonoBehaviour
{
    public int defaultwidth = 1334;
    public int current_width;
    public decimal font_multiplier;
    public double font_mutli2;
    public int fontsize;
    public bool is_tmp;
    //tmp bools and stuff
    public float fontsize2;
    public double temp;
    // Start is called before the first frame update
    public void Start()
    {
        if(is_tmp != true)
        {
            current_width = Screen.width;
            font_multiplier = decimal.Divide(current_width, defaultwidth);
            font_mutli2 = (double)font_multiplier;
            fontsize = this.gameObject.GetComponent<Text>().fontSize;
            temp = fontsize;
            fontsize = (int)(temp * font_mutli2);
            this.gameObject.GetComponent<Text>().fontSize = fontsize;
        }
        else
        {
            float temp;
            current_width = Screen.width;
            font_multiplier = decimal.Divide(current_width, defaultwidth);
            font_mutli2 = (double)font_multiplier;
            fontsize2 = this.gameObject.GetComponent<TextMeshProUGUI>().fontSize;
            temp = fontsize2;
            fontsize2 = (float)(temp * font_mutli2);
            this.gameObject.GetComponent<TextMeshProUGUI>().fontSize = fontsize2;
        }
    }
}
