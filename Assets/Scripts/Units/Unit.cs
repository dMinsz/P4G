using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public abstract class Unit : MonoBehaviour
{
    public UnitData data;

    protected bool isDie = false;

    public virtual void Attack(Vector3 AttackPoint, Vector3 LookPoint) { }

    public abstract void TakeDamage(int strength, Skill nowskill);


    public abstract void TakeSkillDamage(Skill nowskill); 
    
    public virtual void Die() 
    {
        if (isDie) 
        {
            //
        }
    }


}
