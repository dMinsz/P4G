using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHiter : MonoBehaviour, IHitable
{
    public void TakeHit(GameObject attacker)
    {
        // ��Ʋ������ �̵��Ѵ�.

        Debug.Log("Symbol Take Hit , Go to Battle");

        GameManager.Data.Dungeon.tempSymbolShadows = attacker.GetComponent<Symbols>().hasEnemys;

        GameManager.Data.Dungeon.SetGoToBattle(DungeonDataSystem.Turn.Enemy, attacker);


        foreach (var symbol in GameManager.Data.Dungeon.aliveInDungeonSymbols)
        {
            symbol.GetComponent<Symbols>().ReleasePool();
        }

        foreach (var player in GameManager.Data.Dungeon.InBattlePlayers)
        {
            GameManager.Pool.Release(player);
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
