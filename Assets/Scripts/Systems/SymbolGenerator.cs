using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SymbolGenerator : MonoBehaviour
{
    public Transform[] GenratePos;
    public GameObject Symbol;
    //public List<ShadowData> shadowDatas;
    private void Start()
    {
        if (GameManager.Data.Dungeon.IsInit) // ó�� �����Ҷ� 
        {
            for (int i = 0; i < GenratePos.Length; i++)
            {
                var newSymbol = GameManager.Pool.Get(true, Symbol, GenratePos[i].position, Quaternion.identity , i.ToString());

                GameManager.Data.Dungeon.aliveInDungeonSymbols.Add(newSymbol);

                newSymbol.GetComponent<Symbols>().hasEnemys.Add(GameManager.Data.Dungeon.GetRandomShadow());

            }
        }
        else //��Ʋ�ϰ� ���� �ٽ� ���ƿ�����
        {
            var AliveSymbols = GameManager.Data.Dungeon.aliveInDungeonSymbols;
          
            for (int i = 0; i < AliveSymbols.Count; i++)
            {
                var aliveSymbol = GameManager.Pool.Get(true, AliveSymbols[i], AliveSymbols[i].transform.position,
                                                       AliveSymbols[i].transform.rotation);
            }

        }
    }
}
