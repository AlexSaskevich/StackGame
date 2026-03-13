using Zenject;

namespace Source.Code.Infrastructure.Installers.ProjectScope
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            InfrastructureInstaller.Install(Container);
            StateMachineInstaller.Install(Container);
        }
    }
}