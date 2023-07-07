using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyMover : MonoBehaviour
{
    private CharacterController controller;
    private Animator animator;
    public Transform ChacePos;

    Coroutine moveRoutine;
    Coroutine gravityRoutine;
    private float ySpeed = 0;

    
    private void OnEnable()
    {
        moveRoutine = StartCoroutine(MoveRoutine());
        gravityRoutine = StartCoroutine(GravityRoutine());
    }

    private void OnDisable()
    {
        StopCoroutine(moveRoutine);
        StopCoroutine(gravityRoutine);
    }

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        animator = transform.Find("Model").GetComponent<Animator>();
    }
    private IEnumerator MoveRoutine()
    {
        while (true)
        {
            if (ChacePos != null)
            {
                var distance = ChacePos.transform.position - this.transform.position;

                if (distance.sqrMagnitude > 100)
                {
                    this.transform.GetComponent<CharacterController>().enabled = false;

                    this.transform.position = ChacePos.transform.position;//너무 멀어진 거리는 순간이동해준다.

                    this.transform.GetComponent<CharacterController>().enabled = true;
                }
                else if (distance.sqrMagnitude > 2)
                {
                    animator.SetBool("Move", true);
                    transform.LookAt(ChacePos.transform.position);
                    controller.SimpleMove(transform.forward * 5.0f);
                }
                else
                {
                    animator.SetBool("Move", false);
                    transform.LookAt(ChacePos.transform.position);
                }

            }
        
            yield return null;
        }
    }

    private IEnumerator GravityRoutine()
    {
        while (true)
        {
            ySpeed += Physics.gravity.y * Time.deltaTime;

            if (GroundCheck() && ySpeed < 0)
                ySpeed = -1;

            controller.Move(Vector3.up * ySpeed * Time.deltaTime);
            yield return null;
        }
    }

    private bool GroundCheck()
    {
        RaycastHit hit;
        return Physics.SphereCast(transform.position + Vector3.up * 1f, 0.5f, Vector3.down, out hit, 0.6f);
    }

}
