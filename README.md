# P4G

Persona 4 Golden FanGame for Study


# Game Play

Main Game Scene

![Start](./MarkDownFiles/Main.png)

![Loading](./MarkDownFiles/Loading.png)


## CutScene and Dialogue

![Dialogue0](./MarkDownFiles/Dialogue0.png)

![Dialogue1](./MarkDownFiles/Dialogue1.png)
![CutScene](./MarkDownFiles/CutScene.png)

![Dialogue2](./MarkDownFiles/Dialogue2.png)


## In Dungeon and Battle

![InDungeon](./MarkDownFiles/InDungeon.png)

![InBattle](./MarkDownFiles/InBattle.png)

In Battle UI -> Attack , Skill etc..

![Analysis](./MarkDownFiles/Analysis.png)
Analysis allows you to determine the characteristics of enemies.

![Summon](./MarkDownFiles/Summon.png)

If you choose a skill, you will summon a persona and the persona will use the skill.

![SkillAttack](./MarkDownFiles/SkillAttack.png)

## Member Finding

![Member0](./MarkDownFiles/Member0.png)

![Meber1](./MarkDownFiles/Meber1.png)

Members help in battle together.

![MemberSkill](./MarkDownFiles/MemberSkill.png)

# Implement a turn-based game

used Command Pattern

see link below

https://github.com/dMinsz/P4G/blob/master/Assets/Scripts/Systems/Battle/BattleSystem.cs
https://github.com/dMinsz/P4G/tree/master/Assets/Scripts/Systems/Battle/Command

```cs
private void PlayerAttack()
    {
        GameManager.Data.Battle.commandQueue.Enqueue(new UICommand(uiHandler.MenuUI.transform, false));
        GameManager.Data.Battle.commandQueue.Enqueue(new UICommand(uiHandler.SelectMenuUI.transform, false));
        GameManager.Data.Battle.commandQueue.Enqueue(new UICommand(uiHandler.partyUI.transform, false));

        nowPlayer.Attack(nowShadow.attackPoint.position, nowShadow.transform.position, uiHandler.BattleUI.transform, cam);

        GameManager.Data.Battle.commandQueue.Enqueue(new FuncCommand(NextPlayer));
    }
```

BattleSystem Used Command Queue

https://github.com/dMinsz/P4G/blob/master/Assets/Scripts/Systems/Battle/BattleSystem.cs

```cs
    public Command activeCommand;
    public Queue<Command> commandQueue;
    //..
    //skip
    //..
private void Update()
    {
        if (commandQueue != null && commandQueue.Count > 0)
        {
            if (activeCommand == null || !activeCommand.isExecuting)
            {
                activeCommand = commandQueue.Dequeue();
                activeCommand.Execute();
            }
        }
    }
```




