using System;
using System.Windows.Input;

namespace NoteManager.ViewModel
{
    public class DefaultCommandHandler : ICommand
    {
        private Action _action;
        private bool _canExecute;
        public DefaultCommandHandler(Action action, bool canExecute)
        {
            _action = action;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            _action();
        }
    }

    public class DefaultTypedCommandHandler<T> : ICommand
    {
        private Action<object> _action;
        private bool _canExecute;
        public DefaultTypedCommandHandler(Action<object> action, bool canExecute)
        {
            _action = action;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            _action(parameter);
        }
    }
}
