using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Extend this class to create a Singleton instance
/// </summary>
/// <typeparam name="T"></typeparam>
public class SingletonType<T> : MonoBehaviour where T : Component
{

    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<T>();
                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.hideFlags = HideFlags.HideAndDontSave;
                    instance = obj.AddComponent<T>();
                }
            }
            return instance;
        }
    }

    public virtual void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if (instance == null)
        {
            instance = this as T;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
