using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Door : MonoBehaviour
{





    public void OpenDoor() 
    {
        var newPos = this.gameObject.transform.position;
        newPos.y = 9f;
        this.gameObject.transform.position = newPos;
    }
}
