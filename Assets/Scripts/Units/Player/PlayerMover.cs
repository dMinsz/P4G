using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] bool debug;

    [SerializeField] float moveSpeed;
    private float curSpeed;

    private CharacterController characterController;
    private Animator animator;
    private Vector3 moveDir;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        animator = transform.Find("Model").GetComponent<Animator>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (moveDir.magnitude == 0)
        {
            curSpeed = 0;
            animator.SetFloat("MoveSpeed", curSpeed);
            return;
        }

        Vector3 forwardVec = new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z).normalized;
        Vector3 rightVec = new Vector3(Camera.main.transform.right.x, 0, Camera.main.transform.right.z).normalized;

        curSpeed = Mathf.Lerp(curSpeed, moveSpeed, 0.25f);

        characterController.Move(forwardVec * moveDir.z * curSpeed * Time.deltaTime);
        characterController.Move(rightVec * moveDir.x * curSpeed * Time.deltaTime);
        animator.SetFloat("MoveSpeed", curSpeed);

        Quaternion lookRotation = Quaternion.LookRotation(forwardVec * moveDir.z + rightVec * moveDir.x);
 
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, 0.05f);
    }

    private void OnMove(InputValue value)
    {
        moveDir.x = value.Get<Vector2>().x;
        moveDir.z = value.Get<Vector2>().y;
    }
}
