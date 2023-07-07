using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static PersonaData;

public class CutsceneDialogue : MonoBehaviour
{
    public Player player;
    public Transform bat;
    public Kuma kumaobj;
    public CinemachineVirtualCamera cam;
    public void Interact()
    {
        StartCoroutine(DialoguePlay());
    }

    IEnumerator DialoguePlay()
    {
        yield return new WaitForSeconds(2.0f);

        var dialog = GameManager.Data.Dialog;
        dialog.system.StartRutine(7);

        

        for (int i = 0; i < dialog.dialog_cycles[7].info.Count; i++) //��ȭ ������ ������� Ȯ��
        {

            yield return new WaitUntil(() =>
            {
                if (dialog.dialog_cycles[7].info[i].check_read)            //���� ��ȭ�� �о����� �ƴ���
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


        yield return new WaitForSeconds(0.1f);
        bat.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        kumaobj.animator.SetTrigger("Attack");
        yield return new WaitForSeconds(1.0f);
        kumaobj.attackAudio.Play();
        player.GetComponent<CharacterController>().Move(Vector3.forward*-1*5f);

        yield return new WaitForSeconds(0.1f);
        cam.Priority = 20;
        yield return new WaitForSeconds(0.1f);

       
        player.summonEffect.Play();
        player.card[0].gameObject.SetActive(true);
        player.card[1].gameObject.SetActive(true);

        player.animator.SetTrigger("Persona");
        yield return new WaitForSeconds(3.0f);

        player.summonEffect.Stop();
        player.card[0].gameObject.SetActive(false);
        player.card[1].gameObject.SetActive(false);
       

        var pobj = GameManager.Pool.Get(false, player.Personas[0], player.PersonaPoint.position, Quaternion.identity);
        pobj.transform.LookAt(kumaobj.transform);

        yield return new WaitForSeconds(1.0f);
        Destroy(pobj);
        cam.Priority = 1;


        dialog.system.StartRutine(8);



        for (int i = 0; i < dialog.dialog_cycles[8].info.Count; i++) //��ȭ ������ ������� Ȯ��
        {

            yield return new WaitUntil(() =>
            {
                if (dialog.dialog_cycles[8].info[i].check_read)            //���� ��ȭ�� �о����� �ƴ���
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

        //���̵�
        Debug.Log("���̵�");

        GameManager.Scene.LoadScene("LobbyScene");
    }
}
