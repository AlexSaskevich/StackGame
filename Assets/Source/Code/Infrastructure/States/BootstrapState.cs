using Cysharp.Threading.Tasks;
using Source.Code.Infrastructure.Core;
using Source.Code.Infrastructure.States.Common;
using Source.Code.ScenesManagement;
using Zenject;

namespace Source.Code.Infrastructure.States
{
    public class BootstrapState : BaseState
    {
        private readonly ISceneLoadSystem _sceneLoadSystem;

        [Inject]
        public BootstrapState(ICoreStateSystem coreStateSystem, ISceneLoadSystem sceneLoadSystem) : base(
            coreStateSystem)
        {
            _sceneLoadSystem = sceneLoadSystem;
        }

        public override async UniTask Enter()
        {
            await base.Enter();
            await _sceneLoadSystem.LoadSceneAsync(Constants.BootstrapSceneName);
            Initializer.SetState<MenuState>();
        }
    }
}