using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "DungeonDatas", menuName = "Data/DungeonDatas")]
public class DungeonDatas : ScriptableObject
{
    [SerializeField] public DungeonInfo[] dungeons;
    public DungeonInfo[] Dungeons { get { return dungeons; } }

    [Serializable]
    public class DungeonInfo
    {
        [Header("Status")]
        public string DungeonName;

        [Header("Can be Make Shadow")]
        public List<ShadowData> shadows;

        public int symbolHaveShadow;

    }
}
