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

    public BattleEndPage end;

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

    public void OnEscape() 
    {
        uiHandler.CleanUpListUI();
        EndBattle();
    }

    public void OnGuard() 
    {
        uiHandler.CleanUpListUI();

        GameManager.Data.Battle.commandQueue.Enqueue(new UICommand(uiHandler.MenuUI.transform, false));
        GameManager.Data.Battle.commandQueue.Enqueue(new UICommand(uiHandler.SelectMenuUI.transform, false));
        GameManager.Data.Battle.commandQueue.Enqueue(new UICommand(uiHandler.partyUI.transform, false));

        GameManager.Data.Battle.commandQueue.Enqueue(new GuardCommand(nowPlayer));
        GameManager.Data.Battle.commandQueue.Enqueue(new FuncCommand(NextPlayer));
    }


    public void OnAnalysis()
    {
        uiHandler.CleanUpListUI();

        uiHandler.AnalysisingUI.gameObject.SetActive(true);

        var obj = GameManager.UI.ShowInGameUI(AttributeUIPrefab);
        obj.ResetData();
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
        if (!nowShadow.isDie)
        {
            PlayerAttack();
        }
        else
        {
            //죽지않은 섀도우 찾기
            NextShadow();

            if (nowShadow == null)
            {
                
                //game End
                Debug.Log("Player 승리");
                end.Show("YOU WIN");

            }

            PlayerAttack();
        }
    }

    private void PlayerAttack()
    {
        GameManager.Data.Battle.commandQueue.Enqueue(new UICommand(uiHandler.MenuUI.transform, false));
        GameManager.Data.Battle.commandQueue.Enqueue(new UICommand(uiHandler.SelectMenuUI.transform, false));
        GameManager.Data.Battle.commandQueue.Enqueue(new UICommand(uiHandler.partyUI.transform, false));

        nowPlayer.Attack(nowShadow.attackPoint.position, nowShadow.transform.position, uiHandler.BattleUI.transform);

        GameManager.Data.Battle.commandQueue.Enqueue(new FuncCommand(NextPlayer));
    }

    public void OnPlayerUsePersonaAttack()
    {
        if (!nowShadow.isDie)
        {
            PlayerPersonaAttack();
        }
        else
        {
            NextShadow();

            if (nowShadow == null)
            {
                //ToDO
                //game End

                Debug.Log("Player 승리");
                end.Show("YOU WIN");
            }

            PlayerPersonaAttack();
        }

    }

    private void PlayerPersonaAttack()
    {
        GameManager.Data.Battle.commandQueue.Enqueue(new UICommand(uiHandler.MenuUI.transform, false));
        GameManager.Data.Battle.commandQueue.Enqueue(new UICommand(uiHandler.SelectMenuUI.transform, false));
        GameManager.Data.Battle.commandQueue.Enqueue(new UICommand(uiHandler.partyUI.transform, false));

        nowPlayer.UseSkill(nowShadow.attackPoint.position, nowShadow, uiHandler.BattleUI.transform, cam, nowSkill);

        GameManager.Data.Battle.commandQueue.Enqueue(new FuncCommand(NextPlayer));
    }

    bool isTurnChange = true;
    public void NextPlayer()
    {
        if (CheckAllShadowDied() == true)
        {
            //Player Win

            Debug.Log("Player win");
            end.Show("YOU WIN");
            //change Scene etc

            return;
        }

        if (CheckAllPlayerDied() == true)
        {
            //Player Lost

            Debug.Log("Player Lost");

            end.Show("YOU LOSE");

            //change Scene etc

            return;
        }


        int nowIndex = GameManager.Data.Battle.InBattlePlayers.IndexOf(GameManager.Data.Battle.nowPlayer);
        int nowCount = nowIndex + 1;
        int MaxCount = GameManager.Data.Battle.InBattlePlayers.Count;

        if (isTurnChange == true)
        {
            if (nowCount >= MaxCount)
            {
                TurnRoutine = StartCoroutine(EnemyTurnRoutine());
                turnCount++;
                return;
            }
        }
        else 
        {
            turnCount++;

            if (nowCount >= MaxCount)
            {
                GameManager.Data.Battle.nowPlayer = GameManager.Data.Battle.InBattlePlayers[GameManager.Data.Battle.InBattlePlayers.Count-1];


                uiHandler.ChangeCommandUI(); // 파티에 커맨드 표시 바꾸기
                cam.nextPlayer(); // 캠변환


                GameManager.Data.Battle.commandQueue.Enqueue(new UICommand(uiHandler.SelectMenuUI.transform, true));
                GameManager.Data.Battle.commandQueue.Enqueue(new UICommand(uiHandler.partyUI.transform, true));

                LookSetUp();


                return;
            }
        }


        for (int i = nowIndex; i < MaxCount; i++)
        {
            if (IsDiedPlayer(nowIndex + 1))
            {
                nowIndex++;
                continue;
            }

            GameManager.Data.Battle.nowPlayer = GameManager.Data.Battle.InBattlePlayers[++nowIndex];


            uiHandler.ChangeCommandUI(); // 파티에 커맨드 표시 바꾸기
            cam.nextPlayer(); // 캠변환


            GameManager.Data.Battle.commandQueue.Enqueue(new UICommand(uiHandler.SelectMenuUI.transform, true));
            GameManager.Data.Battle.commandQueue.Enqueue(new UICommand(uiHandler.partyUI.transform, true));

            LookSetUp();

            return;
        }

        //여기까지오면 다음 플레이어가없는것
        TurnRoutine = StartCoroutine(EnemyTurnRoutine());
        turnCount++;


    }

    private bool IsDiedPlayer(int index)
    {
        if (index >= GameManager.Data.Battle.InBattlePlayers.Count)
        {
            return true;
        }

        if (GameManager.Data.Battle.InBattlePlayers[index].isDie == true)
        {
            return true;
        }
        return false;
    }

    private bool CheckAllShadowDied()
    {
        int dieCount = 0;
        for (int i = 0; i < InBattleShadows.Count; i++)
        {
            if (InBattleShadows[i].isDie)
            {
                dieCount++;
            }
        }

        if (dieCount == InBattleShadows.Count)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    private void NextShadow()
    {
        var index = InBattleShadows.IndexOf(nowShadow);
        for (int i = index; i < InBattleShadows.Count; i++)
        {
            nowShadow = InBattleShadows[(index + 1) % InBattleShadows.Count];

            if (!nowShadow.isDie)
            {
                break;
            }
            else
            {
                nowShadow = null;
            }
        }
    }

    private bool CheckAllPlayerDied()
    {
        int dieCount = 0;
        for (int i = 0; i < InBattlePlayers.Count; i++)
        {
            if (InBattlePlayers[i].isDie)
            {
                dieCount++;
            }
        }

        if (dieCount == InBattlePlayers.Count)
        {
            return true;
        }
        else
        {
            return false;
        }
    }





    Coroutine TurnRoutine;


    IEnumerator EnemyTurnRoutine()
    {

        Debug.Log("Enemy Turn Routine Start");
        uiHandler.RemoveCommandUI();

        //PartyUI 끄기
        GameManager.Data.Battle.commandQueue.Enqueue(new UICommand(uiHandler.partyUI.transform, true));
        GameManager.Data.Battle.commandQueue.Enqueue(new UICommand(uiHandler.MenuUI.transform, false));


        bool GameDone = false;

        foreach (var shadow in GameManager.Data.Battle.InBattleShadows)
        {//랜덤공격 세팅
            if (!shadow.isDie)
            {
                GameDone = ShadowAttack(shadow);
            }
        }

        while (GameDone)
        {
            if (GameManager.Data.Battle.commandQueue.Count > 0)
            {   //공격이 끝날때까지 기다린다.
                yield return null;
            }
            else
            {

                yield return new WaitForSeconds(1.0f);
                ShadowSetting();

                turnCount++;
                Debug.Log("Enemy Turn Routine Done");
                yield break;
            }
        }
    }
    private bool ShadowAttack(Shadow shadow) 
    {

        if (CheckAllPlayerDied() == true)
        {
            //Player Lost
            Debug.Log("Player Lost");
            end.Show("YOU LOSE");
            return false;
        }
        GameManager.Data.Battle.commandQueue.Enqueue(new FuncCommand(OffShadowTargeting));

        GameManager.Data.Battle.commandQueue.Enqueue(new FuncCommand(SetRandomPlayer));

        GameManager.Data.Battle.commandQueue.Enqueue(new FuncCommand(shadow.GetComponent<Shadow>().Attack));

        return true;
    }

    private void ShadowSetting() 
    {
        //targeting
        GameManager.Data.Battle.commandQueue.Enqueue(new FuncCommand(OnShadowTargeting));

        //default Seting // need is Dead check
        GameManager.Data.Battle.commandQueue.Enqueue(new FuncCommand(ShadowTurnDoneSetting));

        GameManager.Data.Battle.commandQueue.Enqueue(new UICommand(uiHandler.BattleUI.transform, true));
        GameManager.Data.Battle.commandQueue.Enqueue(new UICommand(uiHandler.SelectMenuUI.transform, true));
        GameManager.Data.Battle.commandQueue.Enqueue(new UICommand(uiHandler.partyUI.transform, true));

    }

    public void SetRandomPlayer() 
    {
        int rand = Random.Range(0, GameManager.Data.Battle.InBattlePlayers.Count);

        var randomPlayer = GameManager.Data.Battle.InBattlePlayers[rand];

        if (randomPlayer.isDie) 
        {

            for (int i = 0; i < GameManager.Data.Battle.InBattlePlayers.Count; i++)
            {
                if (IsDiedPlayer(i))
                {
                    continue;
                }

                randomPlayer = GameManager.Data.Battle.InBattlePlayers[i];
                break;
            }
        }
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

    private void ShadowTurnDoneSetting() 
    {
        nowPlayer = InBattlePlayers[0];

        nowShadow = InBattleShadows[0];


        if (nowShadow == null || nowShadow.isDie == true)
        {
            NextShadow();
        }


        if (nowShadow == null) 
        {
            //playe win
            Debug.Log("Player Win");
            end.Show("YOU WIN");
            return;
        }

        nowShadow.targetUI.gameObject.SetActive(true);

        nowPersona = InBattlePlayers[0].Personas[InBattlePlayers[0].nowPersonaIndex];


        if (nowPlayer.isDie == true)
        {
            isTurnChange = false;
            NextPlayer();
            isTurnChange = true;

        }
        else 
        {
            cam.SetPlayerCam(0);
            uiHandler.ChangeCommandUI();

            LookSetUp();
        }
    }


    public void EndBattle()
    {
        var nowSymbol = GameManager.Data.Dungeon.nowSymbol;
        GameManager.Data.Dungeon.aliveInDungeonSymbols.Remove(nowSymbol);
        GameManager.Data.Dungeon.nowSymbol = null;

        GameManager.Pool.erasePoolDicContet(nowSymbol.gameObject.name);
        GameManager.Pool.eraseContainerContet(nowSymbol.gameObject.name);
        GameManager.Pool.DestroyContainer(nowSymbol);

        GameManager.Scene.LoadScene(GameManager.Data.Dungeon.nowDungeon.DungeonName);
    }

}
