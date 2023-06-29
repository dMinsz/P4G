using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using static PersonaData;

public class PersonaSkillCommand : Command
{
    BattlePersona persona;
    Transform summonPoint;
    BattleSystem.PersonaAttackType type;
    Player player;
    BattleCamSystem cam;
    Shadow target;
    public PersonaSkillCommand(BattlePersona persona , Transform summonPoint, Shadow target,  BattleSystem.PersonaAttackType type , Player player , BattleCamSystem cam)
    {
        this.persona = persona;
        this.summonPoint = summonPoint;
        this.target = target;

        this.type = type;
        this.player = player;
        this.cam = cam;
    }
    protected override async Task AsyncExecuter()
    {

        var pobj = GameManager.Pool.Get(false, persona, summonPoint.position, Quaternion.identity);
        pobj.transform.LookAt(target.transform);

        await Task.Delay(1000);

        SetBackCam(this.cam);


        if (type == BattleSystem.PersonaAttackType.Attack)
        {
            pobj.Attack();

            //await Task.Delay((int)pobj.animator.GetCurrentAnimatorStateInfo(0).length * 100);
            //await Task.Delay(500);
            var pos = this.target.transform.position;
            pos.y += 2;

            pobj.attackEffect.transform.position = pos;
            
            pobj.attackEffect.Play();

            await Task.Delay(1000);
            //testing
            target.animator.SetTrigger("Hit");

        }
        else 
        {

            pobj.UseSkill();

            //await Task.Delay((int)pobj.animator.GetCurrentAnimatorStateInfo(0).length * 100);
            //await Task.Delay(500);
            var pos = this.target.transform.position;
            pos.y += 2;

            pobj.skillEffect.transform.position = pos;
            pobj.skillEffect.Play();

            //testing
            await Task.Delay(1000);
            target.animator.SetTrigger("Hit");

        }

        await Task.Delay((int)pobj.animator.GetCurrentAnimatorStateInfo(0).length * 1000);
        pobj.skillEffect.Stop();
        await Task.Delay(1000);

        GameManager.Pool.Release(pobj);
    }

    public void SetBackCam(BattleCamSystem cam)
    {
        if (GameManager.Data.Battle.InBattlePlayers.IndexOf(player) == 0)
        {
            cam.setPlayer1(false);
        }
        else if (GameManager.Data.Battle.InBattlePlayers.IndexOf(player) == 1)
        {
            cam.setPlayer2(false);
        }
        else if (GameManager.Data.Battle.InBattlePlayers.IndexOf(player) == 2)
        {
            cam.setPlayer3(false);
        }

    }
}