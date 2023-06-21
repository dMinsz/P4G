using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ShadowDatas", menuName = "Data/Shadows")]
public class ShadowsDatas : ScriptableObject
{
    [SerializeField] public UnitData[] shadows;
    public UnitData[] Shadows { get { return shadows; } }
}