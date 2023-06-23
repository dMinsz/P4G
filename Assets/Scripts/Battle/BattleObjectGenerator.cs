using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class BattleObjectGenerator : MonoBehaviour
{

    public bool IsDebug = false;
    public float shadowGenerateRange;

    public Transform[] PlayerPoints;

    [HideInInspector] public List<ShadowData> InBattleShadowDatas;
    [HideInInspector] public List<Unit> InBattlePlayers;



    [HideInInspector]  public BattleSystem battleSystem;

    public BattleUI ui;
    public Transform partyUI;
    private void Awake()
    {
        partyUI = ui.transform.Find("PartyUI");
    }

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
            var newShadow = GameManager.Pool.Get(false,obj, RandomSphereInPoint(shadowGenerateRange), Quaternion.identity);
            //var newShadow = GameManager.Resource.Instantiate(InBattleShadowDatas[0].shadow.Prefab, RandomSphereInPoint(shadowGenerateRange), Quaternion.identity);
            newShadow.GetComponent<Shadow>().data = InBattleShadowDatas[0].shadow;
            //newShadow.GetComponent<Shadow>().animator.Rebind();
            battleSystem.InBattleShadows.Add(newShadow.GetComponent<Shadow>());

            MakePlayers();
        }
        else 
        {
            MakeShadows();

            MakePlayers();
        }
        SetUpUI();
    }

    public void SetUpUI()
    {
        for (int i = 0; i < battleSystem.InBattlePlayers.Count; i++)
        {
            var obj = GameManager.Resource.Load<GameObject>("UI/BattlePartyElement");
            var newUI = GameManager.Pool.GetUI(obj, partyUI);

            newUI.transform.Find("Character").GetComponent<Image>().sprite = battleSystem.InBattlePlayers[i].data.battleImage;
            newUI.transform.Find("Status").Find("SPSlider").GetComponent<SliderBar>().player = battleSystem.InBattlePlayers[i];
            newUI.transform.Find("Status").Find("HPSlider").GetComponent<SliderBar>().player = battleSystem.InBattlePlayers[i];

            newUI.transform.Find("SPText").GetComponent<BarText>().player = battleSystem.InBattlePlayers[i];
            newUI.transform.Find("HPText").GetComponent<BarText>().player = battleSystem.InBattlePlayers[i];
        }
    }




    private void MakeShadows() 
    {
        for (int i = 0; i < InBattleShadowDatas.Count; i++)
        {
            GameObject obj = InBattleShadowDatas[i].shadow.Prefab;

            var newShadow = GameManager.Pool.Get(false,obj, RandomSphereInPoint(shadowGenerateRange), Quaternion.identity);

            newShadow.GetComponent<Shadow>().data = InBattleShadowDatas[i].shadow;

            battleSystem.InBattleShadows.Add(newShadow.GetComponent<Shadow>());
        }

    }

    private void MakePlayers() 
    {
        for (int i = 0; i < InBattlePlayers.Count; i++)
        {
            var newPlayer = GameManager.Pool.Get(false, InBattlePlayers[i].data.BattlePrefab, PlayerPoints[i].position, Quaternion.identity);

            newPlayer.GetComponent<Player>().data = InBattlePlayers[i].data;

            newPlayer.GetComponent<Player>().MaxHp = InBattlePlayers[i].GetComponent<Player>().MaxHp;
            newPlayer.GetComponent<Player>().MaxSp = InBattlePlayers[i].GetComponent<Player>().MaxSp;

            newPlayer.GetComponent<Player>().curHp = InBattlePlayers[i].GetComponent<Player>().HP;
            newPlayer.GetComponent<Player>().curSp = InBattlePlayers[i].GetComponent<Player>().SP;
            battleSystem.InBattlePlayers.Add(newPlayer.GetComponent<Player>());
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
