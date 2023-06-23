using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SliderBar : MonoBehaviour
{
    public enum BarType
    {
        HP,
        SP,
        Size
    }

    public Player player;
    public BarType type;
    private Slider slider;
    private UnityEvent<int> OnValueChanged;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    // Start is called before the first frame update
    void Start()
    {
        SetBar();
    }

    public void SetBar()
    {
        if (type == BarType.HP)
        {
            slider.maxValue = player.MaxHp;
            slider.value = player.curHp;
            player.OnHpChanged.AddListener(SetValue);

        }
        else if (type == BarType.SP) 
        {
            slider.maxValue = player.MaxSp;
            slider.value = player.curSp;
            player.OnSpChanged.AddListener(SetValue);
        }
    }
    public void SetValue(int value)
    {
        slider.value = value;
    }

}
