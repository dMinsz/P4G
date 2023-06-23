using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BattleScene : BaseScene
{
    BattleObjectGenerator btg;

    void Awake()
    {
        base.Awake();

        btg = GameObject.Find("@BattleSystem").GetComponent<BattleObjectGenerator>();

        GameManager.Pool.Init();

    }

    protected override IEnumerator LoadingRoutine()
    {
        btg.SetUp();
        progress = 1.0f;
        yield return null;
    }
    public override void Clear()
    {
        
    }

}

