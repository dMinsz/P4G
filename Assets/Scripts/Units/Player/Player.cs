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

    public UnityEvent<int> OnHpChanged;
    public UnityEvent<int> OnSpChanged;

    public int HP { get { return curHp; } protected set { curHp = value; OnHpChanged?.Invoke(curHp); } }
    public int SP { get { return curSp; } protected set { curSp = value; OnSpChanged?.Invoke(curSp); } }


    public Animator animator;
    public List<Player> Partys = new List<Player>();


    public List<BattlePersona> Personas = new List<BattlePersona>();
    public Transform[] card;

    private void OnEnable()
    {
        animator = transform.Find("Model").GetComponent<Animator>();
        animator.SetFloat("MoveSpeed", 0f);
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
        var data = GameManager.Resource.Load<Player>("Datas/"+name);
        Partys.Add(data);
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
    public override void Attack(Vector3 attackPoint, Vector3 lookPoint)
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

    }

    public void UseSkill(Vector3 attackPoint, Vector3 lookPoint) 
    {

        GameManager.Data.Battle.commandQueue.Enqueue(new SummonsCommand(this, this.animator));
        GameManager.Data.Battle.commandQueue.Enqueue(new PersonaSkillCommand(Personas[0] , PersonaPoint, lookPoint));        
    }

    public override void TakeSkillDamage(ResType AttackType, int power, int critical, int hit)
    {
    }

  
}
