

using System;
using System.Windows.Input;

namespace Somerpg.Client.Util
{
    public class Command<T> : ICommand
    {
        public Command(Action<T> action_, Func<T, bool> canExecute_ = null)
        {
            CommandAction = action_;
            CanExecuteFunc = canExecute_;
        }

        public Action<T> CommandAction { get; set; }
        public Func<T, bool> CanExecuteFunc { get; set; }

        public void Execute(object parameter)
        {
            CommandAction((T)parameter);
        }

        public bool CanExecute(object parameter)
        {
            return CanExecuteFunc == null || parameter == null || CanExecuteFunc((T)parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
    public class Command : ICommand
    {
        public Command(Action action_, Func<bool> canExecute_ = null)
        {
            CommandAction = action_;
            CanExecuteFunc = canExecute_;
        }

        public Action CommandAction { get; set; }
        public Func<bool> CanExecuteFunc { get; set; }

        public void Execute(object parameter)
        {
            CommandAction();
        }

        public bool CanExecute(object parameter)
        {
            return CanExecuteFunc == null || CanExecuteFunc();
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}
