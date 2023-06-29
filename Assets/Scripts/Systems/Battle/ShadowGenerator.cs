using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class ShadowGenerator : MonoBehaviour
{

    public bool IsDebug = false;
    public float generateRange;

    public List<ShadowData> InBattleShadows;
    public void SetUp() 
    {
        InBattleShadows = GameManager.Data.Dungeon.tempSymbolShadows;
    }


    private void Awake()
    {


        if (InBattleShadows.Count <= 0) 
        {
            UnityEngine.Debug.Log("Shadow Data Is None");
        }

        if (InBattleShadows.Count == 1)
        {

        }

    }

    private Vector3 RandomSphereInPoint(float radius)
    {
        Vector3 getPoint = UnityEngine.Random.onUnitSphere;
        getPoint.y = 0.0f;

        float r = UnityEngine.Random.Range(0, radius);

        return (getPoint * r) + transform.position;

    }


    private void OnDrawGizmos()
    {
        if (!IsDebug)
        {
            return;
        }

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, generateRange);
    }
}
