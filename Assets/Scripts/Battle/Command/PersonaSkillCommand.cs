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
    public PersonaSkillCommand(BattlePersona persona , Transform summonPoint , Vector3 lookTarget , BattleSystem.PersonaAttackType type )
    {
        this.persona = persona;
        this.summonPoint = summonPoint;
        this.lookTarget = lookTarget;
        this.type = type;
    }
    protected override async Task AsyncExecuter()
    {
        var pobj = GameManager.Pool.Get(false, persona, summonPoint.position, Quaternion.identity);


        if (type == BattleSystem.PersonaAttackType.Attack)
        {

            pobj.Attack();
        }
        else 
        {
            pobj.UseSkill();
        }
        
        await Task.Delay((int)pobj.animator.GetCurrentAnimatorStateInfo(0).length * 1000);

        GameManager.Pool.Release(pobj);
    }
}