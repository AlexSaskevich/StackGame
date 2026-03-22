using System;
using Source.Code.EventBus.Common;
using Zenject;

namespace Source.Code.EventBus
{
    public class ZenjectEventBus : IEventBus
    {
        private readonly SignalBus _signalBus;

        public ZenjectEventBus(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public void Fire<TSignal>(TSignal signal)
        {
            _signalBus.Fire(signal);
        }

        public void Subscribe<TSignal>(Action<TSignal> callback)
        {
            _signalBus.Subscribe(callback);
        }

        public void Unsubscribe<TSignal>(Action<TSignal> callback)
        {
            _signalBus.Unsubscribe(callback);
        }
    }
}