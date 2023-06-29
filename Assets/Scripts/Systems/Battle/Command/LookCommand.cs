using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class LookCommand : Command
{
    Vector3 target;
    Transform unit;

    public LookCommand(Vector3 target, Transform unit)
    {
        this.target = target;
        this.unit = unit;
        
    }


    protected override async Task AsyncExecuter()
    {
        unit.LookAt(target);
        await Task.Delay(10); 
    }


}
