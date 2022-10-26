using UnityEngine.Events;
using UnityEngine;
[System.Serializable]
public class Response_Event
{
    [HideInInspector] public string name;
    [SerializeField] private UnityEvent onPickedResponse;

    public UnityEvent OnPickedResponse => onPickedResponse;
}
