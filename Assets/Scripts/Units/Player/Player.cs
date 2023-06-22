using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Unit , IHitable
{

    public Animator animator;
    public List<Player> Partys = new List<Player>();

    private void Awake()
    {
        animator = transform.Find("Model").GetComponent<Animator>();
        //Partys.Add(this);
    }

    private void OnEnable()
    {
        animator = transform.Find("Model").GetComponent<Animator>();
    }

    private void OnDisable()
    {
        animator.SetFloat("MoveSpeed", 0f);
    }
    public void AddParty(string name) 
    {
        var data = GameManager.Resource.Load<Player>("Datas/"+name);
        Partys.Add(data);
    }
    public void RemoveParty(string name)
    {
        foreach (var player in Partys) 
        {
            if (player.data.unitName == name)
            {
                Partys.Remove(player);
                return;
            }
        }
    }
    public override void Attack()
    {
    }

    public override void TakeSkillDamage(ResType AttackType, int power, int critical, int hit)
    {
    }

    public void TakeHit(Object attacker)
    {
        // 배틀씬으로 이동한다.

        Debug.Log("Symbol Take Hit , Go to Battle");




        GameManager.Data.Dungeon.tempSymbolShadows = ((Symbols)attacker).hasEnemys;
        
        GameManager.Data.Dungeon.InBattlePlayers.Clear();
        GameManager.Data.Dungeon.InBattlePlayers.Add(this);

        if (Partys.Count > 1)
        {
            foreach (var ally in Partys)
            {
                GameManager.Data.Dungeon.InBattlePlayers.Add(ally);
            }
        }

        GameManager.Data.Dungeon.StartTurn = DungeonDataSystem.Turn.Enemy;

        GameManager.Pool.Release(this);
        GameManager.Scene.LoadScene("BattleScene");
    }
}
