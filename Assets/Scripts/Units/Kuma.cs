using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kuma : MonoBehaviour
{
    public Animator animator;
    private void Awake()
    {
        animator = transform.Find("Model").GetComponent<Animator>();

    }
}
