using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Symbols : MonoBehaviour, IHitable
{

    public List<UnitData> hasEnemys;

    //[SerializeField] public Transform enemySpawnPoint;
    //[SerializeField] public List<Transform> patrolPoints;

    //[SerializeField] float interactRange;//

    public void TakeHit()
    {
        // ��Ʋ������ �̵��Ѵ�.

        Debug.Log("Symbol Take Hit , Go to Battle");
    }
}
