using System.ComponentModel;
using System.Globalization;
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
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            // задаем главный контроллер (модель представления)
            // вспомогательные указаны в представлении 
            this.DataContext = new Controller();
        }

        private readonly PaletteHelper _paletteHelper = new PaletteHelper();

        private void ToggleBaseColour(bool isDark)
        {
            ITheme theme = _paletteHelper.GetTheme();
            IBaseTheme baseTheme = isDark ? new MaterialDesignDarkTheme() : (IBaseTheme)new MaterialDesignLightTheme();
            theme.SetBaseTheme(baseTheme);
            _paletteHelper.SetTheme(theme);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string source = Input.Text;
            AstNode program = MathParser.Parse(source);
            string result = MathInterpreter.Execute(program).ToString("#############0.##############", CultureInfo.InvariantCulture);
            Input.Text = result;
            Output.Text = source;
            MathExpression expression = new MathExpression(source, result);
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
