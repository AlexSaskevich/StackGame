using Source.Code.FSM;
using Source.Code.Infrastructure.States;
using Source.Code.Infrastructure.States.Common.Factory;
using Zenject;

namespace Source.Code.Infrastructure.Core
{
    public class CoreStateSystem : ICoreStateSystem, ITickable, IInitializable
    {
        private StateMachine<ICoreStateSystem> _stateMachine;

        [Inject]
        private void Constructor(IStateFactory stateFactory, StateMachine<ICoreStateSystem> stateMachine)
        {
            _stateMachine = stateMachine;

            _stateMachine.AddStates
            (
                stateFactory.CreateState<BootstrapState>(),
                stateFactory.CreateState<MenuState>(),
                stateFactory.CreateState<LoadLevelState>(),
                stateFactory.CreateState<GameLoopState>()
            );

            _stateMachine.TransitionsEnabled = false;
        }

        public void SetState<TState>() where TState : IState<ICoreStateSystem>
        {
            _stateMachine.SetState<TState>();
        }

        public void Initialize()
        {
            _stateMachine.SetState<BootstrapState>();
        }

        public void Tick()
        {
            _stateMachine.Run();
        }
    }
}