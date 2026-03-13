using Cysharp.Threading.Tasks;
using Source.Code.FSM;
using Source.Code.Infrastructure.Core;
using UnityEngine;

namespace Source.Code.Infrastructure.States.Common
{
    public abstract class BaseState : IState<ICoreStateSystem>
    {
        protected BaseState(ICoreStateSystem coreStateSystem)
        {
            Initializer = coreStateSystem;
        }

        public ICoreStateSystem Initializer { get; }

        public virtual UniTask Enter()
        {
            Debug.LogWarning($"Enter in <{GetType().Name}>");
            return UniTask.CompletedTask;
        }

        public virtual void Update()
        {
            // Debug.Log($"Update <{GetType().Name}> state");
        }

        public virtual UniTask Exit()
        {
            Debug.LogWarning($"Exit from <{GetType().Name}>");
            return UniTask.CompletedTask;
        }
    }
}