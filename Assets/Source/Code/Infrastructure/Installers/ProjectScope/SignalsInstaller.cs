using Source.Code.Gameplay.Blocks.Signals;
using Zenject;

namespace Source.Code.Infrastructure.Installers.ProjectScope
{
    public class SignalsInstaller : Installer<SignalsInstaller>
    {
        public override void InstallBindings()
        {
            Container.DeclareSignal<BlockSpawnedSignal>();
        }
    }
}