using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Reflection;
using System;

public class TargetAttributeUI : InGameUI
{

    [Serializable]
    public class ResistUI
    {
        [SerializeField] public Transform uiPos;
        [SerializeField] public TMP_Text text;
    }

    public List<ResistUI> Resist;

    public RectTransform rootTransform;

    public void ResetData()
    {
        foreach (var item in Resist) 
        {
            item.text.text = "-";
            item.text.color = Color.white;
            item.uiPos.gameObject.SetActive(true);
        }
    }


    public void Setup(Resistances resist) 
    {
        int count = 1;
        var fields = typeof(Resistances).GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly);


        for (int i = 0; i < fields.Length; i++) 
        {
            if ((Resistance)fields[i].GetValue(resist) == Resistance.None)
            {
                Resist[i].uiPos.gameObject.SetActive(false);
            }
            else 
            {
                if ((Resistance)fields[i].GetValue(resist) == Resistance.Weak)
                {
                    Resist[i].text.text = "¡å";
                    Resist[i].text.color = Color.white;
                }
                else if ((Resistance)fields[i].GetValue(resist) == Resistance.Strength)
                {
                    Resist[i].text.text = "¡ã";
                    Resist[i].text.color = Color.red;
                }
                else if ((Resistance)fields[i].GetValue(resist) == Resistance.Absorb)
                {
                    Resist[i].text.text = "Èí";
                }
                else if ((Resistance)fields[i].GetValue(resist) == Resistance.Null)
                {
                    Resist[i].text.text = "¹«";
                }
                count++;
            }
        }

        
        rootTransform.sizeDelta = new Vector2(count*110, 80);

    }
    
}
