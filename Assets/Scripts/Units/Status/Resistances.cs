using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Flags]
public enum Resistance 
{
    None = 0,
    Physic = 1,//¹°¸®
    Fire = 2,// È­
    Ice = 4,// ºù
    Electronic = 8,//Àü°Ý
    Wind = 16,//Ç³
    Light = 32, // ±¤
    Dark = 64, // ¾Ï
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
