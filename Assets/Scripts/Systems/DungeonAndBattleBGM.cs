using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonAndBattleBGM : MonoBehaviour
{
    public AudioSource audioSource;
    float PlayeBackTime;
    public void Play()
    {
        this.gameObject.SetActive(true);
        audioSource = GameManager.Resource.Instantiate<AudioSource>("Sound/BattleSounder");
        audioSource.time = PlayeBackTime;
        audioSource.Play();
    }

    public void Stop()
    {
        PlayeBackTime = audioSource.time;
        audioSource.Stop();
    }

    public void ResetAudio()
    {
        PlayeBackTime = 0f;
    }

}
