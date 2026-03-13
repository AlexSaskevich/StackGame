using Cysharp.Threading.Tasks;

namespace Source.Code.FSM
{
    public interface IState<out TInitializer>
    {
        TInitializer Initializer { get; }
        UniTask Enter();
        void Update();
        UniTask Exit();
    }
}