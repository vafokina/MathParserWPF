using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using MaterialDesignThemes.Wpf;
using MathParserWPF.Model;
using MathParserWPF.Model.Data;
using MathParserWPF.View;

namespace MathParserWPF.ViewModel
{
    public class Controller : INotifyPropertyChanged
    {
        private MainWindow _mainWindow;
        private int _i = 0;
        private double _width;
        private double _height;

        // Конструктор
        public Controller()
        {
            _mainWindow = (MainWindow)Application.Current.MainWindow;
            WindowWidth = 550;
            WindowHeight = 450;

            HistoryManager = new HistoryManager();
            VirtualKeyboardHandler = new VirtualKeyboardHandler();
            PhysicalKeyboardHandler = new PhysicalKeyboardHandler();

            this.CalculateCommand = new DelegateCommand(Calculate, CanExecuteCalculate);
            this.CloseHistoryCommand = new DelegateCommand(CloseHistory);
            this.OpenHistoryCommand = new DelegateCommand(OpenHistory);
            this.ShiftHistoryCommand = new DelegateCommand(ShiftHistory);
        }

        // Открытые свойства
        public double WindowWidth
        {
            get { return _width; }
            set
            {
                if (value != _width)
                {
                    _width = value;
                    OnPropertyChanged("WindowWidth");
                }
            }
        }
        public double WindowHeight
        {
            get { return _height; }
            set
            {
                if (value != _height)
                {
                    _height = value;
                    OnPropertyChanged("WindowHeight");
                }
            }
        }

        // Другие ViewModels
        public HistoryManager HistoryManager { get; set; }
        public VirtualKeyboardHandler VirtualKeyboardHandler { get; set; }
        public PhysicalKeyboardHandler PhysicalKeyboardHandler { get; set; }

        // Реализация ICommand
        public IDelegateCommand CalculateCommand { protected set; get; }
        public IDelegateCommand OpenHistoryCommand { protected set; get; }
        public IDelegateCommand CloseHistoryCommand { protected set; get; }
        public IDelegateCommand ShiftHistoryCommand { protected set; get; }

        // Методы Execute() и CanExecute() 
        public void Calculate(object param)
        {
            string result, source;

            source = VirtualKeyboardHandler.InputString;
            //if (!InputChecker.CheckCharacters(source))
            //{
            //    MessageBox.Show("Выражение введено неверно");
            //    return;
            //}

            try
            {
                AstNode program = MathParser.Parse(source);
                result = MathInterpreter.Execute(program).ToString("#############0.##############", CultureInfo.InvariantCulture);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return;
            }
            VirtualKeyboardHandler.InputString = result;
            _mainWindow.Output.Text = source;
            MathExpression expression = new MathExpression(source, result);
            HistoryManager.AddNote(expression);
        }
        public bool CanExecuteCalculate(object param)
        {
            return (VirtualKeyboardHandler.InputString.Length > 0 &&
                    InputChecker.CheckGroups(VirtualKeyboardHandler.InputString));
        }

        private void OpenHistory(object param)
        {
            _mainWindow.HistoryView.Visibility = Visibility.Visible;
            _mainWindow.OpenHistoryButton.Visibility = Visibility.Collapsed;

            double w = _mainWindow.HistoryListView.Width;
            double h = _mainWindow.OpenHistoryButton.Height;
            WindowWidth = WindowWidth + w;
            WindowHeight = WindowHeight - h;
        }

        private void CloseHistory(object param)
        {
            _mainWindow.HistoryView.Visibility = Visibility.Collapsed;
            _mainWindow.OpenHistoryButton.Visibility = Visibility.Visible;

            double w = _mainWindow.HistoryListView.Width;
            double h = _mainWindow.OpenHistoryButton.Height;
            WindowWidth = WindowWidth - w;
            WindowHeight = WindowHeight + h;
        }

        private void ShiftHistory(object param)
        {
            if (Grid.GetColumn(_mainWindow.HistoryView) == 0)
            {
                Grid.SetColumn(_mainWindow.HistoryView, 1);
                Grid.SetColumn(_mainWindow.CalculatorView, 0);

                Grid.SetColumn(_mainWindow.ShiftHistoryButton, 0);
                Grid.SetColumn(_mainWindow.CloseHistoryButton, 2);
                _mainWindow.HistoryLabel.HorizontalAlignment = HorizontalAlignment.Right;
            }
            else
            {
                Grid.SetColumn(_mainWindow.HistoryView, 0);
                Grid.SetColumn(_mainWindow.CalculatorView, 1);

                Grid.SetColumn(_mainWindow.ShiftHistoryButton, 2);
                Grid.SetColumn(_mainWindow.CloseHistoryButton, 0);
                _mainWindow.HistoryLabel.HorizontalAlignment = HorizontalAlignment.Left;
            }
            GridLength t = _mainWindow.AppView.ColumnDefinitions.ElementAt(0).Width;
            _mainWindow.AppView.ColumnDefinitions.ElementAt(0).Width = _mainWindow.AppView.ColumnDefinitions.ElementAt(1).Width;
            _mainWindow.AppView.ColumnDefinitions.ElementAt(1).Width = t;
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
