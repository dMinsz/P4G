using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    //게임 캐릭터 및 적(섀도우) 클래스
    //페르소나 는 다른 Class 를 사용한다.

    //status
    public int Level;
    public int Hp;
    public int Mp;
    protected int Strength; // Indicates the effectiveness of a Persona's Physical Skills.
    protected int Magic; // Indicates the effectiveness of a Persona's Magic Skills and Magic Defense.
    protected int Endurance; // Indicates the effectiveness of a Persona's Physical Defense.
    protected int Agility; // Affects a Persona's Hit and Evasion rates.
    protected int Luck; // Affects the possiblity of a Persona performing Critical Hits and other things

    /*
     * Damage Calcuration
     * DMG = 5 x sqrt(ST/EN x ATK) x MOD x HITS X RND
        DMG = Damage
        ST = Character's Strength stat
        EN = Enemy's Endurance stat
        ATK = Atk value of equipped weapon OR Pwr value of used skill
        MOD = Modifier based on the difference between character level and enemy level
        HITS= Number of hits (for physical skills)
        RND = Randomness factor (between 0.95 and 1.05)
     */

    protected Resistances Res;

    bool isDie = false;

    public abstract void Attack();

    public virtual void TakeDamage(int AttackerEndurance , int AttackerLevel , int hits = 1) 
    {
        float rand = Random.Range(0.95f, 1.06f);
        int DMG = (int)Mathf.Ceil(5 * Mathf.Sqrt(Strength / AttackerEndurance) * (AttackerLevel - Level) * hits * rand);

        if (!isDie) 
        {
            Hp -= DMG;

            if (Hp < 0)
            {
                Hp = 0;
                isDie = true;
            }
        }
    }

    public virtual void Die() 
    {
        if (isDie) 
        {
            //
        }
    }


}
