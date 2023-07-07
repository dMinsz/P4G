using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kuma : MonoBehaviour
{
    public Animator animator;
    public AudioSource attackAudio;
    private void Awake()
    {
        attackAudio = transform.gameObject.GetComponent<AudioSource>();
        animator = transform.Find("Model").GetComponent<Animator>();

    }
}
