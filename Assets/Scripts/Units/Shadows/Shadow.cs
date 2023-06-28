using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Shadow : Unit, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{

    public int Maxhp;
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
    BattleCamSystem cam;
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

    public void Attack(Vector3 attackPoint, Vector3 lookPoint, Transform ui , BattleCamSystem cam)
    {
        this.cam = cam;

        Vector3 OriginPos = transform.position;
        ////Move
        GameManager.Data.Battle.commandQueue.Enqueue(new LookCommand(lookPoint, this.transform));
        GameManager.Data.Battle.commandQueue.Enqueue(new MoveCommand(attackPoint, transform, animator));

        ////Attack
        GameManager.Data.Battle.commandQueue.Enqueue(new AttackCommand(this, GameManager.Data.Battle.nowPlayer, animator));

        ////ReturnBack
        GameManager.Data.Battle.commandQueue.Enqueue(new LookCommand(OriginPos, this.transform));
        GameManager.Data.Battle.commandQueue.Enqueue(new MoveCommand(OriginPos, transform, animator));
        GameManager.Data.Battle.commandQueue.Enqueue(new LookCommand(lookPoint, this.transform));

        //camSet
        GameManager.Data.Battle.commandQueue.Enqueue(new FuncCommand(SetBackCam));


        GameManager.Data.Battle.commandQueue.Enqueue(new UICommand(ui, true));
    }

    public override void TakeSkillDamage(ResType AttackType, int power, int critical, int hit)
    {
        
    }


    public void SetBackCam()
    {
        var player = GameManager.Data.Battle.nowPlayer;
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

    //Targeting
    public bool isTargeted = false;

    public void SetTarget(bool istarget) 
    {
        isTargeted = istarget;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        targetUI.gameObject.SetActive(true);

        if (!isTargeted)
        {
            targetSprite.color = new Color(1f, 1f, 1f, 0.7f);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isTargeted)
        {
            targetUI.gameObject.SetActive(false);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
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
