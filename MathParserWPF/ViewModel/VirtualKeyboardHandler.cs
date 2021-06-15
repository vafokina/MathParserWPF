using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MathParserWPF.ViewModel
{
    public class VirtualKeyboardHandler : INotifyPropertyChanged
    {
        private Controller _controller;
        private string _inputString = "";
        private string _displayString = "";

        // Конструктор
        public VirtualKeyboardHandler(Controller controller)
        {
            _controller = controller;
            AddCharacterCommand = new DelegateCommand(AddCharacter);
            DeleteCharacterCommand = new DelegateCommand(DeleteCharacter, CanExecuteDeleteCharacter);
            ClearCommand = new DelegateCommand(Clear, CanExecuteClear);
        }

        // Открытые свойства
        // Строка ввода
        public string InputString
        {
            set
            {
                bool previousCanExecuteClear = CanExecuteClear(null);
                bool previousCanExecuteDeleteChar = CanExecuteDeleteCharacter(null);
                bool previousCanExecuteCalculate = _controller.CanExecuteCalculate(null);

                value = InputChecker.CorrectString(value);
                //if (!InputChecker.PreCheckCharacters(value)) return;

                if (SetProperty(ref _inputString, value))
                {
                     DisplayString = InputChecker.FormatText(_inputString);

                    if (previousCanExecuteClear != CanExecuteClear(null))
                        ClearCommand.RaiseCanExecuteChanged();
                    if (previousCanExecuteDeleteChar != CanExecuteDeleteCharacter(null))
                        DeleteCharacterCommand.RaiseCanExecuteChanged();
                    if (previousCanExecuteCalculate != _controller.CanExecuteCalculate(null))
                        _controller.CalculateCommand.RaiseCanExecuteChanged();
                }
            }

            get => _inputString;
        }
        // Отображение строки ввода в нужном формате (в представлении)
        public string DisplayString
        {
            protected set => SetProperty(ref _displayString, value);
            get => _displayString;
        }

        // Реализация ICommand
        public IDelegateCommand AddCharacterCommand { protected set; get; }
        public IDelegateCommand DeleteCharacterCommand { protected set; get; }
        public IDelegateCommand ClearCommand { protected set; get; }


        // Методы Execute() и CanExecute() 
        public void AddCharacter(object param)
        {
            InputString += param as string;
        }

        public void Clear(object param)
        {
            InputString = "";
        }

        public bool CanExecuteClear(object param)
        {
            return InputString.Length > 0;
        }

        public void DeleteCharacter(object param)
        {
            InputString = InputString.Substring(0, InputString.Length - 1);
        }

        public bool CanExecuteDeleteCharacter(object param)
        {
            return InputString.Length > 0;
        }

        protected bool SetProperty<T>(ref T storage, T value,
            [CallerMemberName] string propertyName = null)
        {
            if (Equals(storage, value))
                return false;

            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
