using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueInteract : MonoBehaviour
{
    public Transform checkUI;
    public Animator animator;
    public Transform Root;
    public int DialogueIndex;
    public string memberName;
    SphereCollider coll;

    Player player;

    private void Awake()
    {
        coll = GetComponent<SphereCollider>();
    }

    private void Start()
    {
        animator.Play("FallDown");
    }
    public void Interact()
    {
        StartCoroutine(DialoguePlay());
    }

    IEnumerator DialoguePlay()
    {
        checkUI.gameObject.SetActive(false);

        animator.SetTrigger("GetUp");

        var dialog = GameManager.Data.Dialog;
        dialog.system.StartRutine(DialogueIndex);

        player.GetComponent<PlayerInput>().enabled = false;

        for (int i = 0; i < dialog.dialog_cycles[DialogueIndex].info.Count; i++) //��ȭ ������ ������� Ȯ��
        {

            yield return new WaitUntil(() =>
            {
                if (dialog.dialog_cycles[DialogueIndex].info[i].check_read)            //���� ��ȭ�� �о����� �ƴ���
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

        //��Ƽ�� �߰�
        player.AddParty(memberName);


        var PlayerData = GameManager.Resource.Load<PlayerData>("Datas/Players/"+ memberName);



        int partyIndex = 0;

        if (player.Partys.Count == 1)
        {
            var newAllyName = player.Partys[0].data.unitName;
        }
        else 
        {
            partyIndex++;
            var newAllyName = player.Partys[partyIndex].data.unitName;
        }




        var newAlly = GameManager.Pool.Get(true, PlayerData.player.Prefab, this.transform.position, Quaternion.identity);

        newAlly.GetComponent<AllyMover>().ChacePos = player.GetComponent<PlayerMover>().ChacePoitns[partyIndex];


        var Ally = newAlly.GetComponent<Player>();

        Ally.GetComponent<CharacterController>().enabled = false;

        Ally.transform.position = Root.transform.position;

        Ally.GetComponent<CharacterController>().enabled = true;

        Ally.data = PlayerData.player;

        Ally.MaxHp = PlayerData.player.Hp;
        Ally.MaxSp = PlayerData.player.Sp;
        Ally.curHp = PlayerData.player.Hp;
        Ally.curSp = PlayerData.player.Sp;

        //�丣�ҳ� �߰�
        for (int j = 0; j < PlayerData.PersonaNames.Count; j++)
        {
            var personadata = GameManager.Resource.Load<PersonaData>("Datas/Personas/" + PlayerData.PersonaNames[j]);
            Ally.Personas.Add(personadata.PData.prefabs.GetComponent<BattlePersona>());
            Ally.Personas[j].data = personadata.PData;
        }

        Destroy(Ally.transform.Find("DialogueInteract").gameObject); // �ʿ���°� ����
       


        GameManager.Data.Dungeon.InBattlePlayers.Add(Ally);

       
        player.GetComponent<PlayerInput>().enabled = true;
        Destroy(Root.gameObject);

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            checkUI.gameObject.SetActive(true);
            player = other.gameObject.GetComponent<Player>();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            Root.transform.LookAt(other.transform.position);
            this.transform.LookAt(other.transform.position);
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
