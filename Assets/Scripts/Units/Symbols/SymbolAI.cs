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
    public float traceRange;
    //View
    [SerializeField] float range;
    [SerializeField, Range(0, 360)] float angle;
    [SerializeField] LayerMask targetMask;
    [SerializeField] LayerMask obstacleMask;
    [NonSerialized] public bool foundPlayer;

    [Header("Events")]
    public UnityEvent OnIdled;


    Animator animator;
    Transform target;

    private float cosResult;
    public void Awake()
    {
        //cashing
        cosResult = Mathf.Cos(angle * 0.5f * Mathf.Deg2Rad);

        animator = transform.Find("Model").GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        stateMachine = new StateMachine<State, SymbolAI>(this);
        stateMachine.AddState(State.Idle, new IdleState(this, stateMachine));
        stateMachine.AddState(State.Patroll, new PatrollState(this, stateMachine));

    }

    private void Start()
    {
        stateMachine.SetUp(State.Idle);
    }

    private void Update()
    {
        stateMachine.Update();
    }
    public void FindPlayer()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, range, targetMask);
        foreach (Collider collider in colliders)
        {
            // 2. 앞에 있는지
            Vector3 dirTarget = (collider.transform.position - transform.position).normalized;
            if (Vector3.Dot(transform.forward, dirTarget) < cosResult)
            {
                continue;
            }
            // 3. 중간에 장애물이 없는지
            float distToTarget = Vector3.Distance(transform.position, collider.transform.position);
            if (Physics.Raycast(transform.position, dirTarget, distToTarget, obstacleMask))
            {
                continue;
            }
            target = collider.gameObject.transform;
            return;
        }
        target = null;
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
        Gizmos.DrawWireSphere(transform.position, range);

        Vector3 rightDir = AngleToDir(transform.eulerAngles.y + angle * 0.5f);
        Vector3 leftDir = AngleToDir(transform.eulerAngles.y - angle * 0.5f);
        Debug.DrawRay(transform.position, rightDir * range, Color.red);
        Debug.DrawRay(transform.position, leftDir * range, Color.red);
    }

}
