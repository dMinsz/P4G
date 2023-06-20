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
        GameManager.Data.SetVideo(0, "RobyScene");

        GameManager.Scene.LoadScene("VideoPlayScene");
    }
}

