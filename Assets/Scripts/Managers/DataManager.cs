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


    //Battle Shared Data
    public BattleSystem Battle;

    public void SetUp()
    {
        var vidobj = new GameObject("VideoSystem");
        vidobj.transform.parent = transform;
        Video=vidobj.AddComponent<VideoSystem>();


        var dObj = new GameObject("DungeonDataSystem");
        dObj.transform.parent = transform;
        Dungeon = dObj.AddComponent<DungeonDataSystem>();
        Dungeon.Initialize();


        var bObj = new GameObject("BattleDataSystem");
        bObj.transform.parent = transform;
        Battle = bObj.AddComponent<BattleSystem>();
    }

}
