using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Dungeon1f : BaseScene
{

    public DialogueSystem dialog;
    public PlayerGenerator playerGenerator;
    public SymbolGenerator symbolGenerator;
    protected override void Awake()
    {
        Debug.Log("Dungeon 1F Scene Init");

        GameManager.Data.Dungeon.SetUp("Dungeon1f");
        
        GameManager.Pool.Reset();
        GameManager.UI.Reset();

        if (!GameManager.Data.Dungeon.didBattle)
        {
            GameManager.Data.Dungeon.IsSymbolInit = true;
        }
        else 
        {
            GameManager.Data.Dungeon.IsSymbolInit = false;
        }

    }

    protected override IEnumerator LoadingRoutine()
    {
        GameManager.Data.Dialog.SetUp();
        GameManager.Data.Dialog.system = dialog;

        GameManager.Data.Dungeon.IsInit = false;
        yield return new WaitForSecondsRealtime(0.2f);
        progress = 0.6f;

        playerGenerator.Init();
        yield return new WaitForSecondsRealtime(0.2f);
        progress = 0.8f;

        yield return new WaitForSecondsRealtime(0.2f);
        progress = 1.0f;

        GameManager.Data.BattleBackGround.Play();
    }


    public override void Clear()
    {
        GameManager.Data.BattleBackGround.Stop();

        GameManager.Data.Dungeon.didBattle = false;

        GameManager.Data.Dialog.ResetData();
        //다이알로그 릴리즈
        //GameManager.Pool.ReleaseUI(GameManager.Data.Dialog.dialog_obj);

        GameManager.Pool.erasePoolDicContet(GameManager.Data.Dialog.dialog_obj.name);


        foreach (var player in GameManager.Data.Dungeon.InBattlePlayers) 
        {
            GameManager.Pool.Release(player);
        }

        foreach (var symbol in GameManager.Data.Dungeon.aliveInDungeonSymbols)
        {
            GameManager.Pool.Release(symbol);
        }

    }
}