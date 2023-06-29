using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class UICommand : Command
{
    Transform UI;
    bool IsShow;

    public UICommand(Transform UI, bool isShow)
    {
        this.UI = UI;
        IsShow = isShow;
    }

    protected override async Task AsyncExecuter()
    {
        await Task.Delay(10);
        if (IsShow)
        {
            UI.gameObject.SetActive(true);

        }
        else 
        {
            UI.gameObject.SetActive(false);
        }
    }
}
