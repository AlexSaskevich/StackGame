using System;
using System.Collections.Generic;

namespace Source.Code.FSM
{
    public class StateMachine<TInitializer>
    {
        private const int DefaultStateCount = 8;

        private readonly Dictionary<Type, IState<TInitializer>> _states = new(DefaultStateCount);
        private readonly List<Transition<TInitializer>> _anyTransitions = new(DefaultStateCount);
        private readonly List<Transition<TInitializer>> _transitions = new(DefaultStateCount);

        public StateMachine(params IState<TInitializer>[] states)
        {
            if (states == null || states.Length == 0)
                return;

            AddStates(states);
        }

        public bool TransitionsEnabled { get; set; } = true;

        public bool HasCurrentState { get; private set; }

        public bool HasStatesBeenAdded { get; private set; }

        public IState<TInitializer> CurrentState { get; private set; }

        public Transition<TInitializer> CurrentTransition { get; private set; }

        public void AddStates(params IState<TInitializer>[] states)
        {
            if (HasStatesBeenAdded)
                throw new Exception("States have already been added!");

            if (states.Length == 0)
                throw new Exception("You're trying to add an empty state array!");

            foreach (var state in states)
                AddState(state);

            HasStatesBeenAdded = true;
        }

        public TState GetState<TState>() where TState : IState<TInitializer>
        {
            return (TState)GetState(typeof(TState));
        }

        public void SetState<TState>() where TState : IState<TInitializer>
        {
            if (CurrentState is TState)
                return;

            SetState(typeof(TState));
        }

        public void AddTransition<TStateFrom, TStateTo>(Func<bool> condition)
            where TStateFrom : IState<TInitializer>
            where TStateTo : IState<TInitializer>
        {
            if (condition == null)
                throw new ArgumentNullException(nameof(condition));

            var stateFrom = GetState(typeof(TStateFrom));
            var stateTo = GetState(typeof(TStateTo));

            _transitions.Add(new Transition<TInitializer>(stateFrom, stateTo, condition));
        }

        public void AddAnyTransition<TStateTo>(Func<bool> condition) where TStateTo : IState<TInitializer>
        {
            if (condition == null)
                throw new ArgumentNullException(nameof(condition));

            var stateTo = GetState(typeof(TStateTo));

            _anyTransitions.Add(new Transition<TInitializer>(null, stateTo, condition));
        }

        public void SetStateByTransitions()
        {
            CurrentTransition = GetTransition();

            if (CurrentTransition == null)
                return;

            if (CurrentState == CurrentTransition.To)
                return;

            SetState(CurrentTransition.To);
        }

        public void Run()
        {
            if (TransitionsEnabled)
                SetStateByTransitions();

            if (HasCurrentState)
                CurrentState.Update();
        }

        private void AddState(IState<TInitializer> state)
        {
            if (state == null)
                throw new ArgumentNullException(nameof(state));

            Type stateType = state.GetType();

            if (_states.TryAdd(stateType, state) == false)
                throw new Exception($"You're trying to add the same state twice! The <{stateType}> already exists!");
        }

        private IState<TInitializer> GetState(Type type)
        {
            return _states.TryGetValue(type, out var state)
                ? state
                : throw new Exception($"You didn't add the <{type}> state!");
        }

        private void SetState(Type type)
        {
            SetState(GetState(type));
        }

        private void SetState(IState<TInitializer> state)
        {
            if (HasCurrentState) 
                CurrentState.Exit();

            CurrentState = state;
            HasCurrentState = true;
            CurrentState.Enter();
        }

        private Transition<TInitializer> GetTransition()
        {
            for (var i = 0; i < _anyTransitions.Count; i++)
            {
                var anyTransition = _anyTransitions[i];

                if (anyTransition.Condition.Invoke())
                    return anyTransition;
            }

            for (var i = 0; i < _transitions.Count; i++)
            {
                var transition = _transitions[i];

                if (transition.From != CurrentState)
                    continue;

                if (transition.Condition.Invoke())
                    return transition;
            }

            return null;
        }
    }
}