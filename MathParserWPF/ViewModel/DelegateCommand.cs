using System;

namespace MathParserWPF.ViewModel
{
    public class DelegateCommand : IDelegateCommand
    {
        private readonly Action<object> _execute;
        private readonly Func<object, bool> _canExecute;

        // Событие, необходимое для ICommand
        public event EventHandler CanExecuteChanged;

        // Конструкторы
        public DelegateCommand(Action<object> execute, Func<object, bool> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }
        public DelegateCommand(Action<object> execute)
        {
            _execute = execute;
            _canExecute = AlwaysCanExecute;
        }

        // Методы, необходимые для ICommand
        public void Execute(object param)
        {
            _execute(param);
        }

        public bool CanExecute(object param)
        {
            return _canExecute(param);
        }

        // Метод, необходимый для IDelegateCommand
        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        // Метод CanExecute по умолчанию
        private bool AlwaysCanExecute(object param)
        {
            return true;
        }
    }
}
