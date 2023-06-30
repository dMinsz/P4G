using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static Skill;

public class SkillItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler ,IPointerClickHandler
{
    public TMP_Text skillName;
    public TMP_Text Cost;
    public TMP_Text CostType;
    [HideInInspector]public TMP_Text[] texts;
    public Image Icon;
    public Image back;

    public UnityEvent OnClick = new UnityEvent();

    public ResType attackRes;
    public TargetType targetType;
    public int skillPower;
    public int skillCri;
    public int skillHit;


    private void Awake()
    {
        texts = new TMP_Text[3];

        texts[0] = skillName;
        texts[1] = Cost;
        texts[2] = CostType;
        if (back == null)
            back = GetComponentInChildren<Image>();
    }

    private void OnEnable()
    {
        Icon.color = Color.white;

        foreach (var text in texts)
        {
            text.color = Color.white;
        }
        back.gameObject.SetActive(false);
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        Icon.color = Color.black;

        foreach (var text in texts) 
        {
            text.color = Color.black;
        }

        back.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Icon.color = Color.white;

        foreach (var text in texts)
        {
            text.color = Color.white;
        }
        back.gameObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        GameManager.Data.Battle.resType = attackRes;
        GameManager.Data.Battle.targetType = targetType;
        GameManager.Data.Battle.skillPower = skillPower;
        GameManager.Data.Battle.skillCri = skillCri;
        GameManager.Data.Battle.skillHit = skillHit;

        OnClick?.Invoke();
        ChangePlayerStatus();
    }


    public void ChangePlayerStatus()
    {
        if (CostType.text == "HP")
        {
            GameManager.Data.Battle.nowPlayer.HP -= int.Parse(Cost.text);
        }
        else if (CostType.text == "SP") 
        {
            GameManager.Data.Battle.nowPlayer.SP -= int.Parse(Cost.text);
        }
    }
}
