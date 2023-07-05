using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseScene : MonoBehaviour
{
    #region Loading
    public bool isClear = false;
    public float progress { get; protected set; }
    protected abstract IEnumerator LoadingRoutine();

    public void LoadAsync()
    {
        //Clear();
        StartCoroutine(LoadingRoutine());
    }
    #endregion

    protected virtual void Awake()
    {
        Init();
    }

    protected virtual void Init()
    {
    }

    private void OnDestroy()
    {
    }

    public abstract void Clear();
}
