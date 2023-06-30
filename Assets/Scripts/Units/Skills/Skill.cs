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
    [SerializeField] public int cost; // HP 는 % 계산 , MP는 숫자계산

    [SerializeField] public ResType SkillType;
    [SerializeField] public Sprite skillImage;

    [SerializeField] public TargetType target;

    [SerializeField] public int power = 1;
    [SerializeField] public int critical = 1;
    [SerializeField] public int hits = 1;
}
