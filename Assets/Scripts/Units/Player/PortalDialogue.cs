using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PortalDialogue : MonoBehaviour
{
    public Transform checkUI;
    public string FindPlayer;
    //public int CheckPlayer = 2;
    //public int dialoguIndex = 1;
    public string NextDungeon;

    BoxCollider coll;
    Player player;

    private void Awake()
    {
        coll = GetComponent<BoxCollider>();
    }


    public void Interact()
    {
        StartCoroutine(DialoguePlay());
    }
    IEnumerator DialoguePlay()
    {

        var isFind = FindPlayerInData(GameManager.Data.Dungeon.InBattlePlayers, FindPlayer);

        if (isFind == null) // �ش� �÷��̾ ��Ƽ ����� ã����������
        {
            var dialog = GameManager.Data.Dialog;

            dialog.dialog_obj.selectUI.gameObject.SetActive(true);

            var answer = dialog.dialog_obj.selectUI.GetComponent<DialogueAnswerUI>();

            Button yes = answer.yes;
            Button no = answer.no;

            yes.onClick.AddListener(AnswerYes);
            no.onClick.AddListener(AnswerNo);

            dialog.system.StartRutine(1); // ��Ż �����ִµ� �Ѿ�ųĴ� ���̾˷α�
            player.GetComponent<PlayerInput>().enabled = false;
            for (int i = 0; i < dialog.dialog_cycles[1].info.Count; i++) //��ȭ ������ ������� Ȯ��
            {

                yield return new WaitUntil(() =>
                {
                    if (dialog.dialog_cycles[1].info[i].check_read)            //���� ��ȭ�� �о����� �ƴ���
                    {
                        //Debug.Log("������");

                        dialog.dialog_obj.selectUI.gameObject.SetActive(false);
                        dialog.DisplayNext(0, 1);

                        dialog.ResetData();

                        return true;
                    }
                    else
                    {
                        //Debug.Log("���� �� �� ����");
                        return false;
                    }
                });
            }
            player.GetComponent<PlayerInput>().enabled = true;
            //dialog.dialog_obj.selectUI.gameObject.SetActive(false);


        }
        else 
        {
            var dialog = GameManager.Data.Dialog;

            dialog.dialog_obj.selectUI.gameObject.SetActive(true);

            var answer = dialog.dialog_obj.selectUI.GetComponent<DialogueAnswerUI>();

            Button yes = answer.yes;
            Button no = answer.no;

            yes.onClick.AddListener(AnswerYes);
            no.onClick.AddListener(AnswerNo);
            
            dialog.system.StartRutine(6); // �غ�ى�Ĵ� ���̾˷α�
            player.GetComponent<PlayerInput>().enabled = false;
            for (int i = 0; i < dialog.dialog_cycles[6].info.Count; i++) //��ȭ ������ ������� Ȯ��
            {

                yield return new WaitUntil(() =>
                {
                    if (dialog.dialog_cycles[6].info[i].check_read)            //���� ��ȭ�� �о����� �ƴ���
                    {
                        //Debug.Log("������");

                        dialog.dialog_obj.selectUI.gameObject.SetActive(false);
                        dialog.DisplayNext(0, 1);

                        dialog.ResetData();

                        return true;
                    }
                    else
                    {
                        //Debug.Log("���� �� �� ����");
                        return false;
                    }
                });
            }
            player.GetComponent<PlayerInput>().enabled = true;
            //dialog.dialog_obj.selectUI.gameObject.SetActive(false);

        }

        yield break;

    }

    private Unit FindPlayerInData(List<Unit> players , string name) 
    {

        foreach (var player in players) 
        {
            if (player.data.unitName == name) 
            {
                return player;
            }
        }

        return null;
    }


    public void AnswerYes()
    {
        //���̾˷α� ���ֱ�
        GameManager.Data.Dialog.ResetDialogue();


        //�̺�Ʈ ������ ����
        var answer = GameManager.Data.Dialog.dialog_obj.selectUI.GetComponent<DialogueAnswerUI>();
        Button yes = answer.yes;
        Button no = answer.no;

        yes.onClick.RemoveAllListeners();
        no.onClick.RemoveAllListeners();

        player.GetComponent<PlayerInput>().enabled = true;
        GameManager.Data.Dialog.dialog_obj.selectUI.gameObject.SetActive(false);

        Debug.Log("���� ���� ������ " + NextDungeon);
        //�������̵�

        GameManager.Scene.LoadScene(NextDungeon);
    }

    public void AnswerNo()
    {
        Debug.Log("No Answer");
        player.GetComponent<PlayerInput>().enabled = true;
        GameManager.Data.Dialog.dialog_obj.selectUI.gameObject.SetActive(false);

        //���̾˷α� ���ֱ�
        var index = GameManager.Data.Dialog.dialog_cycles[1].info.Count;
        GameManager.Data.Dialog.dialog_cycles[1].info[(index - 1)].check_read = true;

    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            checkUI.gameObject.SetActive(true);
            player = other.gameObject.GetComponent<Player>();
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            checkUI.gameObject.SetActive(false);
        }
    }


}
