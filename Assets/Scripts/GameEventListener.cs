using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class UnityTypesEvent : UnityEvent<GameObject> { }

public class GameEventListener : MonoBehaviour
{
    [SerializeField] private GameEvent _gEvent;
    [SerializeField] private UnityTypesEvent _actions = new UnityTypesEvent();

    private void OnEnable()
    {
        _gEvent.Register(this);
    }

    private void OnDisable()
    {
        _gEvent.UnRegister(this);
    }

    public void OnOccurs(GameObject go)
    {
        _actions.Invoke(go);
    }
}
