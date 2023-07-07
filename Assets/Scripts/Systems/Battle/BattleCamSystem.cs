using Cinemachine;
using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCamSystem : MonoBehaviour
{
    public List<CinemachineVirtualCamera> Cams;
    public List<CinemachineVirtualCamera> ECams;

    public void ResetCams()
    {
        foreach (var cam in Cams) 
        {
            cam.Priority = 1;
        }

    }

    public void ResetECams() 
    {
        foreach (var cam in ECams)
        {
            cam.Priority = 1;
        }
    }

    public void setEcam(int index) 
    {
        ResetCams();
        ResetECams();

        ECams[index].Priority = 2;
    }

    public void setPlayer1(bool isFront) 
    {
        ResetCams();

        if (isFront)
        {
            Cams[1].Priority = 2;
        }
        else 
        {
            Cams[0].Priority = 2;
        }
    }

    public void setPlayer2(bool isFront)
    {
        ResetCams();

        if (isFront)
        {
            Cams[3].Priority = 2;
        }
        else
        {
            Cams[2].Priority = 2;
        }
    }
    public void setPlayer3(bool isFront)
    {
        ResetCams();

        if (isFront)
        {
            Cams[5].Priority = 2;
        }
        else
        {
            Cams[4].Priority = 2;
        }
    }
    
    
    public void nextPlayer() 
    {
        ResetCams();

        int index = GameManager.Data.Battle.InBattlePlayers.IndexOf(GameManager.Data.Battle.nowPlayer) % GameManager.Data.Battle.InBattlePlayers.Count;

        // 0,2,4
        Cams[(index * 2)].Priority = 2;

    }

    public void SetPlayerCam(int indexPlayer) 
    {
        ResetCams();

        Cams[(indexPlayer * 2)].Priority = 2;
    }

}
