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

    public void StartRutine(int index)
    {
        StartCoroutine(DialogueRoutine(index));
    }
 

    IEnumerator DialogueRoutine(int index) 
    {
        if (dialog.dialog_read(index) && !dialog.running)
        {
            IEnumerator dialog_co = dialog.dialog_system_start(index);
            StartCoroutine(dialog_co);
        }

        for (int i = 0; i < dialog.dialog_cycles[index].info.Count; i++) //��ȭ ������ ������� Ȯ��
        {

            yield return new WaitUntil(() =>
            {
                if (dialog.dialog_cycles[index].info[i].check_read)            //���� ��ȭ�� �о����� �ƴ���
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


        if (GameManager.Data.Dialog.IsVelvetRoom)
        {
            GameManager.Data.Dialog.IsVelvetRoom = false;
            GameManager.Data.Video.SetVideo(1, "CutScene");

            GameManager.Scene.LoadScene("VideoPlayScene");
        }

    }
}
