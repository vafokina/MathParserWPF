using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MathParserWPF.View
{
    /// <summary>
    /// Логика взаимодействия для ButtonListControl.xaml
    /// </summary>
    public partial class ButtonListControl : UserControl
    {
        private int _maxCount;
        private List<Button> _buttons;

        public int MaxCount
        {
            get => _maxCount;
            set
            {
                if (value <= 0 || value > 1000) throw new Exception("Число вне зоны допустимых значений");
                _maxCount = value;
            }
        }

        public ButtonListControl() : this(20) { }
        public ButtonListControl(int maxCount)
        {
            MaxCount = maxCount;
            _buttons = new List<Button>(MaxCount);
            InitializeComponent();

            for (int i = 0; i < MaxCount; i++)
            {
                RowDefinition row = new RowDefinition();
                row.Height = GridLength.Auto;
                GridList.RowDefinitions.Add(row);
                Button button = new Button();
                button.SetResourceReference(FrameworkElement.StyleProperty, "ButtonDigitStyle");
                TextBlock textBlock = new TextBlock();
                textBlock.TextWrapping = TextWrapping.Wrap;
                textBlock.TextAlignment = TextAlignment.Right;
                textBlock.Text = "Sample\nResult";
                button.Content = textBlock;
                button.HorizontalContentAlignment = HorizontalAlignment.Right;
                button.Margin = new Thickness(1);
                button.Visibility = Visibility.Collapsed;
                GridList.Children.Add(button);
                Grid.SetRow(button, i);

                _buttons.Add(button);
            }
        }

        public void Add(string s, ICommand command, object param, object viewModel)
        {
            for (int i = MaxCount - 2; i >= 0; i--)
            {
                if (_buttons[i].Visibility == Visibility.Collapsed) continue;
                _buttons[i + 1].Content = _buttons[i].Content;
                _buttons[i + 1].DataContext = _buttons[i].DataContext;
                _buttons[i + 1].Command = _buttons[i].Command;
                _buttons[i + 1].CommandParameter = _buttons[i].CommandParameter;
                _buttons[i + 1].Visibility = Visibility.Visible;
            }
            TextBlock textBlock = new TextBlock();
            textBlock.TextWrapping = TextWrapping.Wrap;
            textBlock.TextAlignment = TextAlignment.Right;
            textBlock.Text = s;

            _buttons[0].Content = textBlock;
            _buttons[0].DataContext = viewModel;
            _buttons[0].Command = command;
            _buttons[0].CommandParameter = param;
            _buttons[0].Visibility = Visibility.Visible;
        }

    }
}
