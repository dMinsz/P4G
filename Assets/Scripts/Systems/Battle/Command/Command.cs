using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public abstract class Command 
{
    bool _isExecuting = false;

    public bool isExecuting { get => _isExecuting; }

    public async void Execute() 
    {
        _isExecuting = true;
        await AsyncExecuter();
        _isExecuting = false;
    }

    protected abstract Task AsyncExecuter();

}
