using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Skilldata", menuName = "Data/Skill")]
public class SkillData : ScriptableObject
{
    [SerializeField] public Skill skill;
    public Skill Skill { get { return skill; } }
}
