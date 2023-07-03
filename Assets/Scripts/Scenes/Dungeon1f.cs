using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dungeon1f : BaseScene
{

    protected override void Awake()
    {
        base.Awake();
        Debug.Log("Dungeon 1F Scene Init");

        GameManager.Data.Dungeon.SetUp("1F");

    }

    protected override IEnumerator LoadingRoutine()
    {
        // fake loading
        GameManager.Data.Dungeon.SetUp("1F");

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