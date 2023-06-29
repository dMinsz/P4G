using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class FillmCommand : Command
{
    BattleCamSystem cam;
    Player target;
    public FillmCommand(BattleCamSystem cam, Player target)
    {
        this.cam = cam;
        this.target = target;
    }

    protected override async Task AsyncExecuter()
    {
        await Task.Delay(10);
        var index = GameManager.Data.Battle.InBattlePlayers.IndexOf(target);
        cam.SetPlayerCam(index);
    }

}