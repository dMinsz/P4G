using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Shadow : Unit, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{

    public int MaxHp;
    public int curHp;

    public int MaxSp;
    public int curSp;

    public Transform attackPoint;

    public UnityEvent<int> OnHpChanged;
    public UnityEvent<int> OnSpChanged;

    public GameObject targetUI;
    SpriteRenderer targetSprite;
    public int HP { get { return curHp; } protected set { curHp = value; OnHpChanged?.Invoke(curHp); } }
    public int SP { get { return curSp; } protected set { curSp = value; OnSpChanged?.Invoke(curSp); } }

    public Animator animator;

    public ParticleSystem hitEffect;


    private void Awake()
    {
        animator = transform.Find("Model").GetComponent<Animator>();
        attackPoint = transform.Find("AttackPoint").transform;

        targetSprite = targetUI.GetComponent<SpriteRenderer>();

    }

    private void Start()
    {
        if (isTargeted)
        {
            targetUI.gameObject.SetActive(true);
        }
    }

    public void Attack()
    {
        GameManager.Data.Battle.commandQueue.Enqueue(new FillmCommand(GameManager.Data.Battle.cam, GameManager.Data.Battle.nowPlayer));
        Vector3 attackPoint = GameManager.Data.Battle.nowPlayer.PersonaPoint.position;
        Vector3 lookPoint = GameManager.Data.Battle.nowPlayer.transform.position;

        Vector3 OriginPos = transform.position;
        ////Move
        GameManager.Data.Battle.commandQueue.Enqueue(new LookCommand(lookPoint, this.transform));
        GameManager.Data.Battle.commandQueue.Enqueue(new MoveCommand(attackPoint, transform, animator));

        ////Attack
        GameManager.Data.Battle.commandQueue.Enqueue(new AttackCommand(this, GameManager.Data.Battle.nowPlayer, animator, GameManager.Data.Battle.nowPlayer.animator, GameManager.Data.Battle.nowSkill));

        ////ReturnBack
        GameManager.Data.Battle.commandQueue.Enqueue(new LookCommand(OriginPos, this.transform));
        GameManager.Data.Battle.commandQueue.Enqueue(new MoveCommand(OriginPos, transform, animator));
        GameManager.Data.Battle.commandQueue.Enqueue(new LookCommand(lookPoint, this.transform));

    }

    public override void TakeDamage(int strength, Skill nowskill)
    {
        float rand = UnityEngine.Random.Range(0.95f, 1.06f);

        int DMG = (int)((strength * strength) / (data.Endurance * 1.5) * rand);


        var index = GameManager.Data.Battle.InBattleShadows.FindIndex(v => v == GameManager.Data.Battle.nowShadow);
        var damageui = GameManager.Data.Battle.uiHandler.DamageUIs[index];
        damageui.SetTarget(this.transform);
        damageui.SetDamage(DMG);

        GameManager.UI.ShowInGameUI(damageui);


        if (!isDie)
        {
            HP -= DMG;

            if (HP < 0)
            {
                HP = 0;
                isDie = true;
                animator.SetBool("IsDie", true);
            }
        }
    }

    public override void TakeSkillDamage(Skill nowskill)
    {
        float rand = UnityEngine.Random.Range(0.95f, 1.06f);
        int defenseFactor = 1;

        string resistname = Enum.GetName(typeof(ResType), nowskill.SkillType);


        Resistance res = Resistance.None;

        FieldInfo fld = typeof(Resistances).GetField(resistname);
        res = (Resistance)fld.GetValue(data.resist);

        if (res == Resistance.Null)
        {
            Debug.Log("공격이 안통함");
            return;
        }
        else if (res == Resistance.Strength)
        {
            defenseFactor = (int)(data.Magic * 1.6);
        }
        else if (res == Resistance.Weak)
        {
            defenseFactor = (int)(data.Magic * 0.5);
        }

        int DMG = (int)(nowskill.power / defenseFactor * rand);



        var index = GameManager.Data.Battle.InBattleShadows.FindIndex(v => v == GameManager.Data.Battle.nowShadow);
        index %= GameManager.Data.Battle.InBattleShadows.Count;
        var damageui = GameManager.Data.Battle.uiHandler.DamageUIs[index];
        damageui.SetTarget(this.transform);
        damageui.SetDamage(DMG);

        GameManager.UI.ShowInGameUI(damageui);


        if (!isDie)
        {
            HP -= DMG;

            if (HP < 0)
            {
                HP = 0;
                isDie = true;
                animator.SetBool("IsDie", true);
            }
        }
    }



    //Targeting
    public bool isTargeted = false;

    public bool isShadowTurn = false;

    public void SetTarget(bool istarget)
    {
        isTargeted = istarget;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isShadowTurn && !isDie)
        {
            targetUI.gameObject.SetActive(true);

            if (!isTargeted)
            {
                targetSprite.color = new Color(1f, 1f, 1f, 0.7f);
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isShadowTurn && !isDie)
        {
            if (!isTargeted)
            {
                targetUI.gameObject.SetActive(false);
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isShadowTurn && !isDie)
        {

            if (!isTargeted)
            {
                if (GameManager.Data.Battle.nowShadow == null || GameManager.Data.Battle.nowShadow == this)
                {
                    isTargeted = true;
                    GameManager.Data.Battle.nowShadow = this;

                    targetUI.gameObject.SetActive(true);
                    targetSprite.color = new Color(1f, 1f, 1f, 1.0f);
                }
                else
                {
                    isTargeted = true;

                    GameManager.Data.Battle.nowShadow.SetTarget(false);
                    GameManager.Data.Battle.nowShadow.targetUI.gameObject.SetActive(false);

                    GameManager.Data.Battle.nowShadow = this;

                    targetUI.gameObject.SetActive(true);
                    targetSprite.color = new Color(1f, 1f, 1f, 1.0f);
                }
            }
        }
    }

}
