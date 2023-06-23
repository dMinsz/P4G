using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : Unit
{

    public Animator animator;

    private void Awake()
    {
        animator = transform.Find("Model").GetComponent<Animator>();
    }

    private void Start()
    {
        animator.Rebind();
    }

    public override void Attack()
    {
        
    }

    public override void TakeSkillDamage(ResType AttackType, int power, int critical, int hit)
    {
        
    }
}
