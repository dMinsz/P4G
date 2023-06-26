using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlePersona : MonoBehaviour
{
    public PersonaData.Persona data;

    public Animator animator;

    private void Awake()
    {
        animator = transform.Find("Model").GetComponent<Animator>();
    }

    public void Attack() 
    {

    }

    public void UseSkill() 
    {

    }
}
