using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BaseUI : MonoBehaviour
{
    protected Dictionary<string, RectTransform> transforms; //UI�� ���� �θ� ����

    protected Dictionary<string, Button> buttons;
    public Dictionary<string, TMP_Text> texts;
    public Dictionary<string, Slider> sliders;
    protected virtual void Awake()
    {
        BindChildren();
    }

    protected virtual void BindChildren()
    {
        transforms = new Dictionary<string, RectTransform>();
        buttons = new Dictionary<string, Button>();
        texts = new Dictionary<string, TMP_Text>();
        sliders = new Dictionary<string, Slider>();
        RectTransform[] children = GetComponentsInChildren<RectTransform>();
        // GetComponentsInChildren => baseUI �� �������� ���� ��� �ڽĵ��� �����´�
        // RectTransform�� UI�� ��� �ֱ⶧���� ��� ���� UI �ڽĵ��� childeren �� �־��ִ� �� �ȴ�.
        foreach (RectTransform child in children)
        {
            string key = child.gameObject.name; // ������Ʈ�� �̸��� Ű������ ����� ���̴�.

            if (transforms.ContainsKey(key))
                continue;

            transforms.Add(key, child);

            Button button = child.GetComponent<Button>();
            if (button != null)
                buttons.Add(key, button);

            TMP_Text text = child.GetComponent<TMP_Text>();
            if (text != null)
                texts.Add(key, text);

            Slider slider = child.GetComponent<Slider>();
            if (slider != null)
                sliders.Add(key, slider);
        }
    }

    public virtual void CloseUI()
    {

    }
}
