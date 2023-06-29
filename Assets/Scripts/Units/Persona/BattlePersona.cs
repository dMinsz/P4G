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

    public Animator animator = new Animator();

    public UnityEvent OnAttack;

    public ParticleSystem summonEffect;

    public ParticleSystem skillEffect;
    public ParticleSystem attackEffect;

    private void Awake()
    {
        animator = transform.Find("Model").GetComponent<Animator>();
    }

    public void Attack() 
    {
        summonEffect.Play();
        animator.SetTrigger("Attack");
    }

    public void UseSkill() 
    {
        summonEffect.Play();
        animator.SetTrigger("Skill");
    }
}
