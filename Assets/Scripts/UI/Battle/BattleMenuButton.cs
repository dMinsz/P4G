using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BattleMenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TMP_Text text;
    public Image back;
    private void Awake()
    {
        if (text == null)
            text = GetComponentInChildren<TMP_Text>();
        if (back == null)
            back = GetComponentInChildren<Image>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        text.fontSize = 35;
        text.color = Color.white;
        back.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        text.fontSize = 30;
        text.color = Color.black;
        back.gameObject.SetActive(false);
    }
}
