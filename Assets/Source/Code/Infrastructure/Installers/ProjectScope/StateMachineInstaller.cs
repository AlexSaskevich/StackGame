using Source.Code.FSM;
using Source.Code.Infrastructure.Core;
using Source.Code.Infrastructure.States;
using Source.Code.Infrastructure.States.Common.Factory;
using Zenject;

namespace Source.Code.Infrastructure.Installers.ProjectScope
{
    public class StateMachineInstaller : Installer<StateMachineInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<StateFactory>().AsSingle().NonLazy();
            Container.Bind<BootstrapState>().AsSingle().NonLazy();
            Container.Bind<MenuState>().AsSingle().NonLazy();
            Container.Bind<LoadLevelState>().AsSingle().NonLazy();
            Container.Bind<GameLoopState>().AsSingle().NonLazy();
            Container.Bind<StateMachine<ICoreStateSystem>>().AsCached().NonLazy();
        }
    }
}