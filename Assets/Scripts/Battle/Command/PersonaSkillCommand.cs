using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using static PersonaData;

public class PersonaSkillCommand : Command
{
    BattlePersona persona;
    Transform summonPoint;
    Vector3 lookTarget;
    BattleSystem.PersonaAttackType type;
    Player player;
    BattleCamSystem cam;
    public PersonaSkillCommand(BattlePersona persona , Transform summonPoint, Vector3 lookTarget,  BattleSystem.PersonaAttackType type , Player player , BattleCamSystem cam)
    {
        this.persona = persona;
        this.summonPoint = summonPoint;
        this.lookTarget = lookTarget;

        this.type = type;
        this.player = player;
        this.cam = cam;
    }
    protected override async Task AsyncExecuter()
    {

        var pobj = GameManager.Pool.Get(false, persona, summonPoint.position, Quaternion.identity);
        pobj.transform.LookAt(lookTarget);

        await Task.Delay(1000);

        SetBackCam(this.cam);


        if (type == BattleSystem.PersonaAttackType.Attack)
        {

            pobj.Attack();
        }
        else 
        {
            pobj.UseSkill();
        }
        
        await Task.Delay((int)pobj.animator.GetCurrentAnimatorStateInfo(0).length * 1000);
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