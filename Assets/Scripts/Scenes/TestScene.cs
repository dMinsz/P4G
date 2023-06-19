using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScene : BaseScene
{
    protected override void Awake()
    {
        base.Awake();
        Debug.Log("Test Scene Init");
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

    private void OnDestroy()
    {
        Debug.Log("Test Scene Release");
    }

    public void OnStartButton()
    {
        //GameManager.Scene.LoadScene("TestScene");
    }

    public override void Clear()
    {

    }
}