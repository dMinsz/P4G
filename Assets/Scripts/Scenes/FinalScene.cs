using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalScene : BaseScene
{
    public FinalDialogueSystem dialog;
    protected override void Awake()
    {
        Debug.Log("Final Scene Init");
    }

    protected override IEnumerator LoadingRoutine()
    {
        GameManager.Pool.Reset();
        GameManager.UI.Reset();
        GameManager.Data.Dialog.SetUp();
        GameManager.Data.Dialog.IsVelvetRoom = true;

        yield return new WaitForSecondsRealtime(0.2f);
        progress = 1.0f;


        if (GameManager.Data.Dungeon.InBattlePlayers.Count >= 3)
        {

            dialog.StartRutine(5);
        }
        else 
        {
            dialog.StartRutine(4);
        }

    }
    public override void Clear()
    {
        GameManager.Data.BattleBackGround.ResetAudio();

        GameManager.Data.Dialog.IsVelvetRoom = false;

        GameManager.UI.Reset();
        GameManager.Data.Dialog.ResetData();

        GameManager.Pool.Init();
        GameManager.Data.Dungeon.Initialize();

        GameManager.Data.Dungeon.IsInit = true;
    }
}
