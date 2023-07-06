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
    Skill nowSkill;
    public AttackCommand(Unit actor, Unit target,Animator actorAnimator, Animator targetAnimator , Skill nowSkill) 
    {
        this.uactor = actor;
        this.target = target;
        this.actorAnim = actorAnimator;
        this.targetAnim = targetAnimator;

        this.nowSkill = nowSkill;
    }
    protected override async Task AsyncExecuter()
    {
   
        actorAnim.SetTrigger("Attack");
        await Task.Delay((int)actorAnim.GetCurrentAnimatorStateInfo(0).length * 1000);
        await Task.Delay(100);

        if (uactor != null)
        {

            await Task.Delay(100);
            //targetAnim.SetTrigger("Hit");
            target.TakeDamage(uactor.data.Strength,nowSkill);

            if (target.isDie)
            {
                targetAnim.SetTrigger("IsDie");
            }
            else
            {
                targetAnim.SetTrigger("Hit");
            }

            await Task.Delay((int)targetAnim.GetCurrentAnimatorStateInfo(0).length * 1000);

        }
        else if (pactor != null)
        {
            await Task.Delay(100);
            //targetAnim.SetTrigger("Hit");
            target.TakeDamage(uactor.data.Strength, nowSkill);

            if (target.isDie)
            {
                targetAnim.SetTrigger("IsDie");
            }
            else 
            {
                targetAnim.SetTrigger("Hit");
            }

            await Task.Delay((int)targetAnim.GetCurrentAnimatorStateInfo(0).length * 1000);

        }
        else 
        {
            Debug.LogError("Attack Command Error");
        }

       

        await Task.Delay(100);
    }

   
}
