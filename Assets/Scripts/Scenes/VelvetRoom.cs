using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelvetRoom : BaseScene
{
    public DialogueSystem dialog;
    protected override void Awake()
    {
        Debug.Log("VelvetRoom Scene Init");
    }

    protected override IEnumerator LoadingRoutine()
    {
        GameManager.UI.Reset();
        GameManager.Data.Dialog.SetUp();
        GameManager.Data.Dialog.IsVelvetRoom = true;


        yield return new WaitForSecondsRealtime(0.2f);
        progress = 1.0f;

        dialog.StartRutine();
    }
    public override void Clear()
    {
        GameManager.Data.Dialog.IsVelvetRoom = false;

    }

    
}
