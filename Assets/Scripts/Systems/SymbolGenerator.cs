using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SymbolGenerator : MonoBehaviour
{
    public Transform[] GenratePos;
    public GameObject Symbol;
    //public List<ShadowData> shadowDatas;
    private void Awake()
    {

     
    }

    private void Start()
    {
        for (int i = 0; i < GenratePos.Length; i++)
        {
            var newSymbol= GameManager.Pool.Get(true,Symbol, GenratePos[i].position , Quaternion.identity);

            GameManager.Data.Dungeon.aliveInDungeonSymbols.Add(newSymbol.GetComponent<Symbols>());

            newSymbol.GetComponent<Symbols>().hasEnemys.Add(GameManager.Data.Dungeon.GetRandomShadow());
            
        }
    }
}
