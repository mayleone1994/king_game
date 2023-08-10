namespace KingGame
{
    public abstract class SubscriberBase
    {
        protected bool _init = false;

        protected abstract void SubscribeToEvents();
        protected abstract void UnsubscribeToEvents();
        ~SubscriberBase()
        {
            UnsubscribeToEvents();
        }
    }
}
