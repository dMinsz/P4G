using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class PlayerAttacker : MonoBehaviour
{
    [SerializeField] bool debug;

    [SerializeField] float range;
    [SerializeField, Range(0, 360)] float angle;

    private Animator animator;
    private float cosResult;
   
 
    private void Awake()
    {
        animator = transform.Find("Model").GetComponent<Animator>();
        //cashing
        cosResult = Mathf.Cos(angle * 0.5f * Mathf.Deg2Rad);
    }

    public void Attack()
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            animator.SetTrigger("Attack");
            AttackTiming();
        }
        
    }

    private void OnAttack(InputValue value)
    {
        Attack();
    }

    public void AttackTiming()
    {
        // 1. 범위 안에 있는지
        Collider[] colliders = Physics.OverlapSphere(transform.position, range);
        foreach (Collider collider in colliders)
        {
            // 2. 앞에 있는지, 대상 방향까지의 cos를 계산하여 내적이 +이면 어택
            Vector3 dirTarget = (collider.transform.position - transform.position).normalized;
            if (Vector3.Dot(transform.forward, dirTarget) < cosResult) 
                continue;                                                              

            IHitable hittable = collider.GetComponent<IHitable>();
            hittable?.TakeHit(this.GetComponent<Player>());

        }
    }

    private void OnDrawGizmos()
    {
        if (!debug)
            return;

        //Show Attack Range
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);

        Vector3 rightDir = AngleToDir(transform.eulerAngles.y + angle * 0.5f);
        Vector3 leftDir = AngleToDir(transform.eulerAngles.y - angle * 0.5f);
        Debug.DrawRay(transform.position, rightDir * range, Color.red);
        Debug.DrawRay(transform.position, leftDir * range, Color.red);
    }

    private Vector3 AngleToDir(float angle)
    {
        float radian = angle * Mathf.Deg2Rad;
        return new Vector3(Mathf.Sin(radian), 0, Mathf.Cos(radian));
    }
}
