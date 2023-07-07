using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingUI : MonoBehaviour
{
    [SerializeField] Slider slider;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void FadeOut()
    {
        animator.SetTrigger("FadeOut");
    }

    public void SetProgress(float progress)
    {
        slider.value = progress;
    }
}
