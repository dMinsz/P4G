using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[Serializable]
public enum Resistance 
{
    None = 0,
    Weak = 1, // 약하다
    Strength =2, // 강함
    Absorb = 3,// 흡수 혹은 반사
    Null = 4, //안통함
}


[Serializable]
public enum ResType
{
    None = 0,
    Physic = 1,//물리
    Fire = 2,// 화
    Ice = 3,// 빙
    Electronic = 4,//전격
    Wind = 5,//풍
    Light = 6, // 광
    Dark = 7, // 암
}


[Serializable]
public class Resistances 
{

    [SerializeField] public Resistance Physic = Resistance.None;
    [SerializeField] public Resistance Fire = Resistance.None;
    [SerializeField] public Resistance Ice = Resistance.None;
    [SerializeField] public Resistance Electronic = Resistance.None;
    [SerializeField] public Resistance Wind = Resistance.None;
    [SerializeField] public Resistance Light = Resistance.None;
    [SerializeField] public Resistance Dark = Resistance.None;
}
