using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : Unit 
{
    public int MaxHp;
    public int curHp;

    public int MaxSp;
    public int curSp;


    public UnityEvent<int> OnHpChanged;
    public UnityEvent<int> OnSpChanged;

    public int HP { get { return curHp; } protected set { curHp = value; OnHpChanged?.Invoke(curHp); } }
    public int SP { get { return curSp; } protected set { curSp = value; OnSpChanged?.Invoke(curSp); } }


    public Animator animator;
    public List<Player> Partys = new List<Player>();


    private void OnEnable()
    {
        animator = transform.Find("Model").GetComponent<Animator>();
        animator.SetFloat("MoveSpeed", 0f);
        //animator.StartPlayback();
        animator.Play("Idle");
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
    public override void Attack()
    {
    }

    public override void TakeSkillDamage(ResType AttackType, int power, int critical, int hit)
    {
    }

  
}
