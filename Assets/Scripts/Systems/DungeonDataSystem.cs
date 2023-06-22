using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DungeonDataSystem : MonoBehaviour
{
    public enum Turn 
    {
        Player,
        Enemy
    }

    public Turn StartTurn;

    public DungeonDatas datas;
    public DungeonDatas.DungeonInfo nowDungeon; 


    public List<ShadowData> shadowDatas = new List<ShadowData>();//현재 던전에서 나올수있는 섀도우 데이터

    // 던전 내에 모든 심볼들이 가지고있는 섀도우데이터
    //public List<ShadowData> aliveInDungeonShadows = new List<ShadowData>();

    public List<Symbols> aliveInDungeonSymbols = new List<Symbols>();


    public List<ShadowData> tempSymbolShadows = new List<ShadowData>();//배틀씬으로 넘겨줄 섀도우데이터
    public List<Player> InBattlePlayers = new List<Player>(); // 배틀씬으로 넘겨줄 플레이어


    public bool IsPlayerTurn = true;

    //현재 던전에서 가져올수있는 섀도우 랜덤으로가져오기
    public List<ShadowData> GetRandomShadows(int shadowCount)
    {
        List<ShadowData> result = new List<ShadowData>();


        for (int i = 0; i < shadowCount; i++)
        {
            int rand = Random.Range(0, shadowDatas.Count);
            result.Add(shadowDatas[rand]);
            aliveInDungeonSymbols.Add(shadowDatas[rand].Shadow.Prefab.GetComponent<Symbols>());// 현재 던전에 있는 심볼 추가
        }


        return result;
    }

    public ShadowData GetRandomShadow()
    {
        int rand = Random.Range(0, shadowDatas.Count);

        aliveInDungeonSymbols.Add(shadowDatas[rand].Shadow.Prefab.GetComponent<Symbols>()); // 현재 던전에 있는 심볼 추가
        return shadowDatas[rand];

    }

    public void Initialize()
    {
        datas = GameManager.Resource.Load<DungeonDatas>("Datas/DungeonDatas");
        nowDungeon = new DungeonDatas.DungeonInfo();
    }
    public bool SetUp(string dungeonName)
    {
        if (SetDungeon(dungeonName) == DungeonSet.New)
        {
            SetShadows();
            if (shadowDatas.Count <= 0)
            {
                Debug.Log("SymbolSystem:  No Shadows Or Read Error");
                return false;
            }

            return true;
        }
        else if (SetDungeon(dungeonName) == DungeonSet.NotFound)
        {
            Debug.Log("SymbolSystem: Dungeon Name InCorrect OR Can not Read Dungeon Info name:" + dungeonName);
            return false;
        }
        else if (SetDungeon(dungeonName) == DungeonSet.Already) 
        {
            Debug.Log("SymbolSystem: Dungeon Name Already Used name : " + dungeonName);
            return false;
        }
        else
        {
            return false;
        }

    }


    //현재 던전의 만들어질수있는 섀도우 가져오기
    private void SetShadows()
    {
        foreach (ShadowData shadow in nowDungeon.shadows)
        {
            shadowDatas.Add(shadow);
        }
    }


    enum DungeonSet 
    {
        NotFound,
        New,
        Already,
        Size
    }

    private DungeonSet SetDungeon(string name)
    {

        if (nowDungeon.DungeonName == name)
        {
            Debug.Log("Come Back to Dugeon");
            return DungeonSet.Already;
        }

        foreach (var data in datas.Dungeons)
        {
            if (data.DungeonName == name)
            {
                nowDungeon = data;
                return DungeonSet.New;
            }
        }
        return DungeonSet.NotFound;
    }




}
