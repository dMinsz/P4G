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

    //Dialogue
    public Dialogue Dialog;

    public DungeonAndBattleBGM BattleBackGround;
    public void SetUp()
    {
        var vidobj = new GameObject("VideoSystem");
        vidobj.transform.parent = transform;
        Video=vidobj.AddComponent<VideoSystem>();


        var dObj = new GameObject("DungeonDataSystem");
        dObj.transform.parent = transform;
        Dungeon = dObj.AddComponent<DungeonDataSystem>();


        var bObj = new GameObject("BattleDataSystem");
        bObj.transform.parent = transform;
        Battle = bObj.AddComponent<BattleSystem>();
        
        Dungeon.Initialize();


        var dlObj = new GameObject("DialogueSystem");
        dlObj.transform.parent = transform;
        Dialog = dlObj.AddComponent<Dialogue>();

        //Dialog.SetUp();

        //var sObj = new GameObject("SoundSystem");
        //sObj.transform.parent = transform;

        var sound = GameManager.Resource.Load<DungeonAndBattleBGM>("Sound/BattleBGM");
        var psound = GameManager.Pool.Get(true, sound);
        psound.transform.SetParent(transform);
        BattleBackGround = psound;


    }



}
