using Cysharp.Threading.Tasks;
using Source.Code.Infrastructure.Core;
using Source.Code.Infrastructure.States.Common;
using Source.Code.ScenesManagement;

namespace Source.Code.Infrastructure.States
{
    public class LoadLevelState : BaseState
    {
        private readonly ISceneLoadSystem _sceneLoadSystem;

        public LoadLevelState(ICoreStateSystem coreStateSystem, ISceneLoadSystem sceneLoadSystem) : base(coreStateSystem)
        {
            _sceneLoadSystem = sceneLoadSystem;
        }

        public override async UniTask Enter()
        {
            await base.Enter();
            await _sceneLoadSystem.LoadSceneAsync(Constants.MainSceneName);
            Initializer.SetState<GameLoopState>();
        }
    }
}