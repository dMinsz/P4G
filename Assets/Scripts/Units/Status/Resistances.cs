using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public enum Resistance 
{
    None = 0,
    Weak = 1, // ���ϴ�
    Strength =2, // ����
    Absorb = 3,// ��� Ȥ�� �ݻ�
    Null = 4, //������
}


public class Resistances 
{
    public Resistance[] _Res;


    public enum Type
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

    public Resistances() 
    {
        _Res = new Resistance[8];
    }
}
