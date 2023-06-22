using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    public UnitData data;

    bool isDie = false;

    public abstract void Attack();

    public virtual void TakeDamage(int AttackerEndurance , int AttackerLevel , int hits = 1) 
    {
        float rand = Random.Range(0.95f, 1.06f);
        int DMG = (int)Mathf.Ceil(5 * Mathf.Sqrt(data.Strength / AttackerEndurance) * (AttackerLevel - data.Level) * hits * rand);

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

    public abstract void TakeSkillDamage(ResType AttackType,int power,int critical,int hit);
    


    public virtual void Die() 
    {
        if (isDie) 
        {
            //
        }
    }


}
