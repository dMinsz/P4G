using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class AttackCommand : Command
{
    Unit actor;
    Unit target;
    Animator animator;
    public AttackCommand(Unit actor,Unit target,Animator animator) 
    {
        this.actor = actor;
        this.target = target;
        this.animator = animator;
    }

    protected override async Task AsyncExecuter()
    {
   
        animator.SetTrigger("Attack");    
        target.TakeDamage(actor.data.Endurance,actor.data.Level);

        await Task.Delay(1500);
    }

   
}
