using System;

namespace Source.Code.EventBus.Common
{
    public interface IEventBus
    {
        void Fire<TSignal>(TSignal signal);
        void Subscribe<TSignal>(Action<TSignal> callback);
        void Unsubscribe<TSignal>(Action<TSignal> callback);
    }
}