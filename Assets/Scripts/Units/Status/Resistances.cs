using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[Serializable]
public enum Resistance 
{
    None = 0,
    Weak = 1, // ���ϴ�
    Strength =2, // ����
    Absorb = 3,// ��� Ȥ�� �ݻ�
    Null = 4, //������
}


[Serializable]
public enum ResType
{
    None = 0,
    Physic = 1,//����
    Fire = 2,// ȭ
    Ice = 3,// ��
    Electronic = 4,//����
    Wind = 5,//ǳ
    Light = 6, // ��
    Dark = 7, // ��
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
