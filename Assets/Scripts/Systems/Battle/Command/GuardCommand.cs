using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class GuardCommand : Command
{

    Player player;
    public GuardCommand(Player player) 
    {
        this.player = player;
    }

    protected override async Task AsyncExecuter()
    {
        player.isGuard = true;
        player.animator.SetBool("IsGuard", true);

        await Task.Delay(100);

    }

}
