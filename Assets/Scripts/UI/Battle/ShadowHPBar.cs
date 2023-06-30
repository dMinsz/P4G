using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ShadowHPBar : InGameUI
{
    

    public Shadow shadow;
    public TMP_Text DamageText;
    private Slider slider;

    protected override void Awake()
    {
        slider = transform.Find("HpSlider").GetComponent<Slider>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //SetBar();
    }

    public void SetBar()
    {

        slider.maxValue = shadow.MaxHp;
        slider.value = shadow.curHp;
        shadow.OnHpChanged.AddListener(SetValue);
    }

    public void SetDamage(int value) 
    {
        DamageText.text = value.ToString();
    }
    public void SetValue(int value)
    {
        slider.value = value;
    }
}
