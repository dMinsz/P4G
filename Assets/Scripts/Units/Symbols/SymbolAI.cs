using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public partial class SymbolAI : MonoBehaviour
{
    public enum State { None, Idle, Patroll, Tracking, Hit, Attack, Size }

    public bool IsDebug = false;
    //[Header("StateMachine")]
    public State state;
    StateMachine<State, SymbolAI> stateMachine;
    public NavMeshAgent agent;
    public Transform[] patrollPoints;
    public float idleMaxTime;
    public float attackRange;
    //View
    [Header("View")]
    [SerializeField] float viewRange;
    [SerializeField, Range(0, 360)] float angle;
    [SerializeField] LayerMask targetMask;
    [SerializeField] LayerMask obstacleMask;


    [Header("Events")]
    public UnityEvent OnIdled;


    Animator animator;
    [HideInInspector]public Transform target;

    private float cosResult;
    public void Awake()
    {
        //cashing
        cosResult = Mathf.Cos(angle * 0.5f * Mathf.Deg2Rad);

        animator = transform.Find("Model").GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        //target = new Transform();

        stateMachine = new StateMachine<State, SymbolAI>(this);
        stateMachine.AddState(State.Idle, new IdleState(this, stateMachine));
        stateMachine.AddState(State.Patroll, new PatrollState(this, stateMachine));
        stateMachine.AddState(State.Tracking, new TrackingState(this, stateMachine));
        stateMachine.AddState(State.Attack, new AttackState(this, stateMachine));
    }

    private void Start()
    {
        stateMachine.SetUp(State.Idle);
    }

    private void Update()
    {
        stateMachine.Update();
    }
    public Transform FindPlayer()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, viewRange, targetMask);
        foreach (Collider collider in colliders)
        {
            // 2. �տ� �ִ���
            Vector3 dirTarget = (collider.transform.position - transform.position).normalized;
            if (Vector3.Dot(transform.forward, dirTarget) < cosResult)
            {
                continue;
            }
            // 3. �߰��� ��ֹ��� ������
            float distToTarget = Vector3.Distance(transform.position, collider.transform.position);
            if (Physics.Raycast(transform.position, dirTarget, distToTarget, obstacleMask))
            {
                continue;
            }
            target = collider.gameObject.transform;
            return target;
        }
        target = null;
        return target;
    }

    public bool CanAttack() 
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, attackRange, targetMask);
        foreach (Collider collider in colliders)
        {
            // 2. �տ� �ִ���
            Vector3 dirTarget = (collider.transform.position - transform.position).normalized;
            if (Vector3.Dot(transform.forward, dirTarget) < cosResult)
            {
                continue;
            }
            target = collider.gameObject.transform;
            return true;
        }
        target = null;
        return false;
    }

    private Vector3 AngleToDir(float angle)
    {
        float radian = angle * Mathf.Deg2Rad;
        return new Vector3(Mathf.Sin(radian), 0, Mathf.Cos(radian));
    }

    private void OnDrawGizmos()
    {
        if (!IsDebug) 
        {
            return;
        }

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, viewRange);

        Vector3 rightDir = AngleToDir(transform.eulerAngles.y + angle * 0.5f);
        Vector3 leftDir = AngleToDir(transform.eulerAngles.y - angle * 0.5f);
        Debug.DrawRay(transform.position, rightDir * viewRange, Color.red);
        Debug.DrawRay(transform.position, leftDir * viewRange, Color.red);
    }

}
