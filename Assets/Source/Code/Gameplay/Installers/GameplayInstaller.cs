using Source.Code.Gameplay.Camera;
using Source.Code.Gameplay.Camera.OrthographicCameraProjection;
using UnityEngine;
using Zenject;

namespace Source.Code.Gameplay.Installers
{
    public class GameplayInstaller : MonoInstaller
    {
        [SerializeField] private UnityEngine.Camera _mainCamera;
        [SerializeField] private CameraConfig _cameraConfig;

        public override void InstallBindings()
        {
            Container.Bind<UnityEngine.Camera>().FromInstance(_mainCamera);
            Container.BindInterfacesTo<CameraSystem>().AsSingle();
            Container.Bind<CameraConfig>().FromScriptableObject(_cameraConfig).AsSingle();
            Container.BindInterfacesTo<OrthographicCameraProjectionProvider>().AsSingle();
        }
    }
}