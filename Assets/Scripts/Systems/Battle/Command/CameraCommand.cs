using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class CameraCommand : Command
{

    int playerIndex;
    bool isFront;

    public CameraCommand(int playerIndex,bool isFront) 
    {
        this.playerIndex = playerIndex;
        this.isFront = isFront;
    }

    protected override async Task AsyncExecuter()
    {




        await Task.Delay(10);

    }
}
