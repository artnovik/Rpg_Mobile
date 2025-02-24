﻿using UnityEngine;

namespace TDC
{
    public class MonoBehaviourSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        protected static T instance;

        public static T Instance
        {
            get
            {
                if (instance != null)
                {
                    return instance;
                }

                Object component = FindObjectOfType(typeof(T));
                if (component == null)
                {
                    instance = new GameObject(typeof(T).ToString()).AddComponent<T>();
                }
                else
                {
                    instance = component as T;
                }

                if (instance == null)
                {
                    Debug.LogError("An instance of " + typeof(T) + " is not found or can't be created !!!");
                }

                return instance;
            }
        }

        public static void Delete()
        {
            Debug.Log("Deleting MonoBehaviourSingleton of type: " + typeof(T));
            instance = default(T);
        }
    }
}