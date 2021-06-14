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
        readonly char[] _permittedChars = { '1', '2' };
        private string _inputString = "";

        public event PropertyChangedEventHandler PropertyChanged;
        

        // Конструктор
        public VirtualKeyboardHandler()
        {
            this.AddCharacterCommand = new DelegateCommand(ExecuteAddCharacter);
            this.DeleteCharacterCommand =
                new DelegateCommand(ExecuteDeleteCharacter, CanExecuteDeleteCharacter);
        }

        // Открытые свойства
        public string InputString
        {
            set
            {
                bool previousCanExecuteDeleteChar = this.CanExecuteDeleteCharacter(null);

                if (this.SetProperty<string>(ref _inputString, value))
                {
                   // this.inputString = FormatText(inputString);

                    if (previousCanExecuteDeleteChar != this.CanExecuteDeleteCharacter(null))
                        this.DeleteCharacterCommand.RaiseCanExecuteChanged();
                }
            }

            get { return _inputString; }
        }

        // Реализация ICommand
        public IDelegateCommand AddCharacterCommand { protected set; get; }

        public IDelegateCommand DeleteCharacterCommand { protected set; get; }

        // Методы Execute() и CanExecute() 
        void ExecuteAddCharacter(object param)
        {
            this.InputString += param as string;
        }

        void ExecuteDeleteCharacter(object param)
        {
            this.InputString = this.InputString.Substring(0, this.InputString.Length - 1);
        }

        bool CanExecuteDeleteCharacter(object param)
        {
            return this.InputString.Length > 0;
        }

        // Закрытый метод, вызываемый из InputString
        private string FormatText(string str)
        {
          //  bool hasNonNumbers = str.IndexOfAny(_specialChars) != -1;
            string formatted = str;

            return formatted;
        }

        public static void HandleKeyDown(KeyEventArgs e)
        {
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

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
