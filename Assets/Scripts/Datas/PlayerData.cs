using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "PlayerData", menuName = "Data/Player")]
public class PlayerData : ScriptableObject
{
    [SerializeField] public UnitData player;
    [SerializeField] public List<string> PersonaNames;
    public UnitData Player { get { return player; } }
}
