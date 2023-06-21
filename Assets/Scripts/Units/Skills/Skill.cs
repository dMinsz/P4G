using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class Skill
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

    [SerializeField] public string skillName;

    [SerializeField] public costType cType = costType.None;
    [SerializeField] public int cost; // HP �� % ��� , MP�� ���ڰ��

    [SerializeField] ResType SkillType;
    [SerializeField] Sprite skillImage;

    [SerializeField] TargetType target;

    [SerializeField] int power = 1;
    [SerializeField] int critical = 1;
    [SerializeField] int hits = 1;
}
