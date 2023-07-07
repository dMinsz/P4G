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

    public List<ShadowData> shadowDatas = new List<ShadowData>();//���� �������� ���ü��ִ� ������ ������

    // ���� ���� ��� �ɺ���
    public  List<GameObject> aliveInDungeonSymbols;
    public  List<ShadowData> tempSymbolShadows;// = new List<ShadowData>();//��Ʋ������ �Ѱ��� �����쵥����
    public  List<Unit> InBattlePlayers;// = new List<Unit>(); // ��Ʋ������ �Ѱ��� �÷��̾�


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



    public void Initialize() //������ �ʱ�ȭ
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
    {// ��Ʋ�� ���� ������ �ʱ�ȭ
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

    //���� �������� �����ü��ִ� ������ �������ΰ�������
    public List<ShadowData> GetRandomShadows(int eachShadows)
    {
        List<ShadowData> result = new List<ShadowData>();


        int EachShadows = Random.Range(1, eachShadows + 1);// �� �ɺ����� ���������� ���ִ� ���� �� ��������


        for (int i = 0; i < EachShadows; i++)
        {
            int rand = Random.Range(0, shadowDatas.Count); // ���������� ���ü��ִ� ������ �������� �������� �̱�
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
