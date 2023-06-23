using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Symbols : MonoBehaviour, IHitable
{

    public List<ShadowData> hasEnemys;

    public void TakeHit(GameObject attacker)
    {
        // 배틀씬으로 이동한다.

        Debug.Log("Symbol Take Hit , Go to Battle");

        GameManager.Data.Dungeon.tempSymbolShadows = hasEnemys;

        GameManager.Data.Dungeon.SetGoToBattle(DungeonDataSystem.Turn.Player, this.gameObject);

        foreach (var symbol in GameManager.Data.Dungeon.aliveInDungeonSymbols)
        {
            symbol.GetComponent<Symbols>().ReleasePool();
        }

        foreach (var player in GameManager.Data.Dungeon.InBattlePlayers)
        {
            player.GetComponent<PlayerHiter>().ReleasePool();
        }


        GameManager.Scene.LoadScene("BattleScene");
    }

    public void ReleasePool() 
    {
        if (GameManager.Pool.IsContain(this.gameObject))
        {
            GameManager.Pool.Release(this.gameObject);
        }
    }
}
