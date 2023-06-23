using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Unit 
{

    public Animator animator;
    public List<Player> Partys = new List<Player>();


    private void OnEnable()
    {
        animator = transform.Find("Model").GetComponent<Animator>();
        animator.SetFloat("MoveSpeed", 0f);
        //animator.StartPlayback();
        animator.Play("Idle");
    }

    private void OnDisable()
    {
       
    }
    public void AddParty(string name) 
    {
        var data = GameManager.Resource.Load<Player>("Datas/"+name);
        Partys.Add(data);
    }
    public void RemoveParty(string name)
    {
        foreach (var player in Partys) 
        {
            if (player.data.unitName == name)
            {
                Partys.Remove(player);
                return;
            }
        }
    }
    public override void Attack()
    {
    }

    public override void TakeSkillDamage(ResType AttackType, int power, int critical, int hit)
    {
    }

  
}
