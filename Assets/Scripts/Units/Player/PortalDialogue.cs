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

        if (isFind == null) // 해당 플레이어를 파티 멤버로 찾지못했을때
        {
            var dialog = GameManager.Data.Dialog;

            dialog.dialog_obj.selectUI.gameObject.SetActive(true);

            var answer = dialog.dialog_obj.selectUI.GetComponent<DialogueAnswerUI>();

            Button yes = answer.yes;
            Button no = answer.no;

            yes.onClick.AddListener(AnswerYes);
            no.onClick.AddListener(AnswerNo);

            dialog.system.StartRutine(1); // 포탈 남아있는데 넘어갈거냐는 다이알로그
            player.GetComponent<PlayerInput>().enabled = false;
            for (int i = 0; i < dialog.dialog_cycles[1].info.Count; i++) //대화 단위를 순서대로 확인
            {

                yield return new WaitUntil(() =>
                {
                    if (dialog.dialog_cycles[1].info[i].check_read)            //현재 대화를 읽었는지 아닌지
                    {
                        //Debug.Log("다읽음");

                        dialog.dialog_obj.selectUI.gameObject.SetActive(false);
                        dialog.DisplayNext(0, 1);

                        dialog.ResetData();

                        return true;
                    }
                    else
                    {
                        //Debug.Log("아직 다 안 읽음");
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
            
            dialog.system.StartRutine(6); // 준비다됬냐는 다이알로그
            player.GetComponent<PlayerInput>().enabled = false;
            for (int i = 0; i < dialog.dialog_cycles[6].info.Count; i++) //대화 단위를 순서대로 확인
            {

                yield return new WaitUntil(() =>
                {
                    if (dialog.dialog_cycles[6].info[i].check_read)            //현재 대화를 읽었는지 아닌지
                    {
                        //Debug.Log("다읽음");

                        dialog.dialog_obj.selectUI.gameObject.SetActive(false);
                        dialog.DisplayNext(0, 1);

                        dialog.ResetData();

                        return true;
                    }
                    else
                    {
                        //Debug.Log("아직 다 안 읽음");
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
        //다이알로그 꺼주기
        GameManager.Data.Dialog.ResetDialogue();


        //이벤트 리스너 제거
        var answer = GameManager.Data.Dialog.dialog_obj.selectUI.GetComponent<DialogueAnswerUI>();
        Button yes = answer.yes;
        Button no = answer.no;

        yes.onClick.RemoveAllListeners();
        no.onClick.RemoveAllListeners();

        player.GetComponent<PlayerInput>().enabled = true;
        GameManager.Data.Dialog.dialog_obj.selectUI.gameObject.SetActive(false);

        Debug.Log("다음 던전 씬으로 " + NextDungeon);
        //다음씬이동

        GameManager.Scene.LoadScene(NextDungeon);
    }

    public void AnswerNo()
    {
        Debug.Log("No Answer");
        player.GetComponent<PlayerInput>().enabled = true;
        GameManager.Data.Dialog.dialog_obj.selectUI.gameObject.SetActive(false);

        //다이알로그 꺼주기
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
