using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class FuncCommand : Command
{
     Action action;

    public FuncCommand(Action func)
    {
        action += func;
    }

    protected override async Task AsyncExecuter()
    {
        await Task.Delay(10);

        action?.Invoke();
    }

}
