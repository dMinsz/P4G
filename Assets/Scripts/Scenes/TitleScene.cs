using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScene : BaseScene
{

    protected override IEnumerator LoadingRoutine()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        progress = 1.0f;
    }
    public override void Clear()
    {

    }

    public void OnStartButton()
    {
        GameManager.Data.Video.SetVideo(0, "VelvetRoom");

        GameManager.Scene.LoadScene("VideoPlayScene");
    }
}

