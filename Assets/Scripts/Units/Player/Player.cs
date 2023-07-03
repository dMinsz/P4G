using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;
using static PersonaData;
using static Skill;

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

    public ParticleSystem summonEffect;
    private void OnEnable()
    {
        animator = transform.Find("Model").GetComponent<Animator>();
        PersonaPoint = transform.Find("PersonaPoint");

        card = new Transform[2];
        card[0] = transform.Find("Model").Find("card_0_Part");
        card[1] = transform.Find("Model").Find("card_1_Part");
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
    public void Attack(Vector3 attackPoint, Vector3 lookPoint, Transform ui)
    {

        Vector3 OriginPos = transform.position;
        ////Move
        GameManager.Data.Battle.commandQueue.Enqueue(new LookCommand(lookPoint, this.transform));
        GameManager.Data.Battle.commandQueue.Enqueue(new MoveCommand(attackPoint, transform, animator));

        ////Attack
        GameManager.Data.Battle.commandQueue.Enqueue(new AttackCommand(this, GameManager.Data.Battle.nowShadow, animator, GameManager.Data.Battle.nowShadow.animator, GameManager.Data.Battle.nowSkill));
        ////ReturnBack
        GameManager.Data.Battle.commandQueue.Enqueue(new LookCommand(OriginPos, this.transform));
        GameManager.Data.Battle.commandQueue.Enqueue(new MoveCommand(OriginPos, transform, animator));

        GameManager.Data.Battle.commandQueue.Enqueue(new LookCommand(lookPoint, this.transform));
        GameManager.Data.Battle.commandQueue.Enqueue(new OffDamageUICommand());


    }

    public void UseSkill(Vector3 attackPoint, Shadow target, Transform ui, BattleCamSystem cam, Skill nowSkill)
    {

        var persona = GameManager.Data.Battle.nowPlayer.Personas[0];

        SetFrontCam(cam);
        GameManager.Data.Battle.commandQueue.Enqueue(new SummonsCommand(this, this.animator));
        GameManager.Data.Battle.commandQueue.Enqueue(new PersonaSkillCommand(persona, PersonaPoint, target, this, cam, nowSkill));
        GameManager.Data.Battle.commandQueue.Enqueue(new OffDamageUICommand());

    }

    public override void TakeDamage(int strength, Skill nowskill)
    {

        float rand = UnityEngine.Random.Range(0.95f, 1.06f);

        int DMG = (int)((strength * strength) / (data.Endurance * 1.5) * rand);

        if (!isDie)
        {
            HP -= DMG;

            if (HP < 0)
            {
                HP = 0;
                isDie = true;

                //animator.SetBool("IsDie", true);
                animator.SetTrigger("IsDie");
            }
        }
    }

    public override void TakeSkillDamage(Skill nowskill) 
    {
        float rand = UnityEngine.Random.Range(0.95f, 1.06f);
        int defenseFactor = 1;

        string resistname = Enum.GetName(typeof(ResType), nowskill.SkillType);


        Resistance res = Resistance.None;

        FieldInfo fld = typeof(Resistances).GetField(resistname);
        res = (Resistance)fld.GetValue(data.resist);

        if (res == Resistance.Null)
        {
            Debug.Log("공격이 안통함");
            return;
        }
        else if (res == Resistance.Strength)
        {
            defenseFactor = (int)(data.Magic * 1.6);
        }
        else if (res == Resistance.Weak)
        {
            defenseFactor = (int)(data.Magic * 0.5);
        }

        int DMG = (int)(nowskill.power / defenseFactor * rand);

        if (!isDie)
        {
           HP -= DMG;

            if (HP < 0)
            {
                HP = 0;
                isDie = true;
                //animator.SetBool("IsDie", true);
                animator.SetTrigger("IsDie");
            }
        }
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
