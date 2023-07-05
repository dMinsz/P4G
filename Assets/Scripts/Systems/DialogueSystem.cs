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

        for (int i = 0; i < dialog.dialog_cycles[index].info.Count; i++) //대화 단위를 순서대로 확인
        {

            yield return new WaitUntil(() =>
            {
                if (dialog.dialog_cycles[index].info[i].check_read)            //현재 대화를 읽었는지 아닌지
                {
                    //Debug.Log("다읽음");

                    //GameManager.Scene.LoadScene("LobbyScene");

                   return true;                                      
                }
                else
                {
                    //Debug.Log("아직 다 안 읽음");
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
