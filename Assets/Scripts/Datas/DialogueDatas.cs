using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DungeonDatas;

[CreateAssetMenu(fileName = "DialogueDatas", menuName = "Data/Dialogue")]
public class DialogueDatas : ScriptableObject
{
    [SerializeField] public List<Dialog_cycle> dialogue;

    [SerializeField] public List<Dialog_cycle> Dialog { get { return dialogue; } }

}
