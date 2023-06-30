using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using static PersonaData;
using static Skill;

public class PersonaSkillCommand : Command
{
    BattlePersona persona;
    Transform summonPoint;
    Player player;
    BattleCamSystem cam;
    Shadow target;
    ResType resistype;
    TargetType targetType;
    int skillPower;
    int skillCri;
    public PersonaSkillCommand(BattlePersona persona , Transform summonPoint, Shadow target,Player player , BattleCamSystem cam,
        ResType resistype, TargetType targetType, int skillPower, int skillCri)
    {
        this.persona = persona;
        this.summonPoint = summonPoint;
        this.target = target;
        this.player = player;
        this.cam = cam;

        this.resistype = resistype;
        this.targetType = targetType;
        this.skillPower = skillPower;
        this.skillCri = skillCri;


    }
    protected override async Task AsyncExecuter()
    {

        var pobj = GameManager.Pool.Get(false, persona, summonPoint.position, Quaternion.identity);
        pobj.transform.LookAt(target.transform);

        await Task.Delay(1000);

        SetBackCam(this.cam);


        if (resistype == ResType.Physic)
        {
            pobj.Attack();

            //await Task.Delay((int)pobj.animator.GetCurrentAnimatorStateInfo(0).length * 100);
            //await Task.Delay(500);
            var pos = this.target.transform.position;
            pos.y += 2;

            pobj.attackEffect.transform.position = pos;
            
            pobj.attackEffect.Play();

            await Task.Delay(1000);
            //testing
            target.animator.SetTrigger("Hit");

        }
        else 
        {
            if (targetType == TargetType.AllEnemy)
            { // 전체 공격
                foreach (var shadow in GameManager.Data.Battle.InBattleShadows)
                {
                    pobj.UseSkill();

                    //await Task.Delay((int)pobj.animator.GetCurrentAnimatorStateInfo(0).length * 100);
                    //await Task.Delay(500);
                    var pos = this.target.transform.position;
                    pos.y += 2;

                    pobj.skillEffect.transform.position = pos;
                    pobj.skillEffect.Play();

                    //testing
                    await Task.Delay(500);

                    shadow.animator.SetTrigger("Hit");

                    shadow.TakeSkillDamage(resistype, skillPower, skillCri, 1);
                }
            }
            else 
            {
                pobj.UseSkill();

                //await Task.Delay((int)pobj.animator.GetCurrentAnimatorStateInfo(0).length * 100);
                //await Task.Delay(500);
                var pos = this.target.transform.position;
                pos.y += 2;

                pobj.skillEffect.transform.position = pos;
                pobj.skillEffect.Play();

                //testing
                await Task.Delay(1000);



                target.animator.SetTrigger("Hit");

                target.TakeSkillDamage(resistype, skillPower, skillCri, 1);
            }
        }

        await Task.Delay((int)pobj.animator.GetCurrentAnimatorStateInfo(0).length * 1000);
        pobj.skillEffect.Stop();
        await Task.Delay(1000);

        GameManager.Pool.Release(pobj);
    }

    public void SetBackCam(BattleCamSystem cam)
    {
        if (GameManager.Data.Battle.InBattlePlayers.IndexOf(player) == 0)
        {
            cam.setPlayer1(false);
        }
        else if (GameManager.Data.Battle.InBattlePlayers.IndexOf(player) == 1)
        {
            cam.setPlayer2(false);
        }
        else if (GameManager.Data.Battle.InBattlePlayers.IndexOf(player) == 2)
        {
            cam.setPlayer3(false);
        }

    }
}