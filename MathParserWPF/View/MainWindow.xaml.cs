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
        public HistoryManager HistoryManager { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            // задаем главный контроллер (модель представления)
            // вспомогательные указаны в представлении 
            VMContainer vmContainer = new VMContainer(new Controller(), new HistoryManager(), new VirtualKeyboardHandler());
            this.DataContext = vmContainer.Controller; //new Controller();
            HistoryManager = vmContainer.HistoryManager;  // new HistoryManager();
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

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            HistoryManager.SaveHistory();
        }
    }
}
