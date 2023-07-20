using UnityEngine;

public abstract class SubscriberBase : MonoBehaviour
{
    protected bool _init = false;

    protected abstract void SubscribeToEvents();
    protected abstract void UnsubscribeToEvents();

    protected virtual void OnDestroy()
    {
        UnsubscribeToEvents();
    }
}
