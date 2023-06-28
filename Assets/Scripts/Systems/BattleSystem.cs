using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class BattleSystem : MonoBehaviour
{
    public enum PersonaAttackType 
    {
        None,
        Attack,
        Skill,
        Size
    }

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

    public PersonaAttackType personaAttackType;

    private void Awake()
    {
        turnType = GameManager.Data.Dungeon.StartTurn;
        commandQueue = new Queue<Command>();
        cam = GetComponent<BattleCamSystem>();
    }

    private void Start()
    {
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
    }


    public void OnPlayerAttack() 
    {
        uiHandler.MenuUI.gameObject.SetActive(false);
        uiHandler.BattleUI.gameObject.SetActive(false);
        nowPlayer.Attack(nowShadow.attackPoint.position,nowShadow.transform.position, uiHandler.BattleUI.transform);
        //NextPlayer();
        GameManager.Data.Battle.commandQueue.Enqueue(new FuncCommand(NextPlayer));
    }

    public void OnPlayerUsePersonaAttack() 
    {
        uiHandler.MenuUI.gameObject.SetActive(false);
        uiHandler.BattleUI.gameObject.SetActive(false);
        nowPlayer.UseSkill(nowShadow.attackPoint.position, nowShadow.transform.position, uiHandler.BattleUI.transform,personaAttackType, cam);

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
            cam.nextPlayer();
            LookSetUp();
            //return true;
        }
        else 
        {

            //text Code
            var index = (GameManager.Data.Battle.InBattlePlayers.IndexOf(GameManager.Data.Battle.nowPlayer)+1) % GameManager.Data.Battle.InBattlePlayers.Count;
            GameManager.Data.Battle.nowPlayer = GameManager.Data.Battle.InBattlePlayers[index];
            
            //text Code
            //uiHandler.MenuUI.gameObject.SetActive(false);
            //uiHandler.BattleUI.gameObject.SetActive(false);

            //text Code
            cam.nextPlayer();
            LookSetUp();


            //real Code
            //EnemyTurn();
            
        }


    }

    public void EnemyTurn() 
    {
        Debug.Log("Enemy Turn Start");
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
