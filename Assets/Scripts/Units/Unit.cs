using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    public UnitData data;

    bool isDie = false;

    public virtual void Attack(Vector3 AttackPoint, Vector3 LookPoint) { }

    public virtual void TakeDamage(int AttackerPower, int hits = 1) 
    {
        float rand = UnityEngine.Random.Range(0.95f, 1.06f);

        int DMG = (int)((AttackerPower * AttackerPower) / (data.Endurance * 1.5) * rand);

        if (!isDie) 
        {
            data.Hp -= DMG;

            if (data.Hp < 0)
            {
                data.Hp = 0;
                isDie = true;
            }
        }
    }

    public virtual void TakeSkillDamage(ResType AttackType, int power, int critical, int hit) 
    {
        float rand = UnityEngine.Random.Range(0.95f, 1.06f);
        int defenseFactor = 1;

        string resistname = Enum.GetName(typeof(ResType), AttackType);

   
        Resistance res = Resistance.None;

        FieldInfo fld = typeof(Resistances).GetField(resistname);
        res = (Resistance)fld.GetValue(data.resist);
        
        if (res == Resistance.Null)
        {
            Debug.Log("공격이 안통함");
            return;
        }
        else if (res == Resistance.Strength) 
        {
            defenseFactor = (int)(data.Magic * 1.6);
        }
        else if (res == Resistance.Weak)
        {
            defenseFactor = (int)(data.Magic * 0.5);
        }

        int DMG = (int)( power  / defenseFactor * rand);

        if (!isDie)
        {
            data.Hp -= DMG;

            if (data.Hp < 0)
            {
                data.Hp = 0;
                isDie = true;
            }
        }

    }
    


    public virtual void Die() 
    {
        if (isDie) 
        {
            //
        }
    }


}
