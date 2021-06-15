using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MathParserWPF.ViewModel;

namespace MathParserWPF.View
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // главный ViewModel
        public Controller Controller { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            this.Controller = new Controller();

            this.DataContext = Controller;
        }

        private void MainWindow_OnKeyDown(object sender, KeyEventArgs e)
        {
            Controller.PhysicalKeyboardHandler.HandleKeyDown(e);
        }
        private void MainWindow_OnKeyUp(object sender, KeyEventArgs e)
        {
            Controller.PhysicalKeyboardHandler.HandleKeyUp(e);
        }
        private void Window_Closing(object sender, CancelEventArgs e)
        {
            Controller.HistoryManager.SaveHistory();
        }

        // если бы поле ввода было TextBox, это использовалось бы
        // для обновления binding при вводе с клавиатуры
        private void Input_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            var binding = ((TextBox)sender).GetBindingExpression(TextBox.TextProperty);
            binding.UpdateSource();
        }
    }
}
