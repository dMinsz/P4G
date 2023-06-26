using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class SummonsCommand : Command
{

    Player player;
    Animator playerAnim;

    public SummonsCommand(Player Player, Animator PlayerAnimator)
    {
        this.player = Player;
        this.playerAnim = PlayerAnimator;
    }

    protected override async Task AsyncExecuter()
    {

        player.card[0].gameObject.SetActive(true);
        player.card[1].gameObject.SetActive(true);

        playerAnim.SetTrigger("Persona");
        await Task.Delay((int)playerAnim.GetCurrentAnimatorStateInfo(0).length * 1000);

        player.card[0].gameObject.SetActive(false);
        player.card[1].gameObject.SetActive(false);
    }
}