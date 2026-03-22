using Source.Code.EventBus;
using Source.Code.Infrastructure.Core;
using Source.Code.ScenesManagement;
using Zenject;

namespace Source.Code.Infrastructure.Installers.ProjectScope
{
    public class InfrastructureInstaller : Installer<InfrastructureInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<SceneLoadingSystem>().AsSingle().NonLazy();
            Container.BindInterfacesTo<CoreStateSystem>().AsSingle().NonLazy();

            Container
                .Bind<UnityEventsDispatcher>()
                .FromNewComponentOnNewGameObject()
                .WithGameObjectName(nameof(UnityEventsDispatcher))
                .AsSingle()
                .NonLazy();

            InstallEventBus();
        }

        private void InstallEventBus()
        {
            SignalBusInstaller.Install(Container);
            SignalsInstaller.Install(Container);
            Container.BindInterfacesTo<ZenjectEventBus>().AsSingle().NonLazy();
        }
    }
}