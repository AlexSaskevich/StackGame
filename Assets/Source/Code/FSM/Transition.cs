using System;

namespace Source.Code.FSM
{
    public class Transition<TInitializer>
    {
        public Transition(IState<TInitializer> from, IState<TInitializer> to, Func<bool> condition)
        {
            From = from;
            To = to;
            Condition = condition;
        }

        public IState<TInitializer> From { get; private set; }
        public IState<TInitializer> To { get; private set; }
        public Func<bool> Condition { get; private set; }
    }
}