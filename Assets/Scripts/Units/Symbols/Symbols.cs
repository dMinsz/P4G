using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Symbols : MonoBehaviour, IHitable
{

    public List<ShadowData> hasEnemys;

    public void TakeHit(Object attacker)
    {
        // ��Ʋ������ �̵��Ѵ�.

        Debug.Log("Symbol Take Hit , Go to Battle");

        GameManager.Data.Dungeon.tempSymbolShadows = hasEnemys;
       

        GameManager.Data.Dungeon.StartTurn = DungeonDataSystem.Turn.Player;

        GameManager.Pool.Release((Unit)attacker);

        GameManager.Scene.LoadScene("BattleScene");
    }
}
