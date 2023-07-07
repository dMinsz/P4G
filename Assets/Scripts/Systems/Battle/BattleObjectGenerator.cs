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
    public Transform[] EnemyPoints;
    //[HideInInspector] public List<ShadowData> InBattleShadowDatas;
    //[HideInInspector] public List<Unit> InBattlePlayers;

    [HideInInspector]   public BattleSystem battleSystem;
    [HideInInspector]   public BattleUIHandler uIHandler;

    private void Awake()
    {
        
    }

    public void SetUp()
    {
        GameManager.Data.Battle = GetComponent<BattleSystem>();
        battleSystem = GetComponent<BattleSystem>();
        uIHandler = GetComponent<BattleUIHandler>();

        if (GameManager.Data.Dungeon.tempSymbolShadows.Count <= 0) 
        {
            UnityEngine.Debug.Log("Shadow Data Is None");
        }

        if (GameManager.Data.Dungeon.tempSymbolShadows.Count == 1)
        {
            GameObject obj = GameManager.Data.Dungeon.tempSymbolShadows[0].shadow.Prefab;

            var newShadowObj = GameManager.Pool.Get(false, obj, EnemyPoints[0].position, Quaternion.identity);
            var newShadow = newShadowObj.GetComponent<Shadow>();


            newShadow.data = GameManager.Data.Dungeon.tempSymbolShadows[0].shadow;

            newShadow.MaxHp = newShadow.data.Hp;
            newShadow.curHp = newShadow.data.Hp;

            newShadow.MaxSp = newShadow.data.Sp;
            newShadow.curSp = newShadow.data.Sp;

            battleSystem.InBattleShadows.Add(newShadow);

            if (GameManager.Resource.resources.ContainsKey("ShadowHPBar.UI/DamageUI"))
            {
                GameManager.Resource.resources.Remove("ShadowHPBar.UI/DamageUI");
            }

            var dobj = GameManager.Resource.Load<ShadowHPBar>("UI/DamageUI");
            ShadowHPBar ui = GameManager.UI.ShowInGameUI<ShadowHPBar>(dobj);

            ui.shadow = newShadow;
            ui.SetBar();

            uIHandler.DamageUIs.Add(ui);
            GameManager.UI.CloseInGameUI(ui);

            MakePlayers();
        }
        else 
        {
            MakeShadows();

            MakePlayers();
        }

        GetComponent<BattleUIHandler>().SetUpUI(battleSystem);

        //SetUpUI();

        //BattleSystem set

        battleSystem.LookSetUp();
        battleSystem.SetDefalt();

    }



    private void MakeShadows() 
    {
        //최대 3마리
        for (int i = 0; i < GameManager.Data.Dungeon.tempSymbolShadows.Count; i++)
        {
            GameObject obj = GameManager.Data.Dungeon.tempSymbolShadows[i].shadow.Prefab;

            var newShadowObj = GameManager.Pool.Get(false,obj, EnemyPoints[i].position, Quaternion.identity , i.ToString());
            var newShadow = newShadowObj.GetComponent<Shadow>();


            newShadow.data = GameManager.Data.Dungeon.tempSymbolShadows[i].shadow;

            newShadow.MaxHp = newShadow.data.Hp;
            newShadow.curHp = newShadow.data.Hp;

            newShadow.MaxSp = newShadow.data.Sp;
            newShadow.curSp = newShadow.data.Sp;

            battleSystem.InBattleShadows.Add(newShadow);


            //hpbar

            var dobj = GameManager.Resource.Load<ShadowHPBar>("UI/DamageUI");
            dobj.name += i;
            ShadowHPBar ui = GameManager.UI.ShowInGameUI<ShadowHPBar>(dobj);

            ui.shadow = newShadow;
            ui.SetBar();

            uIHandler.DamageUIs.Add(ui);
            GameManager.UI.CloseInGameUI(ui);
        }

    }

    private void MakePlayers() 
    {
        var players = GameManager.Data.Dungeon.InBattlePlayers;
        for (int i = 0; i < players.Count; i++)
        {
            var newPlayer = GameManager.Pool.Get(false, players[i].data.BattlePrefab, PlayerPoints[i].position, Quaternion.identity);

            var player = newPlayer.GetComponent<Player>();

            player.data = players[i].data;

            player.MaxHp = players[i].GetComponent<Player>().MaxHp;
            player.MaxSp = players[i].GetComponent<Player>().MaxSp;

            player.curHp = players[i].GetComponent<Player>().HP;
            player.curSp = players[i].GetComponent<Player>().SP;

            //persona
            player.Personas = players[i].GetComponent<Player>().Personas;

            foreach (var persona in player.Personas)
            {

                if (GameManager.Pool.IsContain(persona))
                {
                    GameManager.Pool.erasePoolDicContet(persona.name);
                    GameManager.Pool.eraseContainerContet(persona.name);
                }

                var newPersona = GameManager.Pool.Get(false, persona, player.PersonaPoint.position, Quaternion.identity);
                GameManager.Pool.Release(newPersona);
            }

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
