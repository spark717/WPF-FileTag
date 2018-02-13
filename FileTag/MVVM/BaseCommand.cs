using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace FileTag.MVVM
{
    public class BaseCommand<T> : ICommand
    {
        public BaseCommand()
        {
            _actions = new Dictionary<int, Action>();
            _paramActions = new Dictionary<int, Action<T>>();
            _conditions = new Dictionary<int, Func<bool>>();
            _paramConditions = new Dictionary<int, Func<T, bool>>();
        }

        private int _actionsCount = 0;
        private int _conditionsCount = 0;
        private IDictionary<int, Action> _actions;
        private IDictionary<int, Action<T>> _paramActions;
        private IDictionary<int, Func<bool>> _conditions;
        private IDictionary<int, Func<T, bool>> _paramConditions;

        public event EventHandler CanExecuteChanged;

        public void RiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }

        public bool CanExecute(object parameter)
        {
            T param = default(T);

            if (parameter?.GetType() == typeof(T))
                param = (T)parameter;

            for (int i = 0; i < _conditionsCount; i++)
            {
                if (_conditions.ContainsKey(i))
                    if (!_conditions[i]())
                        return false;

                if (_paramConditions.ContainsKey(i))
                    if (!_paramConditions[i](param))
                        return false;
            }

            return true;
        }

        public void Execute(object parameter)
        {
            T param = default(T);

            if (parameter?.GetType() == typeof(T))
                param = (T)parameter;

            for (int i = 0; i < _actionsCount; i++)
            {
                if (_actions.ContainsKey(i))
                    _actions[i]();

                if (_paramActions.ContainsKey(i))
                    _paramActions[i](param);
            }
        }

        public BaseCommand<T> Subscribe(Action<T> action)
        {
            _paramActions.Add(_actionsCount++, action);
            return this;
        }

        public BaseCommand<T> Subscribe(Action action)
        {
            _actions.Add(_actionsCount++, action);
            return this;
        }

        public BaseCommand<T> While(Func<bool> condition)
        {
            _conditions.Add(_conditionsCount++, condition);
            return this;
        }

        public BaseCommand<T> While(Func<T, bool> condition)
        {
            _paramConditions.Add(_conditionsCount++, condition);
            return this;
        }

        public BaseCommand<T>Configure(Action<BaseCommand<T>> configuration)
        {
            configuration(this);
            return this;
        }

        public BaseCommand<T> ReactOn<P>(BaseCommand<P> command) where P: class
        {
            command.Subscribe(RiseCanExecuteChanged);
            return this;
        }

        public BaseCommand<T> ReactOnSelf()
        {
            Subscribe(RiseCanExecuteChanged);
            return this;
        }
    }
}
