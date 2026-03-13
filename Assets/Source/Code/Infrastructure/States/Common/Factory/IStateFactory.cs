using Source.Code.FSM;
using Source.Code.Infrastructure.Core;

namespace Source.Code.Infrastructure.States.Common.Factory
{
    public interface IStateFactory
    {
        TState CreateState<TState>() where TState : IState<ICoreStateSystem>;
    }
}