using Source.Code.FSM;
using Source.Code.Infrastructure.Core;
using Zenject;

namespace Source.Code.Infrastructure.States.Common.Factory
{
    public class StateFactory : IStateFactory
    {
        private readonly DiContainer _container;

        public StateFactory(DiContainer container)
        {
            _container = container;
        }

        public TState CreateState<TState>() where TState : IState<ICoreStateSystem>
        {
            return _container.Resolve<TState>();
        }
    }
}