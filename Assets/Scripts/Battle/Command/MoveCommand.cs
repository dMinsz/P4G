using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class MoveCommand : Command
{
    Vector3 target;
    Transform unit;
    Animator animator;
    public MoveCommand(Vector3 target, Transform unit, Animator animimator)
    {
        this.target = target;
        this.unit = unit;
        this.animator = animimator;
    }


    protected override async Task AsyncExecuter()
    {
        //Vector3 tempOrigin = unit.position;
        //unit.transform.LookAt(target);
        while (unit.transform.position != target)
        {

            animator.SetFloat("MoveSpeed", 5f);
            unit.transform.position = Vector3.MoveTowards(unit.transform.position,
                                                            target,
                                                             0.10f);
            await Task.Delay(10);

            animator.SetFloat("MoveSpeed", 0f);
        
        }
        //unit.transform.LookAt(tempOrigin);
    }

}
