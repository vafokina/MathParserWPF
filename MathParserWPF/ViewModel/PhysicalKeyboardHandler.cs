using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using MathParserWPF.View;

namespace MathParserWPF.ViewModel
{
    public class PhysicalKeyboardHandler
    {
        private MainWindow _mainWindow;
        private bool isShift = false;

        // Конструктор
        public PhysicalKeyboardHandler()
        {
            _mainWindow = (MainWindow)Application.Current.MainWindow;
        }

        public void HandleKeyDown(KeyEventArgs e)
        {
            if (e.Key == Key.LeftShift || e.Key == Key.RightShift) { isShift = true; return; }

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
                    if (isShift) param = ")";
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
                    if (isShift) param = "×";
                    else param = "8"; break;
                case Key.D9:
                    if (isShift) param = "(";
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
                    if (isShift)
                    { if (_mainWindow.Controller.CanExecuteCalculate(null))
                        _mainWindow.Controller.Calculate(null); return; }
                    else param = "+"; break;
                case Key.OemMinus:
                    param = "-"; break;
                case Key.Return:
                    if (_mainWindow.Controller.CanExecuteCalculate(null))
                        _mainWindow.Controller.Calculate(null); return;
                case Key.Back:
                { _mainWindow.Controller.VirtualKeyboardHandler.DeleteCharacter(null); return; }

            }
            _mainWindow.Controller.VirtualKeyboardHandler.AddCharacter(param);
        }

        public void HandleKeyUp(KeyEventArgs e)
        {
            if (e.Key == Key.LeftShift || e.Key == Key.RightShift) isShift = false;
        }
    }
}
