using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyScene : BaseScene
{
    public DialogueSystem dialog;
    public PlayerGenerator playerGenerator;
    bool TestScenIsAwake = false;
    protected override void Awake()
    {
        Debug.Log("Roby Scene Init");
        
        //test
        GameManager.Data.Dungeon.SetUp("LobbyScene");

        GameManager.Data.Dungeon.IsSymbolInit = true;

        GameManager.Pool.Reset();
        GameManager.UI.Reset();
        GameManager.Data.Dialog.SetUp();
        GameManager.Data.Dialog.system = dialog;

        if (!TestScenIsAwake)
        {
            playerGenerator.Init();
            TestScenIsAwake = true;
        }

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
        GameManager.Pool.Reset();
        GameManager.UI.Reset();
        GameManager.Data.Dialog.SetUp();
        GameManager.Data.Dialog.system = dialog;
        yield return new WaitForSecondsRealtime(0.2f);
        progress = 0.2f;

        
        GameManager.Data.Dungeon.SetUp("LobbyScene");
        yield return new WaitForSecondsRealtime(0.2f);
        progress = 0.4f;

        if (!TestScenIsAwake)
        {
            playerGenerator.Init();
            TestScenIsAwake = true;
        }
        yield return new WaitForSecondsRealtime(0.2f);
        progress = 0.6f;
        
        yield return new WaitForSecondsRealtime(0.2f);
        progress = 1.0f;
    }


    public override void Clear()
    {

        GameManager.Data.Dungeon.didBattle = false;

        GameManager.Data.Dialog.ResetData();

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
