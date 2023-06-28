using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NowCommandUI : MonoBehaviour
{
    public void SetUp(bool isCommand) 
    {
        if (isCommand)
        {
            transform.gameObject.SetActive(true);
        }
        else 
        {
            transform.gameObject.SetActive(false);
        }
    }
}
