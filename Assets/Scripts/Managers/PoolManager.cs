using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UIElements;

public class PoolManager : MonoBehaviour
{
    // ObjectPool 유니티 공식 지원 풀 사용
    // 풀이 없으면 만든다.

    //key는 오브젝트의 이름으로 하기로 약속해둔다.

    Dictionary<string, ObjectPool<GameObject>> poolDic; //사용할 풀
    Dictionary<string, Transform> poolContainer;// 풀 별로 모을 컨테이너
    
    Transform poolRoot; // 모든 풀들을 가지고있을 곳

    Transform DontDestroyPoolRoot; // 씬 전환시 없어지지않는 풀
    //UI 풀
    Canvas canvasRoot;



    private void Awake()
    {
        //Init();
    }

    //Init new Pool Setting
    public void Init()
    {
        poolDic = new Dictionary<string, ObjectPool<GameObject>>();

        poolContainer = new Dictionary<string, Transform>();
        poolRoot = new GameObject("PoolRoot").transform;

        canvasRoot = GameManager.Resource.Instantiate<Canvas>("UI/Canvas");
        
        DontDestroyPoolRoot = new GameObject("DontDestroyPoolRoot").transform;
        DontDestroyPoolRoot.transform.parent = transform;
    }

    public void Reset()
    {
        canvasRoot = GameManager.Resource.Instantiate<Canvas>("UI/Canvas");

        //poolDic = new Dictionary<string, ObjectPool<GameObject>>();
        //poolContainer = new Dictionary<string, Transform>();
        //poolRoot = new GameObject("PoolRoot").transform;

        //canvasRoot = GameManager.Resource.Instantiate<Canvas>("UI/Canvas");

    }

    public void ResetOnDestroy() 
    {
        if (DontDestroyPoolRoot != null)
        {
            Destroy(DontDestroyPoolRoot.gameObject);
        }

        DontDestroyPoolRoot = new GameObject("DontDestroyPoolRoot").transform;
        DontDestroyPoolRoot.transform.parent = transform;
    }

    public void CleanUpDontDestroyRoot() 
    { // 비워있는 컨테이너 삭제
        if (DontDestroyPoolRoot != null && DontDestroyPoolRoot.childCount > 0)
        {

            for (int i = 0; i < DontDestroyPoolRoot.childCount; i++)
            {
                var container = DontDestroyPoolRoot.GetChild(i);

                if (container.childCount <= 0)
                {
                    Destroy(container.gameObject);
                }

            }
        }
    }

    public void DestroyContainer(GameObject obj)
    {
        if (DontDestroyPoolRoot != null && DontDestroyPoolRoot.childCount > 0)
        {

            for (int i = 0; i < DontDestroyPoolRoot.childCount; i++)
            {
                var container = DontDestroyPoolRoot.GetChild(i);

                if (container.childCount > 0)
                {
                    
                    if (container.GetChild(0).gameObject == obj)
                    {
                        Destroy(container.gameObject);
                    }
                }


            }
        }
    }



    public T Get<T>(bool IsDontDestroy,T original, Vector3 position, Quaternion rotation, Transform parent, string suffix = "") where T : Object
    {
        if (original is GameObject) // gameObject 일때
        {
            GameObject prefab = original as GameObject;
            string key = prefab.name; // 키를 오브젝트의 이름으로


            if (suffix != "")
            {
                key += suffix;
            }
            GameObject obj;

         
            if (!poolDic.ContainsKey(key)) // 이미 키로 설정되어 있지않으면(해당하는 이름의 풀이없으면)
                CreatePool(key, prefab, IsDontDestroy); // 풀로만든다.

            obj = poolDic[key].Get(); // 키로 이미 있으면 가져와서 쓰고 없으면 위에 만든 풀에서 가져와서쓴다

       
            //해당하는 오브젝트에 부모,위치,포지션,로테이션 설정
            obj.transform.parent = parent;
            obj.transform.position = position;
            obj.transform.rotation = rotation;
            return obj as T; //만든거 리턴
        }
        else if (original is Component) // Component 일때 
        {
            Component component = original as Component;
            string key = component.gameObject.name;// 키를 해당 컴포넌트가 가지고있는 오브젝트의 이름으로

            if (suffix != "")
            {
                key += suffix;
            }

            GameObject obj;

         
            if (!poolDic.ContainsKey(key)) // 이미 키로 설정되어 있지않으면(해당하는 이름의 풀이없으면)
                CreatePool(key, component.gameObject, IsDontDestroy); // 풀로만든다.

            obj = poolDic[key].Get(); // 키로 이미 있으면 가져와서 쓰고 없으면 위에 만든 풀에서 가져와서쓴다

       
 
            //해당하는 오브젝트에 부모,위치,포지션,로테이션 설정
            obj.transform.parent = parent;
            obj.transform.position = position;
            obj.transform.rotation = rotation;
            return obj.GetComponent<T>(); // 만든거의 컴포넌트 리턴
        }
        else // 게임오브젝트도아니고 컴포넌트도 아닐때 아무것도안함
        {
            return null;
        }
    }


    // 아래의 Get 함수들은 각각 하나의 값들씩 안넣을때 오버로딩
    // 안넣은 값은 기본값으로 넣어준다.

    public T Get<T>(bool IsDontDestroy, T original, Vector3 position, Quaternion rotation , string suffix) where T : Object
    {
        return Get<T>(IsDontDestroy, original, position, rotation, null , suffix);
    }


    public T Get<T>(bool IsDontDestroy, T original, Vector3 position, Quaternion rotation) where T : Object
    {
        return Get<T>(IsDontDestroy, original, position, rotation, null);
    }

    public T Get<T>(bool IsDontDestroy,T original, Transform parent) where T : Object
    {
        return Get<T>(IsDontDestroy, original, Vector3.zero, Quaternion.identity, parent);
    }

    public T Get<T>(bool IsDontDestroy, T original) where T : Object
    {
        return Get<T>(IsDontDestroy, original, Vector3.zero, Quaternion.identity, null);
    }



    //풀 에서 해제 / 릴리즈
    //풀에 없으면 
    public bool Release<T>(T instance) where T : Object
    {
        if (instance is GameObject)
        {
            GameObject go = instance as GameObject;
            string key = go.name;

            if (!poolDic.ContainsKey(key)) // 해당하는 key 가 풀에없으면 당연히 릴리즈 실패
                return false;

            poolDic[key].Release(go); // 여기에 Release 는 유니티의 ObjectPool 릴리즈 를 사용
            return true;
        }
        else if (instance is Component)
        {
            Component component = instance as Component;
            string key = component.gameObject.name;

            if (!poolDic.ContainsKey(key))// 해당하는 key 가 풀에없으면 당연히 릴리즈 실패
                return false;

            poolDic[key].Release(component.gameObject); // 컴포넌트라 컴포넌트를 가지고있는 오브젝트를 릴리즈한다.
            return true;
        }
        else
        {
            return false;
        }
    }

    //풀안에 해당하는 오브젝트가 있는 지 확인하는 함수
    public bool IsContain<T>(T original) where T : Object
    {
        if (original is GameObject)
        {
            GameObject prefab = original as GameObject;
            string key = prefab.name;

            if (poolDic.ContainsKey(key))
                return true;
            else
                return false;

        }
        else if (original is Component)
        {
            Component component = original as Component;
            string key = component.gameObject.name;

            if (poolDic.ContainsKey(key))
                return true;
            else
                return false;
        }
        else
        {
            return false;
        }
    }

    //풀이 없을때 만드는 함수
    private void CreatePool(string key, GameObject prefab , bool IsDontDestroy)
    {
        GameObject root = new GameObject();
        root.gameObject.name = $"{key}Container";

        if (IsDontDestroy == true)
        {
            root.transform.parent = DontDestroyPoolRoot;
        }
        else 
        {
            root.transform.parent = poolRoot;
        }

      
        poolContainer.Add(key, root.transform);

        //유니티 지원 ObjectPool 을 만들때 
        // 만들때,가져올때,릴리즈할때,지울때 에 액션을 넣어줘야한다.
        ObjectPool<GameObject> pool = new ObjectPool<GameObject>(
            createFunc: () =>
            {
                GameObject obj = Instantiate(prefab);
                obj.gameObject.name = key;
                return obj;
            },
            actionOnGet: (GameObject obj) =>
            {
                obj.gameObject.SetActive(true);
                obj.transform.parent = null;
            },
            actionOnRelease: (GameObject obj) =>
            {
                obj.gameObject.SetActive(false);
                obj.transform.parent = poolContainer[key];
            },
            actionOnDestroy: (GameObject obj) =>
            {
                Destroy(obj);
            }
            );


            poolDic.Add(key, pool);

    }


    // UI 풀
    public T GetUI<T>(T original, Vector3 position, string suffix = "") where T : Object
    {
        if (original is GameObject)
        {
            GameObject prefab = original as GameObject;
            string key = prefab.name;

            if (suffix != "")
            {
                key += suffix;
            }
            

            if (!poolDic.ContainsKey(key))
                CreateUIPool(key, prefab);

            GameObject obj = poolDic[key].Get();
            obj.transform.position = position;
            return obj as T;
        }
        else if (original is Component)
        {
            Component component = original as Component;
            string key = component.gameObject.name;

            if (suffix != "")
            {
                key += suffix;
            }

            if (!poolDic.ContainsKey(key))
                CreateUIPool(key, component.gameObject);

            GameObject obj = poolDic[key].Get();
            obj.transform.position = position;
            return obj.GetComponent<T>();
        }
        else
        {
            return null;
        }
    }

    public T GetUI<T>(T original, Transform parent, string suffix = "") where T : Object
    {
        if (original is GameObject)
        {
            GameObject prefab = original as GameObject;
            string key = prefab.name;

            if (suffix != "")
            {
                key += suffix;
            }

            if (!poolDic.ContainsKey(key))
                CreateUIPool(key, prefab);

            GameObject obj = poolDic[key].Get();
            //obj.transform.position = position;
            obj.transform.SetParent(parent, false);
            return obj as T;
        }
        else if (original is Component)
        {
            Component component = original as Component;
            string key = component.gameObject.name;

            if (suffix != "")
            {
                key += suffix;
            }

            if (!poolDic.ContainsKey(key))
                CreateUIPool(key, component.gameObject);

            GameObject obj = poolDic[key].Get();
            //obj.transform.position = position;
            obj.transform.SetParent(parent, false);
            return obj.GetComponent<T>();
        }
        else
        {
            return null;
        }
    }

    public T GetUI<T>(T original, string suffix = "") where T : Object
    {
        if (original is GameObject)
        {
            GameObject prefab = original as GameObject;
            string key = prefab.name;

            if (suffix != "")
            {
                key += suffix;
            }

            if (!poolDic.ContainsKey(key))
                CreateUIPool(key, prefab);

            GameObject obj = poolDic[key].Get();
            return obj as T;
        }
        else if (original is Component)
        {
            Component component = original as Component;
            string key = component.gameObject.name;

            if (suffix != "")
            {
                key += suffix;
            }

            if (!poolDic.ContainsKey(key))
                CreateUIPool(key, component.gameObject);

            GameObject obj = poolDic[key].Get();
            return obj.GetComponent<T>();
        }
        else
        {
            return null;
        }
    }

    public bool ReleaseUI<T>(T instance) where T : Object
    {
        if (instance is GameObject)
        {
            GameObject go = instance as GameObject;
            string key = go.name;

            if (!poolDic.ContainsKey(key))
                return false;

            poolDic[key].Release(go);
            return true;
        }
        else if (instance is Component)
        {
            Component component = instance as Component;
            string key = component.gameObject.name;

            if (!poolDic.ContainsKey(key))
                return false;

            poolDic[key].Release(component.gameObject);
            return true;
        }
        else
        {
            return false;
        }
    }

    // UI 는 캔버스에 넣어두고 다시빼서 이동시키는게
    // 위치값이 안이상해진다.
    private void CreateUIPool(string key, GameObject prefab)
    {
        ObjectPool<GameObject> pool = new ObjectPool<GameObject>(
            createFunc: () =>
            {
                GameObject obj = Instantiate(prefab);
                obj.gameObject.name = key;
                return obj;
            },
            actionOnGet: (GameObject obj) =>
            {
                obj.gameObject.SetActive(true);
            },
            actionOnRelease: (GameObject obj) =>
            {
                obj.gameObject.SetActive(false);
                obj.transform.SetParent(canvasRoot.transform, false);
            },
            actionOnDestroy: (GameObject obj) =>
            {
                Destroy(obj);
            }
            );
        poolDic.Add(key, pool);
    }


}
