using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FileTag
{
    public class Command<T> : ICommand where T : class
    {
        public static Command<T> Create()
        {
            return new Command<T>();
        }

        private Action<T> _execute;
        private Func<T, bool> _canExecute;

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            var param = parameter as T;

            if (_canExecute == null)
                return true;

            return _canExecute(param);
        }

        public void Execute(object parameter)
        {
            var param = parameter as T;

            if (CanExecute(param))
                _execute(param);
        }

        public Command<T> Action(Action<T> method)
        {
            _execute = method;
            return this;
        }

        public Command<T> While(Func<T, bool> method)
        {
            _canExecute = method;

            //RiseCanExecuteChanged();
            return this;
        }

        public Command<T> Configure (Action<Command<T>> action)
        {
            action(this);

            return this;
        }

        public void RiseCanExecuteChanged()
        {
            RiseCanExecuteChanged(this);
        }

        public void RiseCanExecuteChanged(object invoker)
        {
            CanExecuteChanged?.Invoke(invoker, new EventArgs());
        }
    }
}
