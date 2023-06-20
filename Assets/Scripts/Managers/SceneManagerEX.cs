using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerEX : MonoBehaviour
{
    private BaseScene curScene;
    public BaseScene CurrentScene
    {
        get
        {
            if (curScene == null)
                curScene = GameObject.FindObjectOfType<BaseScene>();

            return curScene;
        }
    }

    public string nextScene;

    //private LoadingUI loadingUI;

    //private void Awake()
    //{
    //    LoadingUI loadingUI = GameManager.Resource.Load<LoadingUI>("PreFabs/UI/LoadingUI");
    //    this.loadingUI = Instantiate(loadingUI);
    //    this.loadingUI.transform.SetParent(transform);
    //}


    string GetSceneName(BaseScene scene)
    {
        string name = scene.name;
        return name;
    }

    public void LoadScene(BaseScene scene)
    {
        nextScene = scene.name;
        //SceneManager.LoadScene(GetSceneName(scene));
        StartCoroutine(LoadingRoutine(scene.name));
    }

    public void LoadScene(string name)
    {
        nextScene = name;
        //SceneManager.LoadScene(name);
        StartCoroutine(LoadingRoutine(name));
    }

    public AsyncOperation LoadSceneAsync(BaseScene nextScene)
    {
        return SceneManager.LoadSceneAsync(GetSceneName(nextScene));
    }

    public AsyncOperation LoadSceneAsync(string nextScene)
    {
        return SceneManager.LoadSceneAsync(nextScene);
    }


    IEnumerator LoadingRoutine(string sceneName)
    {
        AsyncOperation oper = LoadSceneAsync(sceneName);
        //oper.allowSceneActivation = false; // Scene Load �� ������ �ٷ� ������ �Ѿ���ʰ�
        Time.timeScale = 0f; // Loading �߿��� �ð� ����

        //loadingUI.SetProgress(0f);
        //loadingUI.FadeOut();

        yield return new WaitForSecondsRealtime(0.5f); // Wait fade out

        while (!oper.isDone)
        {
            //loadingUI.SetProgress(Mathf.Lerp(0f, 0.5f, oper.progress)); // Scene Loading for 50%
            yield return null;
        }

        //�߰��ε��Ұ͵� �ε�
        CurrentScene.LoadAsync();

        while (CurrentScene.progress < 1f)
        {
            //loadingUI.SetProgress(Mathf.Lerp(0.5f, 1f, curScene.progress));
            yield return null;
        }

        //oper.allowSceneActivation = true;
        //loadingUI.SetProgress(1f);
        //loadingUI.FadeIn();


        yield return new WaitForSecondsRealtime(0.5f); // wait Fade In
        Time.timeScale = 1f;
    }

    public void Clear()
    {
        CurrentScene.Clear();
    }
}