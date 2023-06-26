using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class AttackCommand : Command
{
    Unit uactor;
    BattlePersona pactor;
    Unit target;
    Animator animator;
    public AttackCommand(Unit actor, Unit target,Animator animator) 
    {
        this.uactor = actor;
        this.target = target;
        this.animator = animator;
    }
    public AttackCommand(BattlePersona actor, Unit target, Animator animator)
    {
        this.pactor = actor;
        this.target = target;
        this.animator = animator;
    }
    protected override async Task AsyncExecuter()
    {
   
        animator.SetTrigger("Attack");


        if (uactor != null)
        {
            target.TakeDamage(uactor.data.Endurance, uactor.data.Level);
        }
        else if (pactor != null)
        {
            target.TakeDamage(pactor.data.Endurance, pactor.data.Level);
        }
        else 
        {
            Debug.LogError("Attack Command Error");
        }

       

        await Task.Delay(1500);
    }

   
}
