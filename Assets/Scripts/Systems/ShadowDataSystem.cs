using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowDataSystem : MonoBehaviour
{

    public DungeonDatas datas;
    public DungeonDatas.DungeonInfo nowDungeon;
    List<ShadowData> shadows = new List<ShadowData>();
    private void Awake()
    {
        datas = GameManager.Resource.Load<DungeonDatas>("Datas/DungeonDatas");
    }


    //���� �������� �����ü��ִ� ������ �������ΰ�������
    public List<ShadowData> GetRandomShadow(int shadowCount)
    {
        List<ShadowData> result = new List<ShadowData>();


        for (int i = 0; i < shadowCount; i++)
        {
            int rand = Random.Range(0, shadows.Count + 1);
            result.Add(shadows[rand]);
        }

        return result;
    }

    public bool SetUp(string dungeonName)
    {
        if (SetNowDungeon(dungeonName))
        {
            SetShadows();
            if (shadows.Count <= 0)
            {
                Debug.Log("SymbolSystem:  No Shadows Or Read Error");
                return false;
            }

            return true;
        }
        else
        {
            Debug.Log("SymbolSystem: Dungeon Name InCorrect OR Can not Read Dungeon Info name:" + dungeonName);
            return false;
        }

    }


    //���� ������ ����������ִ� ������ ��������
    private void SetShadows()
    {
        foreach (ShadowData shadow in nowDungeon.shadows)
        {
            shadows.Add(shadow);
        }
    }


    private bool SetNowDungeon(string name)
    {
        foreach (var data in datas.Dungeons)
        {
            if (data.DungeonName == name)
            {
                nowDungeon = data;
                return true;
            }
        }
        return false;
    }




}
