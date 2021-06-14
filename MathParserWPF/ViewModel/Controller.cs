using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
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

        public Controller()
        {
            _mainWindow = (MainWindow)Application.Current.MainWindow;
            WindowWidth = 550;
            this.CalculateCommand = new DelegateCommand(Calculate);
            this.CloseHistoryCommand = new DelegateCommand(CloseHistory);
            this.OpenHistoryCommand = new DelegateCommand(OpenHistory);
            this.ShiftHistoryCommand = new DelegateCommand(ShiftHistory);
        }


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

        // Реализация ICommand
        public IDelegateCommand CalculateCommand { protected set; get; }
        public IDelegateCommand OpenHistoryCommand { protected set; get; }
        public IDelegateCommand CloseHistoryCommand { protected set; get; }
        public IDelegateCommand ShiftHistoryCommand { protected set; get; }

        private void Calculate(object param)
        {
            string source = _mainWindow.Input.Text;
            AstNode program = MathParser.Parse(source);
            string result = MathInterpreter.Execute(program).ToString("#############0.##############", CultureInfo.InvariantCulture);
            _mainWindow.Input.Text = result;
            _mainWindow.Output.Text = source;
            MathExpression expression = new MathExpression(source, result);
            _mainWindow.HistoryManager.AddNote(expression);
        }

        private void OpenHistory(object param)
        {
            _mainWindow.HistoryView.Visibility = Visibility.Visible;
            _mainWindow.OpenHistoryButton.Visibility = Visibility.Collapsed;

            double t = _mainWindow.HistoryListView.Width;
            WindowWidth = WindowWidth + t;
        }

        private void CloseHistory(object param)
        {
            _mainWindow.HistoryView.Visibility = Visibility.Collapsed;
            _mainWindow.OpenHistoryButton.Visibility = Visibility.Visible;

            double t = _mainWindow.HistoryListView.Width;
            WindowWidth = WindowWidth - t;
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
