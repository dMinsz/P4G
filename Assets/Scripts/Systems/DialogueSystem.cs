using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;

public class DialogueSystem : MonoBehaviour
{
    Dialogue dialog;

    private void Awake()
    {
        dialog = GameManager.Data.Dialog;
    }

    public void StartRutine()
    {
        StartCoroutine(DialogueRoutine());
    }
 

    IEnumerator DialogueRoutine() 
    {
        if (dialog.dialog_read(0) && !dialog.running)
        {
            IEnumerator dialog_co = dialog.dialog_system_start(0);
            StartCoroutine(dialog_co);
        }

        for (int i = 0; i < dialog.dialog_cycles[0].info.Count; i++) //��ȭ ������ ������� Ȯ��
        {

            yield return new WaitUntil(() =>
            {
                if (dialog.dialog_cycles[0].info[i].check_read)            //���� ��ȭ�� �о����� �ƴ���
                {
                    //Debug.Log("������");

                    //GameManager.Scene.LoadScene("LobbyScene");

                   return true;                                      
                }
                else
                {
                    //Debug.Log("���� �� �� ����");
                    return false;                                     
                }
            });
        }



        GameManager.Data.Dialog.IsVelvetRoom = false;
        GameManager.Data.Video.SetVideo(1, "LobbyScene");

        GameManager.Scene.LoadScene("VideoPlayScene");
    }
}
