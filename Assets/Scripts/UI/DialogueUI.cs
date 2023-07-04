using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueUI : BaseUI
{
    public Transform selectUI;
    public Transform answerArea;

    public Transform dialogueRoot;
    public Image bustUp;
    public Image Igor;
    public TMP_Text Contents;
    public TMP_Text dname;
    public Button NextButton;

    protected override void Awake()
    {
        BindChildren();
    }

    protected override void BindChildren()
    {
        transforms = new Dictionary<string, RectTransform>();
        buttons = new Dictionary<string, Button>();
        texts = new Dictionary<string, TMP_Text>();
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
        }
    }


}
