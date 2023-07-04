using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScene : BaseScene
{



    protected override IEnumerator LoadingRoutine()
    {
        yield return null;
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

