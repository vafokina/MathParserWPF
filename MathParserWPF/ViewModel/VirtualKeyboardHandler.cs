using MathParserWPF.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MathParserWPF.ViewModel
{
    public class VirtualKeyboardHandler : INotifyPropertyChanged
    {
        private string _inputString = "";
        private string _displayString = "";
        private MainWindow _mainWindow;

        // Конструктор
        public VirtualKeyboardHandler()
        {
            _mainWindow = (MainWindow)Application.Current.MainWindow;
            AddCharacterCommand = new DelegateCommand(AddCharacter);
            DeleteCharacterCommand = new DelegateCommand(DeleteCharacter, CanExecuteDeleteCharacter);
            ClearCommand = new DelegateCommand(Clear, CanExecuteClear);
        }

        // Открытые свойства
        public string InputString
        {
            set
            {
                bool previousCanExecuteClear = this.CanExecuteClear(null);
                bool previousCanExecuteDeleteChar = this.CanExecuteDeleteCharacter(null);
                bool previousCanExecuteCalculate = _mainWindow.Controller.CanExecuteCalculate(null);

                value = InputChecker.CorrectString(value);
                //if (!InputChecker.PreCheckCharacters(value)) return;

                if (this.SetProperty<string>(ref _inputString, value))
                {
                     this.DisplayString = InputChecker.FormatText(_inputString);

                    if (previousCanExecuteClear != this.CanExecuteClear(null))
                        this.ClearCommand.RaiseCanExecuteChanged();
                    if (previousCanExecuteDeleteChar != this.CanExecuteDeleteCharacter(null))
                        this.DeleteCharacterCommand.RaiseCanExecuteChanged();
                    if (previousCanExecuteCalculate != _mainWindow.Controller.CanExecuteCalculate(null))
                        _mainWindow.Controller.CalculateCommand.RaiseCanExecuteChanged();
                }
            }

            get { return _inputString; }
        }

        public string DisplayString
        {
            protected set { this.SetProperty<string>(ref _displayString, value); }
            get { return _displayString; }
        }

        // Реализация ICommand
        public IDelegateCommand AddCharacterCommand { protected set; get; }
        public IDelegateCommand DeleteCharacterCommand { protected set; get; }
        public IDelegateCommand ClearCommand { protected set; get; }


        // Методы Execute() и CanExecute() 
        public void AddCharacter(object param)
        {
            this.InputString += param as string;
        }

        public void Clear(object param)
        {
            this.InputString = "";
        }

        public bool CanExecuteClear(object param)
        {
            return this.InputString.Length > 0;
        }

        public void DeleteCharacter(object param)
        {
            this.InputString = this.InputString.Substring(0, this.InputString.Length - 1);
        }

        public bool CanExecuteDeleteCharacter(object param)
        {
            return this.InputString.Length > 0;
        }

        protected bool SetProperty<T>(ref T storage, T value,
            [CallerMemberName] string propertyName = null)
        {
            if (object.Equals(storage, value))
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
