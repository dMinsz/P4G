using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class DataManager : MonoBehaviour
{
    //video Data
    public VideoSystem Video;

    //Shadow Datas
    public ShadowDataSystem Shadow;

    private void Awake()
    {
        Video = new VideoSystem();
        Shadow = new ShadowDataSystem();
    }





}
