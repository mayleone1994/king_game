using UnityEngine;

public abstract class SubscriberBaseMonoBehaviourBase : MonoBehaviour
{
    protected bool _init = false;

    protected abstract void SubscribeToEvents();
    protected abstract void UnsubscribeToEvents();

    protected virtual void OnDestroy()
    {
        UnsubscribeToEvents();
    }
}

