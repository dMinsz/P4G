using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class OffDamageUICommand : Command
{
    public OffDamageUICommand() 
    {
    }

    protected override async Task AsyncExecuter()
    {

        foreach (var damageui in GameManager.Data.Battle.uiHandler.DamageUIs)
        {
            if (damageui.gameObject.activeSelf == true) 
            {
                GameManager.UI.CloseInGameUI(damageui);
            }

        }
        
    }


}
