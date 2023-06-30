using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static Skill;

public class BattleSystem : MonoBehaviour
{
 
    public DungeonDataSystem.Turn turnType;
    public int turnCount = 1; //

    public Player nowPlayer;
    public Shadow nowShadow;
    public BattlePersona nowPersona;

    public List<Shadow> InBattleShadows = new List<Shadow>();
    public List<Player> InBattlePlayers = new List<Player>();

    public Command activeCommand;
    public Queue<Command> commandQueue;

    public BattleUIHandler uiHandler;
    public BattleCamSystem cam;

    public Skill nowSkill;

    public TargetAttributeUI AttributeUIPrefab;

    private void Awake()
    {
        turnType = GameManager.Data.Dungeon.StartTurn;
        commandQueue = new Queue<Command>();
        cam = GetComponent<BattleCamSystem>();

    }

    private void Start()
    {
    }
    public void Initialize()
    {
        var uiobj = GameManager.Resource.Load<TargetAttributeUI>("UI/TargetAttribute");
        AttributeUIPrefab = GameManager.UI.ShowInGameUI(uiobj);
        GameManager.UI.CloseInGameUI(AttributeUIPrefab);
    }


    private void Update()
    {
        if (commandQueue != null && commandQueue.Count > 0)
        {
            if (activeCommand == null || !activeCommand.isExecuting)
            {
                activeCommand = commandQueue.Dequeue();
                activeCommand.Execute();
            }
        }
    }

    public void LookSetUp()
    {

        var nowplayer = nowPlayer;
        if (nowplayer == null)
        {
            nowplayer = InBattlePlayers[0];
        }

        var nowshadow = nowShadow;
        if (nowshadow == null)
        {
            nowshadow = InBattleShadows[0];
        }

        //Look Set
        foreach (var shadow in InBattleShadows)
        {
            shadow.transform.LookAt(nowplayer.transform.position);
        }

        foreach (var player in InBattlePlayers)
        {
            player.transform.LookAt(nowshadow.transform.position);
        }
    }

    public void SetDefalt()
    {
        nowPlayer = InBattlePlayers[0];
        nowShadow = InBattleShadows[0];

        nowShadow.targetUI.gameObject.SetActive(true);

        nowPersona = InBattlePlayers[0].Personas[InBattlePlayers[0].nowPersonaIndex];

        cam.SetPlayerCam(0);
        uiHandler.ChangeCommandUI();

        LookSetUp();
    }

    public void OnAnalysis() 
    {
        uiHandler.AnalysisingUI.gameObject.SetActive(true);

        var obj = GameManager.UI.ShowInGameUI(AttributeUIPrefab);
        obj.Setup(nowShadow.data.resist);
        obj.SetTarget(nowShadow.transform);
    }

    public void OffAnlysis() 
    {
        GameManager.UI.CloseInGameUI(AttributeUIPrefab);

        uiHandler.AnalysisingUI.gameObject.SetActive(false);
    }

    public void OnPlayerAttack()
    {
        GameManager.Data.Battle.commandQueue.Enqueue(new UICommand(uiHandler.MenuUI.transform, false));
        GameManager.Data.Battle.commandQueue.Enqueue(new UICommand(uiHandler.SelectMenuUI.transform, false));
        GameManager.Data.Battle.commandQueue.Enqueue(new UICommand(uiHandler.partyUI.transform, false));
        nowPlayer.Attack(nowShadow.attackPoint.position, nowShadow.transform.position, uiHandler.BattleUI.transform);
        GameManager.Data.Battle.commandQueue.Enqueue(new FuncCommand(NextPlayer));
    }

    public void OnPlayerUsePersonaAttack()
    {
        GameManager.Data.Battle.commandQueue.Enqueue(new UICommand(uiHandler.MenuUI.transform, false));
        GameManager.Data.Battle.commandQueue.Enqueue(new UICommand(uiHandler.SelectMenuUI.transform, false));
        GameManager.Data.Battle.commandQueue.Enqueue(new UICommand(uiHandler.partyUI.transform, false));
        
        nowPlayer.UseSkill(nowShadow.attackPoint.position, nowShadow, uiHandler.BattleUI.transform, cam , nowSkill);

        GameManager.Data.Battle.commandQueue.Enqueue(new FuncCommand(NextPlayer));

    }


    public void NextPlayer()
    {
        int nowIndex = GameManager.Data.Battle.InBattlePlayers.IndexOf(GameManager.Data.Battle.nowPlayer);
        int nowCount = nowIndex + 1;
        int MaxCount = GameManager.Data.Battle.InBattlePlayers.Count;

        if (nowCount < MaxCount)
        {
            GameManager.Data.Battle.nowPlayer = GameManager.Data.Battle.InBattlePlayers[++nowIndex];
            uiHandler.ChangeCommandUI();
            cam.nextPlayer();


            GameManager.Data.Battle.commandQueue.Enqueue(new UICommand(uiHandler.SelectMenuUI.transform, true));
            GameManager.Data.Battle.commandQueue.Enqueue(new UICommand(uiHandler.partyUI.transform, true));

            LookSetUp();
        }
        else
        {
            TurnRoutine = StartCoroutine(EnemyTurnRoutine());
            turnCount++;
        }

    }



    Coroutine TurnRoutine;


    IEnumerator EnemyTurnRoutine()
    {
        Debug.Log("Enemy Turn Routine Start");
        uiHandler.RemoveCommandUI();

        GameManager.Data.Battle.commandQueue.Enqueue(new UICommand(uiHandler.partyUI.transform, true));

        foreach (var shadow in GameManager.Data.Battle.InBattleShadows)
        {//·£´ý°ø°Ý
            GameManager.Data.Battle.commandQueue.Enqueue(new FuncCommand(OffShadowTargeting));

            GameManager.Data.Battle.commandQueue.Enqueue(new FuncCommand(SetRandomPlayer));

            GameManager.Data.Battle.commandQueue.Enqueue(new FuncCommand(shadow.GetComponent<Shadow>().Attack));

        }

        while (true)
        {
            if (GameManager.Data.Battle.commandQueue.Count > 0)
            {
                yield return null;
            }
            else
            {
                //targeting
                GameManager.Data.Battle.commandQueue.Enqueue(new FuncCommand(OnShadowTargeting));

                //default Seting // need is Dead check
                GameManager.Data.Battle.commandQueue.Enqueue(new FuncCommand(SetDefalt));

                GameManager.Data.Battle.commandQueue.Enqueue(new UICommand(uiHandler.BattleUI.transform, true));
                GameManager.Data.Battle.commandQueue.Enqueue(new UICommand(uiHandler.SelectMenuUI.transform, true));
                GameManager.Data.Battle.commandQueue.Enqueue(new UICommand(uiHandler.partyUI.transform, true));

                turnCount++;
                Debug.Log("Enemy Turn Routine Done");
                yield break;
            }
        }
    }

    public void SetRandomPlayer() 
    {
        int rand = Random.Range(0, GameManager.Data.Battle.InBattlePlayers.Count);

        var randomPlayer = GameManager.Data.Battle.InBattlePlayers[rand];
        GameManager.Data.Battle.nowPlayer = randomPlayer;
    }

    public void FilmingHitedPlayer() 
    {
        cam.ResetCams();
        int index = GameManager.Data.Battle.InBattlePlayers.IndexOf(GameManager.Data.Battle.nowPlayer);
        cam.SetPlayerCam(index);
        LookSetUp();
    }

    public void OffShadowTargeting() 
    {
        foreach (var shadow in GameManager.Data.Battle.InBattleShadows)
        {
            shadow.targetUI.SetActive(false);
            shadow.isShadowTurn = true;
        }
    }
    public void OnShadowTargeting()
    {
        foreach (var shadow in GameManager.Data.Battle.InBattleShadows)
        {
            shadow.isShadowTurn = false;
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
