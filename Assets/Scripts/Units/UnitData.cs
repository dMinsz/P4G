using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UnitData 
{
    //���� ĳ���� �� ��(������) �������ͽ� Ŭ����

    [SerializeField] public string unitName;
    [SerializeField] public int Level;
    [SerializeField] public int Hp;
    [SerializeField] public int Mp;
    [SerializeField] public int Strength; // Indicates the effectiveness of a Persona's Physical Skills.
    [SerializeField] public int Magic; // Indicates the effectiveness of a Persona's Magic Skills and Magic Defense.
    [SerializeField] public int Endurance; // Indicates the effectiveness of a Persona's Physical Defense.
    [SerializeField] public int Agility; // Affects a Persona's Hit and Evasion rates.
    [SerializeField] public int Luck; // Affects the possiblity of a Persona performing Critical Hits and other things


    [SerializeField] public GameObject Prefab;


    [SerializeField] public Resistances resist;
    [SerializeField] public List<SkillData> skills = new List<SkillData>();

    //[SerializeField] public List<SkillDatas> skills = new List<SkillDatas>();
}