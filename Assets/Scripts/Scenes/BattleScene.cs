using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BattleScene : BaseScene
{
    BattleObjectGenerator btg;

    protected override void Awake()
    {

        
    }

    protected override IEnumerator LoadingRoutine()
    {
        yield return new WaitForSecondsRealtime(0.2f);
        btg = GameObject.Find("@BattleSystem").GetComponent<BattleObjectGenerator>();

        GameManager.Pool.Reset();
        progress = 0.3f;

        yield return new WaitForSecondsRealtime(0.2f);
        GameManager.Data.Dungeon.IsInit = false;
        progress = 0.6f;

        yield return new WaitForSecondsRealtime(0.2f);
        GameManager.UI.Reset();
        btg.SetUp();
        GameManager.Data.Battle.Initialize();
        progress = 0.8f;

        yield return new WaitForSecondsRealtime(0.2f);
        progress = 1.0f;
        GameManager.Data.BattleBackGround.Play();
    }
    public override void Clear()
    {
        GameManager.Data.BattleBackGround.Stop();

        GameManager.Data.Dungeon.IsSymbolInit = false;
        GameManager.Data.Dungeon.didBattle = true;


        GameManager.Pool.erasePoolDicContet("DamageUI");

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

