using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Dungeon1f : BaseScene
{

    public DialogueSystem dialog;
    public PlayerGenerator playerGenerator;
    protected override void Awake()
    {
        Debug.Log("Dungeon 1F Scene Init");

        GameManager.Data.Dungeon.SetUp("1F");
        
        GameManager.Pool.Reset();
        GameManager.UI.Reset();

    }

    protected override IEnumerator LoadingRoutine()
    {
        GameManager.Data.Dialog.SetUp();
        GameManager.Data.Dialog.system = dialog;

        GameManager.Data.Dungeon.IsInit = false;

        playerGenerator.Init();

        if (GameManager.Data.Dungeon.didBattle)
        {
            GameManager.Data.Dungeon.IsSymbolInit = true;
        }
        else 
        {
            GameManager.Data.Dungeon.IsSymbolInit = false;
        }


        //yield return new WaitForSecondsRealtime(0.2f);
        //progress = 0.2f;
        //yield return new WaitForSecondsRealtime(0.2f);
        //progress = 0.4f;
        //yield return new WaitForSecondsRealtime(0.2f);
        //progress = 0.6f;
        //yield return new WaitForSecondsRealtime(0.2f);
        //progress = 0.8f;
        yield return new WaitForSecondsRealtime(0.2f);
        progress = 1.0f;
    }


    public override void Clear()
    {
        GameManager.Data.Dialog.ResetData();
        //다이알로그 릴리즈
        //GameManager.Pool.ReleaseUI(GameManager.Data.Dialog.dialog_obj);

        GameManager.Pool.erasePooDicContet(GameManager.Data.Dialog.dialog_obj.name);


        foreach (var player in GameManager.Data.Dungeon.InBattlePlayers) 
        {
            GameManager.Pool.Release(player);
        }

        foreach (var symbol in GameManager.Data.Dungeon.aliveInDungeonSymbols)
        {//심볼컨테이너 삭제
            GameManager.Pool.DestroyContainer(symbol);
        }
    }
}