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
    Animator actorAnim;
    Animator targetAnim;
    public AttackCommand(Unit actor, Unit target,Animator actorAnimator, Animator targetAnimator) 
    {
        this.uactor = actor;
        this.target = target;
        this.actorAnim = actorAnimator;
        this.targetAnim = targetAnimator;
    }
    protected override async Task AsyncExecuter()
    {
   
        actorAnim.SetTrigger("Attack");
        await Task.Delay((int)actorAnim.GetCurrentAnimatorStateInfo(0).length * 1000);
        await Task.Delay(100);

        if (uactor != null)
        {

            await Task.Delay(100);
            targetAnim.SetTrigger("Hit");
            await Task.Delay((int)targetAnim.GetCurrentAnimatorStateInfo(0).length * 1000);


            target.TakeDamage(uactor.data.Strength);
        }
        else if (pactor != null)
        {
            await Task.Delay(100);
            targetAnim.SetTrigger("Hit");

            await Task.Delay((int)targetAnim.GetCurrentAnimatorStateInfo(0).length * 1000);

            target.TakeDamage(uactor.data.Strength);
        }
        else 
        {
            Debug.LogError("Attack Command Error");
        }

       

        await Task.Delay(100);
    }

   
}
