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
        dialog.system.StartRutine(2);
        player.GetComponent<PlayerInput>().enabled = false;
        for (int i = 0; i < dialog.dialog_cycles[2].info.Count; i++) //대화 단위를 순서대로 확인
        {

            yield return new WaitUntil(() =>
            {
                if (dialog.dialog_cycles[2].info[i].check_read)            //현재 대화를 읽었는지 아닌지
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

        //파티에 추가
        player.AddParty("Chie");


        var PlayerData = GameManager.Resource.Load<PlayerData>("Datas/Players/Chie");

        var newAllyName = player.Partys[0].data.unitName;


        var newAlly = GameManager.Pool.Get(true, PlayerData.player.Prefab, this.transform.position, Quaternion.identity);

        newAlly.GetComponent<AllyMover>().ChacePos = player.GetComponent<PlayerMover>().ChacePoitns[0];

        var Ally = newAlly.GetComponent<Player>();

        Ally.data = PlayerData.player;

        Ally.MaxHp = PlayerData.player.Hp;
        Ally.MaxSp = PlayerData.player.Sp;
        Ally.curHp = PlayerData.player.Hp;
        Ally.curSp = PlayerData.player.Sp;

        //페르소나 추가
        for (int j = 0; j < PlayerData.PersonaNames.Count; j++)
        {
            var personadata = GameManager.Resource.Load<PersonaData>("Datas/Personas/" + PlayerData.PersonaNames[j]);
            Ally.Personas.Add(personadata.PData.prefabs.GetComponent<BattlePersona>());
            Ally.Personas[j].data = personadata.PData;
        }

        Destroy(Ally.transform.Find("DialogueInteract").GetComponent<DialogueInteract>()); // 필요없는거 삭제
        Destroy(Ally.transform.Find("DialogueInteract").GetComponent<SphereCollider>());

        Ally.transform.position = Root.transform.position;

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
