using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "PersonaData", menuName = "Data/Persona")]
public class PersonaData : ScriptableObject
{
    [SerializeField] public Persona persona;

    public Persona PData { get { return persona; } }

    [Serializable]
    public class Persona
    {
        [SerializeField] public String personaName;

        [Header("Status")]
        [SerializeField] public int Level;
        [SerializeField] public int Strength;
        [SerializeField] public int Magic; 
        [SerializeField] public int Endurance; 
        [SerializeField] public int Agility; 
        [SerializeField] public int Luck;

        [SerializeField] public Resistances resist;

        [Header("Prefabs")]

        [SerializeField] public List<SkillData> skills = new List<SkillData>();

        public Sprite bustup;
        public GameObject prefabs;

        public Persona(Persona u)
        {
            this.personaName = u.personaName;
        
            this.Strength = u.Strength;
            this.Magic = u.Magic;
            this.Endurance = u.Endurance;
            this.Agility = u.Agility;
            this.Luck = u.Luck;
            this.resist = u.resist;
            this.skills = u.skills;

            this.bustup = u.bustup; 
            this.prefabs = u.prefabs;
        }
    }

   

}
