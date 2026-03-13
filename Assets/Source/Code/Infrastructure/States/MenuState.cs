using Cysharp.Threading.Tasks;
using Source.Code.Infrastructure.Core;
using Source.Code.Infrastructure.States.Common;
using Source.Code.ScenesManagement;

namespace Source.Code.Infrastructure.States
{
    public class MenuState : BaseState
    {
        private readonly ISceneLoadSystem _sceneLoadSystem;

        public MenuState(ICoreStateSystem coreStateSystem, ISceneLoadSystem sceneLoadSystem) : base(coreStateSystem)
        {
            _sceneLoadSystem = sceneLoadSystem;
        }

        public override async UniTask Enter()
        {
            await base.Enter();
            await _sceneLoadSystem.LoadSceneAsync(Constants.MenuSceneName);
        }
    }
}