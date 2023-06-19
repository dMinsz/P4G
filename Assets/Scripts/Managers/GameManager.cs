using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //gameManager
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    //SceneManager
    private static SceneManagerEX sceneManager;
    public static SceneManagerEX Scene { get { return sceneManager; } }

    //PoolManager
    private static PoolManager poolManager;

    public static PoolManager Pool { get { return poolManager; } }

    //ResourceManager
    private static ResourceManager resourceManager;
    public static ResourceManager Resource { get { return resourceManager; } }

    private static UIManager uiManager;
    public static UIManager UI { get { return uiManager; } }


    private GameManager() { }


    private void Awake() // ����Ƽ������ �����ͻ󿡼� �߰��Ҽ� �ֱ⶧���� �̷������α���
    {
        if (instance != null)
        {
            Debug.LogWarning("GameInstance: valid instance already registered.");
            Destroy(this);
            return;
        }

        DontDestroyOnLoad(this); // ����Ƽ�� ���� ��ȯ�ϸ� �ڵ����� ������Ʈ���� �����ȴ�
                                 // �ش� �ڵ�� ���� ���ϰ� ����
        instance = this;

        InitManagers();
    }

    private void OnDestroy()
    {
        if (instance == this)
            instance = null;
    }

    private void InitManagers()
    {

        GameObject resourceObj = new GameObject();
        resourceObj.name = "ResourceManager";
        resourceObj.transform.SetParent(transform);
        resourceManager = resourceObj.AddComponent<ResourceManager
            >();

        GameObject poolObj = new GameObject();
        poolObj.name = "PoolManager";
        poolObj.transform.SetParent(transform);
        poolManager = poolObj.AddComponent<PoolManager>();

        GameObject uiObj = new GameObject();
        uiObj.name = "UIManager";
        uiObj.transform.parent = transform;
        uiManager = uiObj.AddComponent<UIManager>();

        GameObject sObj = new GameObject();
        sObj.name = "SceneManagerEX";
        sObj.transform.SetParent(transform);
        sceneManager = sObj.AddComponent<SceneManagerEX>();

    }
}
