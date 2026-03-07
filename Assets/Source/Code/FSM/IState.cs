namespace Source.Code.FSM
{
    public interface IState<out TInitializer>
    {
        TInitializer Initializer { get; }
        void Enter();
        void Update();
        void Exit();
    }
}