using UnityEngine;
using Zenject;

namespace Source.Code.Infrastructure.Installers.SceneScope
{
    public class GameLoopInstaller : MonoInstaller
    {
        [SerializeField] private Camera _mainCamera;

        public override void InstallBindings()
        {
            Container.Bind<Camera>().FromInstance(_mainCamera);
        }
    }
}