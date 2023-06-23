using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Shadow : Unit
{

    public int Maxhp;
    public int curHp;

    public int MaxSp;
    public int curSp;


    public UnityEvent<int> OnHpChanged;
    public UnityEvent<int> OnSpChanged;

    public int HP { get { return curHp; } protected set { curHp = value; OnHpChanged?.Invoke(curHp); } }
    public int SP { get { return curSp; } protected set { curSp = value; OnSpChanged?.Invoke(curSp); } }

    public Animator animator;

    private void Awake()
    {
        animator = transform.Find("Model").GetComponent<Animator>();
    }

    private void Start()
    {
    }

    public override void Attack()
    {
        
    }

    public override void TakeSkillDamage(ResType AttackType, int power, int critical, int hit)
    {
        
    }
}
