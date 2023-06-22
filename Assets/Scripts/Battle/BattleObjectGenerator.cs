using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class BattleObjectGenerator : MonoBehaviour
{

    public bool IsDebug = false;
    public float shadowGenerateRange;

    public Transform[] PlayerPoints;

    [HideInInspector] public List<ShadowData> InBattleShadowDatas;
    //[HideInInspector] public List<PlayerData> InBattlePlayerDatas;
    [HideInInspector] public List<Player> InBattlePlayers;



    [HideInInspector]  public BattleSystem battleSystem;

    //public void SetUp() 
    //{
    //    InBattleShadowDatas = GameManager.Data.Dungeon.tempSymbolShadows;
    //    InBattlePlayerDatas = GameManager.Data.Dungeon.InBattlePlayers;
    //}
    private void Start()
    {
      
    }


    public void SetUp()
    {
        InBattleShadowDatas = GameManager.Data.Dungeon.tempSymbolShadows;
        InBattlePlayers = GameManager.Data.Dungeon.InBattlePlayers;

        battleSystem = GetComponent<BattleSystem>();

        if (InBattleShadowDatas.Count <= 0) 
        {
            UnityEngine.Debug.Log("Shadow Data Is None");
        }

        if (InBattleShadowDatas.Count == 1)
        {
            GameObject obj = InBattleShadowDatas[0].shadow.Prefab;
            var newShadow = GameManager.Pool.Get(true,obj, RandomSphereInPoint(shadowGenerateRange), Quaternion.identity);

            battleSystem.InBattleShadows.Add(newShadow.GetComponent<Shadow>());
            MakePlayers();
        }
        else 
        {
            MakeShadows();

            MakePlayers();
        }

    }

    

    private void MakeShadows() 
    {
        for (int i = 0; i < InBattleShadowDatas.Count; i++)
        {
            GameObject obj = InBattleShadowDatas[i].shadow.Prefab;

            //
            var newShadow = GameManager.Pool.Get(false,obj, RandomSphereInPoint(shadowGenerateRange), Quaternion.identity);

            battleSystem.InBattleShadows.Add(newShadow.GetComponent<Shadow>());
        }

    }

    private void MakePlayers() 
    {
        for (int i = 0; i < InBattlePlayers.Count; i++)
        {
            var newPlayer = GameManager.Pool.Get(true, InBattlePlayers[i], PlayerPoints[i].position, Quaternion.identity);

            battleSystem.InBattlePlayers.Add(newPlayer);
        }
    }


    private Vector3 RandomSphereInPoint(float radius)
    {
        Vector3 getPoint = UnityEngine.Random.onUnitSphere;
        getPoint.y = 0.0f;

        float r = UnityEngine.Random.Range(0, radius);

        return (getPoint * r) + transform.position;

    }


    private void OnDrawGizmos()
    {
        if (!IsDebug)
        {
            return;
        }

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, shadowGenerateRange);
    }
}
