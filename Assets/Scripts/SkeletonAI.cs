using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AI_State
{
    Idle, Patrol, Trace, Attack, Hide
}

public class SkeletonAI : MonoBehaviour
{
    [SerializeField]
    AI_State state;

    Animator anim;
    Camera eye;
    Plane[] eyePlanes;
    GameObject enemy;

    // Move
    float moveValue = 0f;

    // State Patrol
    public GameObject targetObject;
    Vector3 targetPos;
    float patrolMoveSpeed = 1f;
    float patrolRotateSpeed = 1f;

    // State Trace
    float traceMoveSpeed = 5f;
    float traceRotateSpeed = 3f;


    // Start is called before the first frame update
    void Start()
    {
        enemy = GameObject.FindGameObjectWithTag("Player");

        // Component
        anim = GetComponent<Animator>();
        eye = GetComponentInChildren<Camera>();

        state = AI_State.Idle;

        ChangeState(state);
    }

    // Update is called once per frame
    void Update()
    {
        // 매 프레임 실행되는 코드
        switch(state)
        {
            case AI_State.Idle: UpdateIdle(); break;
            case AI_State.Patrol: UpdatePatrol(); break;
            case AI_State.Trace: UpdateTrace(); break;
            case AI_State.Attack: UpdateAttack(); break;
            case AI_State.Hide: UpdateHide(); break;
        }
        anim.SetFloat("MoveValue", moveValue);
    }

    void UpdateIdle()
    {
        if (FindEnemy())
        {
            ChangeState(AI_State.Trace);
            return;
        }
    }

    void UpdatePatrol()
    {
        if (FindEnemy())
        {
            ChangeState(AI_State.Trace);
            return;
        }

        Vector3 dir = targetPos - transform.position;
        float dist = dir.magnitude;

        if (dist < 0.3f)
        {
            ChangeState(AI_State.Idle);
            return;
        }

        moveValue = Mathf.Clamp(moveValue + Time.deltaTime, 0f, 0.5f);

        Quaternion targetRotation = Quaternion.LookRotation(dir, Vector3.up);
        transform.rotation = 
            Quaternion.Slerp(transform.rotation, targetRotation, patrolRotateSpeed * Time.deltaTime);

        transform.position += transform.forward * moveValue * patrolMoveSpeed * Time.deltaTime;
    }

    void UpdateTrace()
    {
        Vector3 dir = targetPos - transform.position;
        float dist = dir.magnitude;

        if (dist < 0.3f)
        {
            ChangeState(AI_State.Attack);
            return;
        }
        moveValue = Mathf.Clamp(moveValue + Time.deltaTime, 0f, 1f);

        Quaternion targetRotation = Quaternion.LookRotation(dir, Vector3.up);
        transform.rotation =
            Quaternion.Slerp(transform.rotation, targetRotation, traceRotateSpeed * Time.deltaTime);

        transform.position += transform.forward * moveValue * traceMoveSpeed * Time.deltaTime;

    }

    void UpdateAttack()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Attack"))
        {
            float normalizedTime = anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
            if (normalizedTime >= 0.8f)
            {
                ChangeState(AI_State.Idle);
                return;
            }
        }
    }

    void UpdateHide()
    {

    }

    void ChangeState(AI_State nextState)
    {
        state = nextState;

        anim.SetBool("Idle", false);
        anim.SetBool("Walk", false);
        anim.SetBool("Run", false);
        anim.SetBool("Attack", false);

        StopAllCoroutines();

        switch(state)
        {
            case AI_State.Idle: StartCoroutine(CoroutineIdle()); break;
            case AI_State.Patrol: StartCoroutine(CoroutinePatrol()); break;
            case AI_State.Trace: StartCoroutine(CoroutineTrace()); break;
            case AI_State.Attack: StartCoroutine(CoroutineAttack()); break;
            case AI_State.Hide: StartCoroutine(CoroutineHide()); break;
        }
    }

    IEnumerator CoroutineIdle()
    {
        // 최초에 한번 실행되는 코드
        anim.SetBool("Idle", true);

        moveValue = 0f;

        while (true)
        {
            //yield return new WaitUntil(() => 5 == 3);   // false 일 때 기다림
            //yield return new WaitWhile(() => 5 == 3);   // true 일 때 기다림

            yield return new WaitForSeconds(3f);

            // 일정 시간마다 실행되는 코드
            ChangeState(AI_State.Patrol);
            yield break;
        }
    }

    IEnumerator CoroutinePatrol()
    {
        // 최초에 한번 실행되는 코드
        anim.SetBool("Walk", true);

        // 이동 목표를 설정
        targetPos = transform.position +
            new Vector3(Random.Range(-5f, 5f), 0f, Random.Range(-5f, 5f));

        targetObject.transform.position = targetPos;

        yield break;

        //while (true)
        //{
        //    yield return new WaitForSeconds(3f);

        //    ChangeState(AI_State.Trace);
        //    yield break;
        //}
    }

    IEnumerator CoroutineTrace()
    {
        // 최초에 한번 실행되는 코드
        anim.SetBool("Run", true);

        // 타겟 위치를 적의 위치로 설정
        var enemyPos = enemy.transform.position;
        enemyPos.y = transform.position.y;
        targetPos = enemyPos;

        yield break;
        //while (true)
        //{
        //    yield return new WaitForSeconds(1f);

        //    ChangeState(AI_State.Attack);
        //    yield break;
        //}
    }

    IEnumerator CoroutineAttack()
    {
        // 최초에 한번 실행되는 코드
        anim.SetBool("Attack", true);

        moveValue = 0f;

        while (true)
        {
            yield break;
            // TODO : 애니메이션 종료 확인 후 상태 전환
            //yield return new WaitWhile(()=>);
            //ChangeState(AI_State.Idle);
        }
    }

    IEnumerator CoroutineHide()
    {
        // 최초에 한번 실행되는 코드

        while (true)
        {
            //yield return new WaitUntil(() => 5 == 3);   // false 일 때 기다림
            //yield return new WaitWhile(() => 5 == 3);   // true 일 때 기다림

            yield return new WaitForSeconds(3f);

            // 일정 시간마다 실행되는 코드
        }
        yield break;
    }

    private bool FindEnemy()
    {
        eyePlanes = GeometryUtility.CalculateFrustumPlanes(eye);
        Bounds bounds = enemy.GetComponentInChildren<Collider>().bounds;
        bool isFind = GeometryUtility.TestPlanesAABB(eyePlanes, bounds);

        return isFind;
    }
}
