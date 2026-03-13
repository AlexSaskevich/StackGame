using Source.Code.FSM;

namespace Source.Code.Infrastructure.Core
{
    public interface ICoreStateSystem
    {
        void SetState<TState>() where TState : IState<ICoreStateSystem>;
    }
}