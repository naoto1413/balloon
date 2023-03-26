using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalCoroutine : MonoBehaviour
{
    // singleton
    private static GlobalCoroutine instance;

    public static Coroutine Run(IEnumerator routine)
    {
        // check and create GameObject.
        if (instance == null)
        {
            GameObject obj = new GameObject();
            obj.name = "GlobalCoroutine";
            instance = obj.AddComponent<GlobalCoroutine>();
            DontDestroyOnLoad(obj);
        }

        return instance.StartCoroutine(instance.routine(routine));
    }

    private IEnumerator routine(IEnumerator src)
    {
        yield return StartCoroutine(src);
    }

}
