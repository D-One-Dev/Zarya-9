using System;
using UnityEngine;
using UnityEngine.Events;

public class AnimationEventHandler : MonoBehaviour
{
    public UnityEvent action;

    public void InvokeAction()
    {
        action?.Invoke();
    }
}
