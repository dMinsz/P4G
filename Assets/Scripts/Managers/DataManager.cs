using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Video;

public class DataManager : MonoBehaviour
{
    //video Data
    public VideoSystem Video;

    //Dungeon Datas
    public DungeonDataSystem Dungeon;

    public void SetUp()
    {
        var vidobj = new GameObject();
        vidobj.transform.parent = transform;
        Video=vidobj.AddComponent<VideoSystem>();


        var dObj = new GameObject();
        dObj.transform.parent = transform;
        Dungeon = dObj.AddComponent<DungeonDataSystem>();
        
        Dungeon.Initialize();
    }

}
