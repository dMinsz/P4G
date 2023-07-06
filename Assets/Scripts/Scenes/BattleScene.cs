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
        GameManager.Data.Dungeon.IsSymbolInit = false;
        GameManager.Data.Dungeon.didBattle = true;


        GameManager.Pool.erasePooDicContet("DamageUI");

        //foreach (var player in GameManager.Data.Dungeon.InBattlePlayers)
        //{
        //    //symbol.GetComponent<Symbols>().ReleasePool();
        //    GameManager.Pool.Release(player);
        //}

        //foreach (var symbol in GameManager.Data.Dungeon.aliveInDungeonSymbols)
        //{//심볼컨테이너 삭제
        //    GameManager.Pool.DestroyContainer(symbol);
        //}
    }

}

