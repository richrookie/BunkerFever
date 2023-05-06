using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public abstract class UI_Base : MonoBehaviour
{
    protected Dictionary<Type, UnityEngine.Object[]> _objects = new Dictionary<Type, UnityEngine.Object[]>();

    public abstract void Init();

    protected void Bind<T>(Type type) where T : UnityEngine.Object
    {
        string[] names = Enum.GetNames(type);
        UnityEngine.Object[] objects = new UnityEngine.Object[names.Length];
        _objects.Add(typeof(T), objects);

        for (int i = 0; i < names.Length; i++)
        {
            if (typeof(T) == typeof(GameObject))
                objects[i] = Util.FindChild(gameObject, names[i], true);
            else
                objects[i] = Util.FindChild<T>(gameObject, names[i], true);

            if (objects[i] == null)
                Debug.Log($"Failed to bind{names[i]}");
        }
    }

    protected T Get<T>(Enum idx) where T : UnityEngine.Object
    {
        UnityEngine.Object[] objects = null;

        if (_objects.TryGetValue(typeof(T), out objects) == false)
            return null;

        return objects[Convert.ToInt32(idx)] as T;
    }

    protected GameObject GetObject(Enum idx) => Get<GameObject>(idx);
    protected Text GetText(Enum idx) => Get<Text>(idx);
    protected Button GetButton(Enum idx) => Get<Button>(idx);
    protected Image GetImage(Enum idx) => Get<Image>(idx);
    protected TextMeshProUGUI GetTMProUGUI(Enum idx) { return Get<TextMeshProUGUI>(idx); }
}
