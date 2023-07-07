using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SymbolGenerator : MonoBehaviour
{
    public Transform[] GenratePos;
    public GameObject Symbol;

    //public List<ShadowData> shadowDatas;

    private void Start()
    {
        if (GameManager.Data.Dungeon.IsSymbolInit) // 처음 시작할때 
        {
            foreach (var symbol in GameManager.Data.Dungeon.aliveInDungeonSymbols.ToList())
            {
                //이전 씬에있던 심볼들 삭제
                GameManager.Data.Dungeon.aliveInDungeonSymbols.Remove(symbol);

                GameManager.Pool.erasePoolDicContet(symbol.gameObject.name);

                GameManager.Pool.DestroyContainer(symbol);

                GameManager.Pool.eraseContainerContet(symbol.gameObject.name);

            }


            for (int i = 0; i < GenratePos.Length; i++)
            {
                var newSymbol = GameManager.Pool.Get(true, Symbol, GenratePos[i].position, Quaternion.identity , i.ToString());

                newSymbol.GetComponent<SymbolAI>().agent.Warp(GenratePos[i].position);//agent 가 위치값에 에러를 만들어서 워프 시켜준다.

                GameManager.Data.Dungeon.aliveInDungeonSymbols.Add(newSymbol);

                int nowDungeonEachShadow = GameManager.Data.Dungeon.nowDungeon.symbolHaveShadow;

                //Shadow Instanciate

                //int rand = Random.Range(1, nowDungeonEachShadow+1); 

                newSymbol.GetComponent<Symbols>().hasEnemys = GameManager.Data.Dungeon.GetRandomShadows(nowDungeonEachShadow);
            }
        }
        else //배틀하고 나서 다시 돌아왔을때
        {
            var AliveSymbols = GameManager.Data.Dungeon.aliveInDungeonSymbols;
          
            for (int i = 0; i < AliveSymbols.Count; i++)
            {
                var aliveSymbol = GameManager.Pool.Get(true, AliveSymbols[i], AliveSymbols[i].transform.position,
                                                       AliveSymbols[i].transform.rotation);

                aliveSymbol.GetComponent<SymbolAI>().agent.Warp(AliveSymbols[i].transform.position);
            }

        }
    }
}
