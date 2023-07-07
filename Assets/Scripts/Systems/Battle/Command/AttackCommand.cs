using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class AttackCommand : Command
{
    Unit actor;
    BattlePersona pactor;
    Unit target;
    Animator actorAnim;
    Animator targetAnim;
    Skill nowSkill;

    BattleCamSystem cam;

    public AttackCommand(Unit actor, Unit target,Animator actorAnimator, Animator targetAnimator , Skill nowSkill , BattleCamSystem cam) 
    {
        this.actor = actor;
        this.target = target;
        this.actorAnim = actorAnimator;
        this.targetAnim = targetAnimator;

        this.nowSkill = nowSkill;
        if (cam != null ) 
        {
            this.cam = cam;
        }
    }
    protected override async Task AsyncExecuter()
    {
        actor.soundSource.PlayOneShot(actor.attackSound);

        actorAnim.SetTrigger("Attack");
        await Task.Delay((int)actorAnim.GetCurrentAnimatorStateInfo(0).length * 1000);
        await Task.Delay(100);

        int camindex = GameManager.Data.Battle.InBattlePlayers.IndexOf(GameManager.Data.Battle.nowPlayer);

        if (actor != null)
        {
            if (cam != null)
            {
                cam.setEcam(camindex);
            }

            //await Task.Delay(100);
            //targetAnim.SetTrigger("Hit");
            target.TakeDamage(actor.data.Strength,nowSkill);

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
            //await Task.Delay(100);
            //targetAnim.SetTrigger("Hit");
            target.TakeDamage(actor.data.Strength, nowSkill);

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
        if (cam != null)
            cam.SetPlayerCam(camindex);

        await Task.Delay(100);
    }

   
}
