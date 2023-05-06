using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Util
{
    private static Dictionary<float, WaitForSeconds> waitDic = new Dictionary<float, WaitForSeconds>();
    public static WaitForSeconds WaitGet(float waitSec)
    {
        if (waitDic.TryGetValue(waitSec, out WaitForSeconds waittime)) return waittime;
        return waitDic[waitSec] = new WaitForSeconds(waitSec);
    }
    public static WaitForFixedUpdate waitFixed = new WaitForFixedUpdate();

    public static T GetOrAddComponent<T>(GameObject go) where T : UnityEngine.Component
    {
        T component = go.GetComponent<T>();
        if (component == null)
            component = go.AddComponent<T>();
        return component;
    }

    public static GameObject FindChild(GameObject go, string name = null, bool recursive = false)
    {
        Transform transform = FindChild<Transform>(go, name, recursive);
        if (transform == null)
            return null;

        return transform.gameObject;
    }

    public static T FindChild<T>(GameObject go, string name = null, bool recursive = false) where T : UnityEngine.Object
    {
        if (go == null)
            return null;

        if (recursive == false)
        {
            for (int i = 0; i < go.transform.childCount; i++)
            {
                Transform transform = go.transform.GetChild(i);
                if (string.IsNullOrEmpty(name) || transform.name == name)
                {
                    T component = transform.GetComponent<T>();
                    if (component != null)
                        return component;
                }
            }
        }
        else
        {
            foreach (T component in go.GetComponentsInChildren<T>(true))
            {
                if (string.IsNullOrEmpty(name) || component.name == name)
                    return component;
            }
        }
        return null;
    }

    public static T StringToEnum<T>(string name) where T : Enum
    {
        return (T)Enum.Parse(typeof(T), name);
    }

    public static Vector3 UiToRealPos(Vector2 uiPos)
    {
        return Camera.main.ScreenToWorldPoint(new Vector3(uiPos.x, uiPos.y, (Camera.main.nearClipPlane + Camera.main.farClipPlane) * 0.5f));
    }
    public static Vector2 RealPosToUi(Vector3 realPos)
    {
        return Camera.main.WorldToScreenPoint(realPos);
    }
}