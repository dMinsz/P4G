using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleSystem : MonoBehaviour
{

    public enum TurnType
    {
        Player,
        Enemy,
        Done,
    }

    
    public int turnCount = 0; //

    public List<Shadow> InBattleShadows = new List<Shadow>();
    public List<Player> InBattlePlayers = new List<Player>();

    public void SetUp()
    {
        //Look Set
        foreach (var shadow in InBattleShadows) 
        {
            shadow.transform.LookAt(InBattlePlayers[0].transform.position);
        }

        foreach (var player in InBattlePlayers) 
        {
            player.transform.LookAt(InBattleShadows[0].transform.position);
        }
    }

    public void ReleasePool() 
    {
        foreach (var symbol in GameManager.Data.Dungeon.aliveInDungeonSymbols)
        {
            symbol.GetComponent<Symbols>().ReleasePool();
        }

    }

    public void EndBattle() 
    {
        var nowSymbol = GameManager.Data.Dungeon.nowSymbol;
        GameManager.Data.Dungeon.aliveInDungeonSymbols.Remove(nowSymbol);
        GameManager.Data.Dungeon.nowSymbol = null;

       
        GameManager.Pool.DestroyContainer(nowSymbol);

        GameManager.Scene.LoadScene("LobbyScene");
    }

}
