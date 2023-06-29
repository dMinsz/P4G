using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static PersonaData;

public class Player : Unit 
{
    public int MaxHp;
    public int curHp;

    public int MaxSp;
    public int curSp;

    public Transform PersonaPoint;

    public UnityEvent<int> OnHpChanged = new UnityEvent<int>();
    public UnityEvent<int> OnSpChanged = new UnityEvent<int>();

    public int HP { get { return curHp; } set { curHp = value; OnHpChanged?.Invoke(curHp); } }
    public int SP { get { return curSp; } set { curSp = value; OnSpChanged?.Invoke(curSp); } }


    public Animator animator;
    public List<Player> Partys = new List<Player>();

    public int nowPersonaIndex = 0;
    public List<BattlePersona> Personas = new List<BattlePersona>();
    public Transform[] card;

    private void OnEnable()
    {
        animator = transform.Find("Model").GetComponent<Animator>();
       // animator.SetFloat("MoveSpeed", 0f);
        PersonaPoint = transform.Find("PersonaPoint");

        card = new Transform[2];
        card[0] = transform.Find("Model").Find("card_0_Part");
        card[1] = transform.Find("Model").Find("card_1_Part");

        ////animator.StartPlayback();
        //animator.Play("Idle");
    }

    private void OnDisable()
    {
       
    }
    public void AddParty(string name) 
    {
        //todo Change
        //Debug.Log("Datas/Players/" + name);

        var data = GameManager.Resource.Load<PlayerData>("Datas/Players/" + name);
        var ally = GameManager.Pool.Get(true, data.player.Prefab.GetComponent<Player>());

        ally.data = data.player;

        ally.MaxHp = data.player.Hp;
        ally.MaxSp = data.player.Sp;
        ally.curHp = data.player.Hp;
        ally.curSp = data.player.Sp;

        GameManager.Pool.Release(ally);
        Partys.Add(ally);
    }
    public void RemoveParty(string name)
    {
        foreach (var player in Partys) 
        {
            if (player.data.unitName == name)
            {
                Partys.Remove(player);
                return;
            }
        }
    }
    public void Attack(Vector3 attackPoint, Vector3 lookPoint , Transform ui)
    {
        Vector3 OriginPos = transform.position;
        ////Move
        GameManager.Data.Battle.commandQueue.Enqueue(new LookCommand(lookPoint, this.transform));
        GameManager.Data.Battle.commandQueue.Enqueue(new MoveCommand(attackPoint, transform , animator));

        ////Attack
        GameManager.Data.Battle.commandQueue.Enqueue(new AttackCommand(this, GameManager.Data.Battle.nowShadow, animator));

        ////ReturnBack
        GameManager.Data.Battle.commandQueue.Enqueue(new LookCommand(OriginPos, this.transform));
        GameManager.Data.Battle.commandQueue.Enqueue(new MoveCommand(OriginPos, transform, animator));

        GameManager.Data.Battle.commandQueue.Enqueue(new LookCommand(lookPoint, this.transform));

        //GameManager.Data.Battle.commandQueue.Enqueue(new UICommand(ui, true));
    }

    public void UseSkill(Vector3 attackPoint, Vector3 lookPoint , Transform ui , BattleSystem.PersonaAttackType type , BattleCamSystem cam) 
    {

        var persona = GameManager.Data.Battle.nowPlayer.Personas[0];

        SetFrontCam(cam);
        GameManager.Data.Battle.commandQueue.Enqueue(new SummonsCommand(this, this.animator));
        GameManager.Data.Battle.commandQueue.Enqueue(new PersonaSkillCommand(persona, PersonaPoint,lookPoint, type , this , cam));
        //GameManager.Data.Battle.commandQueue.Enqueue(new UICommand(ui, true));
    }

    public override void TakeSkillDamage(ResType AttackType, int power, int critical, int hit)
    {
    }


    public void SetFrontCam(BattleCamSystem cam) 
    {
        if (GameManager.Data.Battle.InBattlePlayers.IndexOf(this) == 0)
        {
            cam.setPlayer1(true);
        }
        else if (GameManager.Data.Battle.InBattlePlayers.IndexOf(this) == 1)
        {
            cam.setPlayer2(true);
        }
        else if (GameManager.Data.Battle.InBattlePlayers.IndexOf(this) == 2)
        {
            cam.setPlayer3(true);
        }
    }

}
