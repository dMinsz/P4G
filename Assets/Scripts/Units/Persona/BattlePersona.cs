using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BattlePersona : MonoBehaviour
{
    public enum AttackType
    {
        Attack,
        Magic
    }

    public PersonaData.Persona data;

    public Animator animator;

    public UnityEvent OnAttack;

    private void Awake()
    {
        animator = transform.Find("Model").GetComponent<Animator>();
    }

    public void Attack() 
    {
        animator.SetTrigger("Attack");
    }

    public void UseSkill() 
    {
        animator.SetTrigger("Skill");
    }
}
