using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using MathParserWPF.Model;
using MathParserWPF.ViewModel;

namespace MathParserWPF.View
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new Controller();
        }

        private readonly PaletteHelper _paletteHelper = new PaletteHelper();

        public event PropertyChangedEventHandler PropertyChanged;



        private void ToggleBaseColour(bool isDark)
        {
            ITheme theme = _paletteHelper.GetTheme();
            IBaseTheme baseTheme = isDark ? new MaterialDesignDarkTheme() : (IBaseTheme)new MaterialDesignLightTheme();
            theme.SetBaseTheme(baseTheme);
            _paletteHelper.SetTheme(theme);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AstNode program = MathParser.Parse(Input.Text);
            Input.Text = MathInterpreter.Execute(program).ToString();
        }

        private void ToggleButton_OnChecked(object sender, RoutedEventArgs e)
        {
        }

        private void OpenHistoryButton_Click(object sender, RoutedEventArgs e)
        {
        }

        private void CloseHistoryButton_Click(object sender, RoutedEventArgs e)
        {
        }

        private void ShiftHistoryButton_Click(object sender, RoutedEventArgs e)
        {
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
        private void MainWindow_OnKeyDown(object sender, KeyEventArgs e)
        {
            VirtualKeyboardHandler.HandleKeyDown(e);
        }

        private void Input_OnTextChanged(object sender, TextChangedEventArgs e)
        {
                var binding = ((TextBox)sender).GetBindingExpression(TextBox.TextProperty);
                binding.UpdateSource();
        }
    }
}
