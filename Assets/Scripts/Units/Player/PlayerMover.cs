using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] bool debug;

    [SerializeField] public List<Transform> ChacePoitns = new List<Transform>();
    [SerializeField] float moveSpeed;
    private float curSpeed;

    private CharacterController controller;
    private Animator animator;
    private Vector3 moveDir;
    private float ySpeed = 0;
    Coroutine moveRoutine;
    Coroutine gravityRoutine;


    public AudioClip moveSound;
    public AudioSource soundSource;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        animator = transform.Find("Model").GetComponent<Animator>();
    }

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


    private void Update()
    {
        //Move();
    }

    private IEnumerator MoveRoutine()
    {

        while (true)
        {

            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            {
                soundSource.Stop();
                yield return null;
                continue;

            }

            if (moveDir.sqrMagnitude <= 0)
            {
                soundSource.Stop();
                curSpeed = Mathf.Lerp(curSpeed, 0, 0.1f);
                animator.SetFloat("MoveSpeed", curSpeed);
                yield return null;
                continue;
            }

            if (soundSource.isPlaying == false)
            {
                soundSource.Play();
            }

            Vector3 forwardVec = new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z).normalized;
            Vector3 rightVec = new Vector3(Camera.main.transform.right.x, 0, Camera.main.transform.right.z).normalized;

            curSpeed = Mathf.Lerp(curSpeed, moveSpeed, 0.25f);

            controller.Move(forwardVec * moveDir.z * curSpeed * Time.deltaTime);
            controller.Move(rightVec * moveDir.x * curSpeed * Time.deltaTime);
            animator.SetFloat("MoveSpeed", curSpeed);

            Quaternion lookRotation = Quaternion.LookRotation(forwardVec * moveDir.z + rightVec * moveDir.x);

            if (moveDir.z < 0)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, 0.001f);

            }
            else 
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, 0.01f);
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

    private void OnMove(InputValue value)
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            moveDir.x = value.Get<Vector2>().x;
            moveDir.z = value.Get<Vector2>().y;
        }
    }
}
