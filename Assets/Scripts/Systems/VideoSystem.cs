using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoSystem : MonoBehaviour
{
    public int playVideoNum = 0;
    public string nextScene;

    public void SetVideo(int videoNumber, string runAfterScene)
    {
        playVideoNum = videoNumber;
        nextScene = runAfterScene;
    }

}
