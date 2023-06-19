using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Flags]
public enum Resistance 
{
    None = 0,
    Physic = 1,//����
    Fire = 2,// ȭ
    Ice = 4,// ��
    Electronic = 8,//����
    Wind = 16,//ǳ
    Light = 32, // ��
    Dark = 64, // ��
}

public abstract class Resistances 
{
    Resistance REG;

    public Resistance GetResistance() 
    {
        return REG;
    }

    public void SetResistance(Resistance reg) 
    {
        REG = reg; 
    }

    public void SetResistance(byte byteReg) 
    {
        REG = (Resistance)byteReg;
    }

    public void AddResistance(Resistance reg) 
    {
        REG = REG | reg;
    }

    public void RemoveResistance(Resistance reg) 
    {
        REG &= ~reg;
    }

    public bool HasResistance(Resistance reg) 
    {
       return REG.HasFlag(reg);
    }
}
