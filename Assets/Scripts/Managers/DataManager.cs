using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class DataManager : MonoBehaviour
{
    //video Data
    public int playVideoNum = 0;
    public string nextScene;

    public void SetVideo(int videoNumber, string runAfterScene) 
    {
        playVideoNum = videoNumber;
        nextScene = runAfterScene;
    }

    public void ChnageVideoClip() 
    {
        var player = GameObject.Find("VideoPlayer").GetComponent<VideoPlayer>();
        
        if (player == null) 
        {
            Debug.Log("Not Found Video player");
            return;
        }

        player.playOnAwake = false;
        
        if (playVideoNum == 0) 
        {
            VideoClip clip = GameManager.Resource.Load<VideoClip>("Videos/0.mp4");
            if (clip == null )
                Debug.Log("Not Found Video Clip");

            player.clip = clip;
        }
        else if (playVideoNum == 1)
        {
            VideoClip clip = GameManager.Resource.Load<VideoClip>("Videos/1.mp4");
            if (clip == null)
                Debug.Log("Not Found Video Clip");

            player.clip = clip;
        }
    }
}
