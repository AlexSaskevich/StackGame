using Source.Code.Infrastructure.Core;
using Source.Code.Infrastructure.States.Common;

namespace Source.Code.Infrastructure.States
{
    public class GameLoopState : BaseState
    {
        public GameLoopState(ICoreStateSystem coreStateSystem) : base(coreStateSystem)
        {
        }
    }
}