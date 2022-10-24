using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance = null;

    public static T Instance
    {
        get
        {
            if ((object)instance == null)
            {
                instance = (T)FindObjectOfType(typeof(T));

                if (instance == null)
                {
                    GameObject singletonObj = new GameObject(typeof(T).ToString());
                    instance = singletonObj.AddComponent<T>();
                }
            }

            return instance;
        }
    }
}
