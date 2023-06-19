using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractAdaptor : MonoBehaviour
{
    public UnityEvent<Interactor> OnInvoked;

    public void Interact(Interactor interactor)
    {
        OnInvoked?.Invoke(interactor);
    }
}
