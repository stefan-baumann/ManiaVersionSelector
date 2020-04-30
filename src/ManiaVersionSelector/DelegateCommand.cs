using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace ManiaVersionSelector
{
    public class DelegateCommand
        : ICommand
    {
        private readonly Predicate<object> condition;
        private readonly Action<object> action;

        public DelegateCommand(Action<object> action)
            : this(action, null)
        { }

        public DelegateCommand(Action<object> action, Predicate<object> condition)
        {
            this.action = action;
            this.condition = condition;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            if (this.condition == null)
            {
                return true;
            }

            return this.condition(parameter);
        }

        public void Execute(object parameter)
        {
            this.action(parameter);
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
