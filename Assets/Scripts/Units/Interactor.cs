using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interactor : MonoBehaviour
{
    [SerializeField] bool debug;

    [SerializeField] Transform point;
    [SerializeField] float range;

    public Animator animator;
    public void Interact()
    {
        Collider[] colliders = Physics.OverlapSphere(point.position, range);
        foreach (Collider collider in colliders)
        {
            InteractAdaptor adaptor = collider.GetComponent<InteractAdaptor>();
            if (adaptor != null)
            {
                animator.Play("WinPose");
                adaptor.Interact(this);
                break;
            }
        }
    }

    private void OnInteract(InputValue value)
    {
        Interact();
    }

    private void OnDrawGizmos()
    {
        if (!debug)
            return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(point.position, range);
    }
}
