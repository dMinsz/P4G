using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BattleEndPage : MonoBehaviour
{
    public TMP_Text EndMessage;


    public void Show(string message) 
    {
        EndMessage.text = message;
        this.gameObject.SetActive(true);
    }

    public void Close() 
    {
        this.gameObject.SetActive(false);
    }

}
