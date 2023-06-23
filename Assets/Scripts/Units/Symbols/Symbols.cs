using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Symbols : MonoBehaviour, IHitable
{

    public List<ShadowData> hasEnemys;

    public void TakeHit(Object attacker)
    {
        // 배틀씬으로 이동한다.

        Debug.Log("Symbol Take Hit , Go to Battle");

        GameManager.Data.Dungeon.tempSymbolShadows = hasEnemys;
        GameManager.Data.Dungeon.InBattlePlayers.Clear();

        GameManager.Data.Dungeon.InBattlePlayers.Add((Unit)attacker);
        
        if (((Player)attacker).Partys.Count >= 1 ) 
        {
            foreach (var ally in ((Player)attacker).Partys)
            {
                GameManager.Data.Dungeon.InBattlePlayers.Add(ally);
            }
        }

        GameManager.Data.Dungeon.StartTurn = DungeonDataSystem.Turn.Player;

        //GameManager.Pool.Release(attacker);
        GameManager.Scene.LoadScene("BattleScene");
    }
}
