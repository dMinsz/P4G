using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoPlayScene : BaseScene
{
    public VideoPlayer vplayer;
    public Videos video;
    private string nextScene;
    bool isLoadingScene = false;
    protected override void Init()
    {
        video.ChnageVideoClip(GameManager.Data.Video.playVideoNum);

        nextScene = GameManager.Data.Video.nextScene;
        vplayer.Play();
    }

    private void Update()
    {
        if ((vplayer.frame) > 0 && (vplayer.isPlaying == false) && !isLoadingScene) 
        {
            isLoadingScene = true;
            GameManager.Scene.LoadScene(nextScene);
        }

        if (Input.anyKey && !isLoadingScene)
        {
            isLoadingScene = true;
            GameManager.Scene.LoadScene(nextScene);
        }
    }

    protected override IEnumerator LoadingRoutine()
    {
        yield return null;
    }
    public override void Clear()
    {

    }
}
