using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BarText : MonoBehaviour
{
    public enum BarType
    {
        HP,
        SP,
        Size
    }

    public Player player;
    public BarType type;
    public TMP_Text text;


    private void Awake()
    {
        text = GetComponent<TMP_Text>();
    }

    // Start is called before the first frame update
    void Start()
    {
        SetText();
    }
    public void SetText()
    {
        if (type == BarType.HP)
        {
            text.text = player.curHp.ToString();
            player.OnHpChanged.AddListener(SetValue);

        }
        else if (type == BarType.SP)
        {
            text.text = player.curSp.ToString();
            player.OnSpChanged.AddListener(SetValue);
        }
    }
    public void SetValue(int value)
    {
        text.text = value.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
