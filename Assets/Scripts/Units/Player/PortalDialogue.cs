using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PortalDialogue : MonoBehaviour
{
    public Transform checkUI;
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

        if (GameManager.Data.Battle.InBattlePlayers.Count < 2)
        {


            var dialog = GameManager.Data.Dialog;

            dialog.dialog_obj.selectUI.gameObject.SetActive(true);

            var answer = dialog.dialog_obj.selectUI.GetComponent<DialogueAnswerUI>();

            Button yes = answer.yes;
            Button no = answer.no;

            yes.onClick.AddListener(AnswerYes);
            no.onClick.AddListener(AnswerNo);

            dialog.system.StartRutine(1);
            player.GetComponent<PlayerInput>().enabled = false;
            for (int i = 0; i < dialog.dialog_cycles[1].info.Count; i++) //��ȭ ������ ������� Ȯ��
            {

                yield return new WaitUntil(() =>
                {
                    if (dialog.dialog_cycles[1].info[i].check_read)            //���� ��ȭ�� �о����� �ƴ���
                    {
                        //Debug.Log("������");

                        dialog.dialog_obj.selectUI.gameObject.SetActive(false);
                        dialog.DisplayNext(0,1);

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
        //�������̵�
        //Debug.Log("���� ���� ������");

    }


    public void AnswerYes()
    {
        player.GetComponent<PlayerInput>().enabled = true;
        GameManager.Data.Dialog.dialog_obj.selectUI.gameObject.SetActive(false);
        Debug.Log("���� ���� ������");
    }

    public void AnswerNo()
    {
        Debug.Log("No Answer");
        player.GetComponent<PlayerInput>().enabled = true;
        GameManager.Data.Dialog.dialog_obj.selectUI.gameObject.SetActive(false);

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
