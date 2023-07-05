using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutScene : BaseScene
{
    public DialogueSystem dialog;
    public CutsceneDialogue cdialog;

    protected override void Awake()
    {
        Debug.Log("CutScene Init");

        GameManager.Pool.Reset();
        GameManager.UI.Reset();

   
        GameManager.Data.Dialog.SetUp();
        GameManager.Data.Dialog.system = dialog;


        cdialog.Interact();
    }

    protected override IEnumerator LoadingRoutine()
    {

        // fake loading
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

        //dialog.Interact();
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
