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
    public GameObject nowSymbol;

    public bool IsInit;
    public bool IsSymbolInit;
    public bool didBattle;

    public DungeonDatas datas;
    public DungeonDatas.DungeonInfo nowDungeon; 

    public List<ShadowData> shadowDatas = new List<ShadowData>();//현재 던전에서 나올수있는 섀도우 데이터

    // 던전 내에 모든 심볼들
    public  List<GameObject> aliveInDungeonSymbols;
    public  List<ShadowData> tempSymbolShadows;// = new List<ShadowData>();//배틀씬으로 넘겨줄 섀도우데이터
    public  List<Unit> InBattlePlayers;// = new List<Unit>(); // 배틀씬으로 넘겨줄 플레이어


    public void AddAliveSymbol(GameObject obj) 
    {
        aliveInDungeonSymbols.Add(obj);
    }
    public void AddTempShadows(ShadowData data)
    {
        tempSymbolShadows.Add(data);
    }

    public void AddBattlePlayer(Unit unit)
    {
        InBattlePlayers.Add(unit);
    }



    public void Initialize() //데이터 초기화
    {
        datas = GameManager.Resource.Load<DungeonDatas>("Datas/DungeonDatas");
        nowDungeon = new DungeonDatas.DungeonInfo();

        InBattlePlayers = new List<Unit>();
        tempSymbolShadows = new List<ShadowData>();
        aliveInDungeonSymbols = new List<GameObject>();

        IsInit = true;
        IsSymbolInit = true;
        didBattle = false;
    }
    public bool SetUp(string dungeonName)
    {// 배틀에 들어가는 섀도우 초기화
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


    public void SetGoToBattle(Turn start, GameObject nowSym) 
    {
        StartTurn = start;
        nowSymbol = nowSym;
    }

    //현재 던전에서 가져올수있는 섀도우 랜덤으로가져오기
    public List<ShadowData> GetRandomShadows(int eachShadows)
    {
        List<ShadowData> result = new List<ShadowData>();


        int EachShadows = Random.Range(1, eachShadows + 1);// 각 심볼마다 가지고있을 수있는 몬스터 수 랜덤설정


        for (int i = 0; i < EachShadows; i++)
        {
            int rand = Random.Range(0, shadowDatas.Count); // 던전내에서 나올수있는 섀도우 종류에서 랜덤으로 뽑기
            result.Add(shadowDatas[rand]);
        }


        return result;
    }

    public ShadowData GetRandomShadow()
    {
        int rand = Random.Range(0, shadowDatas.Count);
        return shadowDatas[rand];

    }


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
