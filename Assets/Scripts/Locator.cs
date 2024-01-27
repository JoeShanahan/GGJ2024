using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Locator
{
    private static readonly Dictionary<Type, Component> instances = new();

    public static void Add(Type type, Component obj)
    {
        if (instances.TryGetValue(type, out Component previous) && previous != obj)
        {
            throw new InvalidOperationException(type + " can't be replaced. An instance is already set, remove the previous instance before you set a new one");
        }

        instances[type] = obj;
    }
    
    public static void Add<T>(T obj) where T : Component
    {
        if (instances.TryGetValue(typeof(T), out Component previous))
        {
            if (previous == obj)
            {
                return;
            }
            if (previous != null)
            {
                throw new InvalidOperationException(typeof(T) + "can be replaced while an instance is already set, remove the instance before you set a new one");
            }
        }
        
        instances[typeof(T)] = obj;
    }

    public static void Remove(Type type)
    {
        instances.Remove(type);
    }

    public static T Locate<T>() where T : Component
    {
        T obj = null;

        if(instances.TryGetValue(typeof(T), out var instance))
        {
            if (instance != null)
            {				
                obj = (T)instance;
            }			
        }
        
        if(obj == null)
        {
            if (UnityEngine.Object.FindObjectOfType<T>() is T component)
            {
                obj  = component;
            }
            else
            {
                obj = FindObjectOfTypeAll<T>();
            }

            if (obj == null)
            {
                throw new InvalidOperationException(typeof(T).Name + "is not Found in the hierarchy");
            }
            Add(obj);
        }
        
        return obj;
    }

    public static T FindObjectOfTypeAll<T>() where T : Component 
    {
        List<T> results = new List<T>();
        for(int i = 0; i < SceneManager.sceneCount; i++) 
        {
            Scene s = SceneManager.GetSceneAt(i);

            GameObject[] allGameObjects = s.GetRootGameObjects();
            for (int j = 0; j < allGameObjects.Length; j++) 
            {
                GameObject go = allGameObjects[j];					
                go.GetComponentsInChildren(true, results);
                if (results.Count > 0 )
                {
                    return results[0];
                }
            }  
        }     
        return null; 
    }	
}