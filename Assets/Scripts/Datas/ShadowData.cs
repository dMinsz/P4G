using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ShadowData", menuName = "Data/Shadow")]
public class ShadowData : ScriptableObject
{
    [SerializeField] public UnitData shadow;
    public UnitData Shadow { get { return shadow; } }
}
