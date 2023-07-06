using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHiter : MonoBehaviour, IHitable
{
    Coroutine SceneChangeRoutine;
    public CinemachineVirtualCamera Cam;
    Animator animator;

    private void Awake()
    {
        animator = transform.Find("Model").GetComponent<Animator>();
    }

    public void TakeHit(GameObject attacker)
    {
        // 배틀씬으로 이동한다.

        transform.LookAt(attacker.transform);

        Debug.Log("Symbol Take Hit , Go to Battle");

        GameManager.Data.Dungeon.tempSymbolShadows = attacker.GetComponent<Symbols>().hasEnemys;

        GameManager.Data.Dungeon.SetGoToBattle(DungeonDataSystem.Turn.Enemy, attacker);


        //foreach (var symbol in GameManager.Data.Dungeon.aliveInDungeonSymbols)
        //{
        //    symbol.GetComponent<Symbols>().ReleasePool();
        //}

        //foreach (var player in GameManager.Data.Dungeon.InBattlePlayers)
        //{
        //    GameManager.Pool.Release(player);
        //}


        SceneChangeRoutine = StartCoroutine(GotoBattleScene());


    }


    IEnumerator GotoBattleScene() 
    {
        animator.SetTrigger("Hit");
        transform.GetComponent<PlayerInput>().enabled = false;
        Cam.m_Lens.FieldOfView = 30;
        yield return new WaitForSeconds(0.5f);
        Cam.m_Lens.FieldOfView = 60;
        transform.GetComponent<PlayerInput>().enabled = true;
        GameManager.Scene.LoadScene("BattleScene");

        yield break;

    }

}
