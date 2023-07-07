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


        //cdialog.Interact();
    }

    protected override IEnumerator LoadingRoutine()
    {
        yield return new WaitForSecondsRealtime(0.2f);
        progress = 1.0f;
        cdialog.Interact();
    }
    public override void Clear()
    {
        GameManager.Data.Dialog.ResetData();
        //다이알로그 릴리즈
        //GameManager.Pool.ReleaseUI(GameManager.Data.Dialog.dialog_obj);

        GameManager.Pool.erasePoolDicContet(GameManager.Data.Dialog.dialog_obj.name);

    }
}
