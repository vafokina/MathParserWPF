using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using MaterialDesignThemes.Wpf;
using MathParserWPF.View;

namespace MathParserWPF.ViewModel
{
   public class Controller : INotifyPropertyChanged
    {
        private MainWindow _mainWindow;
        private int _i = 0;
        private double _width;

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

        public Controller()
        {
            _mainWindow = (MainWindow)Application.Current.MainWindow;
            WindowWidth = 550;
            
        }

        private readonly PaletteHelper _paletteHelper = new PaletteHelper();

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
       

        
        private void ToggleBaseColour(bool isDark)
        {
            ITheme theme = _paletteHelper.GetTheme();
            IBaseTheme baseTheme = isDark ? new MaterialDesignDarkTheme() : (IBaseTheme)new MaterialDesignLightTheme();
            theme.SetBaseTheme(baseTheme);
            _paletteHelper.SetTheme(theme);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.HistoryListView.Add("String \n" + _i);
            _i++;
        }

        private void ToggleButton_OnChecked(object sender, RoutedEventArgs e)
        {
            var isChecked = ((ToggleButton)sender).IsChecked;
            ToggleBaseColour(isChecked != null && (bool)isChecked);
        }

        private void OpenHistoryButton_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.HistoryView.Visibility = Visibility.Visible;
            _mainWindow.OpenHistoryButton.Visibility = Visibility.Collapsed;

            double t = _mainWindow.HistoryListView.Width;
            WindowWidth += t;
        }

        private void CloseHistoryButton_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.HistoryView.Visibility = Visibility.Collapsed;
            _mainWindow.OpenHistoryButton.Visibility = Visibility.Visible;

            double t = _mainWindow.HistoryListView.Width;
            WindowWidth -= t;
        }

        private void ShiftHistoryButton_Click(object sender, RoutedEventArgs e)
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

        private void OperandButton_Click(object sender, RoutedEventArgs e)
        {

        }
        private void DigitButton_Click(object sender, RoutedEventArgs e)
        {

        }
        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
