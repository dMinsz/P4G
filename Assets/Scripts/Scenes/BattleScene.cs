using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BattleScene : BaseScene
{
    BattleObjectGenerator btg;

    protected override void Awake()
    {
        base.Awake();

        btg = GameObject.Find("@BattleSystem").GetComponent<BattleObjectGenerator>();

        GameManager.Pool.Reset();


        GameManager.Data.Dungeon.IsInit = false;
    }

    protected override IEnumerator LoadingRoutine()
    {
        GameManager.UI.Reset();
        btg.SetUp();
        GameManager.Data.Battle.Initialize();
        progress = 1.0f;
        yield return null;
    }
    public override void Clear()
    {
        
    }

}

