using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

//������ ����Ѵ�.
public class WaitFuncCommand : Command
{
    Action action;

    public WaitFuncCommand(Action func)
    {
        action += func;
    }

    protected override async Task AsyncExecuter()
    {

        while (GameManager.Data.Battle.commandQueue.Count > 0) 
        {
            await Task.Delay(100);
            action?.Invoke();
        }
        

    }

}
