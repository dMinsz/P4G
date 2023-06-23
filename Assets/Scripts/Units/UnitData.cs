using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UnitData 
{
    //게임 캐릭터 및 적(섀도우) 스테이터스 클래스

    [SerializeField] public string unitName;
    [SerializeField] public int Level;
    [SerializeField] public int Hp;
    [SerializeField] public int Mp;
    [SerializeField] public int Sp;
    [SerializeField] public int Strength; // Indicates the effectiveness of a Persona's Physical Skills.
    [SerializeField] public int Magic; // Indicates the effectiveness of a Persona's Magic Skills and Magic Defense.
    [SerializeField] public int Endurance; // Indicates the effectiveness of a Persona's Physical Defense.
    [SerializeField] public int Agility; // Affects a Persona's Hit and Evasion rates.
    [SerializeField] public int Luck; // Affects the possiblity of a Persona performing Critical Hits and other things


    [SerializeField] public GameObject Prefab;
    [SerializeField] public GameObject BattlePrefab;
    [SerializeField] public Sprite battleImage;

    [SerializeField] public Resistances resist;
    [SerializeField] public List<SkillData> skills = new List<SkillData>();

    //[SerializeField] public List<SkillDatas> skills = new List<SkillDatas>();


    public UnitData(UnitData u) 
    {
        this.unitName = u.unitName;
        this.Level = u.Level;  
        this.Hp = u.Hp;
        this.Strength = u.Strength;
        this.Magic = u.Magic;
        this.Endurance = u.Endurance;
        this.Agility = u.Agility;
        this.Luck = u.Luck;
        this.Prefab = u.Prefab;
        this.resist = u.resist;
        this.skills = u.skills;

        this.Sp = u.Sp;
        this.BattlePrefab = u.BattlePrefab;
        this.battleImage = u.battleImage;

    }
}
