using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : Unit
{

    Animator animator;

    private void Awake()
    {
        animator = transform.Find("Model").GetComponent<Animator>();
    }

    public override void Attack()
    {
        
    }

    public override void TakeSkillDamage(ResType AttackType, int power, int critical, int hit)
    {
        
    }
}
