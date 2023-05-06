using UnityEngine;
using UnityEngine.EventSystems;

public abstract class BaseScene : MonoBehaviour
{
    void Awake()
    {
        Init();

        EventSystem eventSystem = GameObject.FindObjectOfType<EventSystem>();
        if (eventSystem == null)
            Managers.Resource.Instantiate("EventSystem");
        else
        {
            eventSystem.gameObject.SetActive(true);
            eventSystem.enabled = true;
        }
    }

    protected abstract void Init();

    public abstract void Clear();
}
