using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyScene : BaseScene
{

    protected override void Awake()
    {
        base.Awake();
        Debug.Log("Roby Scene Init");

        GameManager.Data.Dungeon.SetUp("Lobby");
        GameManager.Pool.Reset();
    }

    protected override IEnumerator LoadingRoutine()
    {
        // fake loading

        yield return new WaitForSecondsRealtime(0.2f);
        progress = 0.2f;
        yield return new WaitForSecondsRealtime(0.2f);
        progress = 0.4f;
        yield return new WaitForSecondsRealtime(0.2f);
        progress = 0.6f;
        yield return new WaitForSecondsRealtime(0.2f);
        progress = 0.8f;
        yield return new WaitForSecondsRealtime(0.2f);
        progress = 1.0f;
    }
    public override void Clear()
    {    
    }
}
