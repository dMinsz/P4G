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
    public PersonaSkillCommand(BattlePersona persona , Transform summonPoint , Vector3 lookTarget )
    {
        this.persona = persona;
        this.summonPoint = summonPoint;
        this.lookTarget = lookTarget;
    }
    protected override async Task AsyncExecuter()
    {
        var pobj = GameManager.Pool.Get(false, persona, summonPoint.position, Quaternion.identity);

        GameManager.Data.Battle.commandQueue.Enqueue(new LookCommand(lookTarget, pobj.transform));
        pobj.animator.SetTrigger("Attack");

        await Task.Delay((int)pobj.animator.GetCurrentAnimatorStateInfo(0).length * 1000);

        GameManager.Pool.Release(pobj);
    }
}