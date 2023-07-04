using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Videos: MonoBehaviour
{
    public VideoClip[] clips;
    public VideoPlayer player;

    public int playVideoNum = 0;

    private void Awake()
    {
        player = GetComponent<VideoPlayer>();
    }

    public void ChnageVideoClip(int videoIndex)
    {
        if (player == null)
        {
            Debug.Log("Not Found Video player");
            return;
        }

        player.playOnAwake = false;

        if (playVideoNum == videoIndex)
        {
            player.clip = clips[0];
        }
        else if (playVideoNum == videoIndex)
        {
            player.clip = clips[1];
        }
    }

    public void GoToNextScene() 
    {
        player.Stop();

        var next = GameManager.Data.Video.nextScene;

        GameManager.Scene.LoadScene(next);
    }
}
