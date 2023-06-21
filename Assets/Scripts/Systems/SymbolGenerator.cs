using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SymbolGenerator : MonoBehaviour
{
    public Transform[] GenratePos;
    public GameObject Symbol;
    private void Awake()
    {
       
    }

    private void Start()
    {
        for (int i = 0; i < GenratePos.Length; i++)
        {
            GameManager.Pool.Get(Symbol, GenratePos[i].position , Quaternion.identity);
        }
    }
}
