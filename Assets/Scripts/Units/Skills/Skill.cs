using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    public enum costType 
    {
        None,
        HP,
        MP,
        SP
    }

    public enum TargetType    
    {
        Enemy,
        AllEnemy,
        Ally,
        AllAlly,
        Self,
    }


    public string skillName;
    
    public costType cType = costType.None;
    public int cost;

    Resistances.Type SkillType;
    Sprite skillImage;

    TargetType target;

    public abstract void UseSkill();

}
