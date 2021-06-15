using System.Windows.Input;

namespace MathParserWPF.ViewModel
{
    public class PhysicalKeyboardHandler
    {
        // Главный ViewModel
        private readonly Controller _controller;
        // Состояние кнопки Shift (нажата или нет)
        private bool _isShift;

        // Конструктор
        public PhysicalKeyboardHandler(Controller controller)
        {
            _controller = controller;
        }

        // Обработчики нажатия клавиш
        public void HandleKeyDown(KeyEventArgs e)
        {
            if (e.Key == Key.LeftShift || e.Key == Key.RightShift) { _isShift = true; return; }

            //MessageBox.Show(e.Key.ToString());
            string param = "";
            switch (e.Key)
            {
                case Key.NumPad0:
                    param = "0"; break;
                case Key.NumPad1:
                    param = "1"; break;
                case Key.NumPad2:
                    param = "2"; break;
                case Key.NumPad3:
                    param = "3"; break;
                case Key.NumPad4:
                    param = "4"; break;
                case Key.NumPad5:
                    param = "5"; break;
                case Key.NumPad6:
                    param = "6"; break;
                case Key.NumPad7:
                    param = "7"; break;
                case Key.NumPad8:
                    param = "8"; break;
                case Key.NumPad9:
                    param = "9"; break;
                case Key.D0:
                    if (_isShift) param = ")";
                    else param = "0"; break;
                case Key.D1:
                    param = "1"; break;
                case Key.D2:
                    param = "2"; break;
                case Key.D3:
                    param = "3"; break;
                case Key.D4:
                    param = "4"; break;
                case Key.D5:
                    param = "5"; break;
                case Key.D6:
                    param = "6"; break;
                case Key.D7:
                    param = "7"; break;
                case Key.D8:
                    if (_isShift) param = "×";
                    else param = "8"; break;
                case Key.D9:
                    if (_isShift) param = "(";
                    else param = "9"; break;
                case Key.Add:
                    param = "+"; break;
                case Key.Subtract:
                    param = "-"; break;
                case Key.Divide:
                    param = "÷"; break;
                case Key.Multiply:
                    param = "×"; break;
                case Key.OemPeriod:
                    param = "."; break;
                case Key.Decimal:
                    param = "."; break;
                case Key.OemQuestion:
                    param = "÷"; break;
                case Key.OemPlus:
                    if (_isShift)
                    {
                        if (_controller.CanExecuteCalculate(null))
                            _controller.Calculate(null); return;
                    }
                    else param = "+"; break;
                case Key.OemMinus:
                    param = "-"; break;
                case Key.Return:
                    if (_controller.CanExecuteCalculate(null))
                        _controller.Calculate(null); return;
                case Key.Back:
                    {
                        if (_controller.VirtualKeyboardHandler.CanExecuteDeleteCharacter(null))
                            _controller.VirtualKeyboardHandler.DeleteCharacter(null); return;
                    }

            }
            _controller.VirtualKeyboardHandler.AddCharacter(param);
        }
        public void HandleKeyUp(KeyEventArgs e)
        {
            if (e.Key == Key.LeftShift || e.Key == Key.RightShift) _isShift = false;
        }
    }
}
