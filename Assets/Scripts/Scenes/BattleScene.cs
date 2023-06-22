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

        

    }

    protected override IEnumerator LoadingRoutine()
    {
        btg.SetUp();

        foreach (var player in btg.InBattlePlayers)
        {
            player.GetComponent<PlayerMover>().enabled = false;
            player.GetComponent<PlayerAttacker>().enabled = false;
            player.GetComponent<PlayerInput>().enabled = false;
        }
       
        yield return null;
    }
    public override void Clear()
    {
        foreach (var player in btg.InBattlePlayers)
        {
            player.GetComponent<PlayerMover>().enabled = true;
            player.GetComponent<PlayerAttacker>().enabled = true;
            player.GetComponent<PlayerInput>().enabled = true;
        }
    }

}

