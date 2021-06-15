using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace MathParserWPF.ViewModel
{
    public class InputChecker
    {
        // проверка на неразрешенные символы
        public static bool PreCheckCharacters(string str)
        {
            Regex regex = new Regex(@"^[()., \-+*×\/÷\d]*$");
            return regex.IsMatch(str);
        }

        // алгоритм проверки расстановки скобок со стеком
        public static bool CheckGroups(string str)
        {
            Regex regex = new Regex(@"[()[\]]+");
            if (!regex.IsMatch(str)) return true;

            Stack<char> stack = new Stack<char>();

            for (int i = 0; i < str.Length; i++)
            {
                // if ()
                if (str[i] == '(' || str[i] == '[')
                {
                    stack.Push(str[i]);
                }
                else
                {
                    if (str[i] != ')' && str[i] != ']') continue;
                    if (stack.Count != 0)
                    {
                        char t = stack.Pop();
                        if ((str[i] == ')' && t == '(') || (str[i] == ']' && t == '['))
                        { }
                        else return false;
                    }
                    else return false;
                }
            }
            if (stack.Count != 0) return false;
            return true;
        }

        // корректировка ввода : неверный ввод игнорируется
        public static string CorrectString(string str)
        {
            if (str.Length == 0) return str;

            char[] onlyOperands = new[] { '+', '÷', '×' };
            char[] operands = new[] { '+', '-', '÷', '×' };
            char[] digits = new[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            char lastChar = str[str.Length - 1];

            if (str.Length == 1)
            {
                if (onlyOperands.Contains(lastChar) || lastChar == '.') return "";
                return str;
            }

            char prevChar = str[str.Length - 2];

            if (digits.Contains(lastChar) && prevChar == ')') return str.Substring(0, str.Length - 1);
            if (lastChar == '-')
            {
                if (prevChar == '.') return str.Substring(0, str.Length - 1);
                if (onlyOperands.Contains(prevChar))
                {
                    if (str.Length == 2) str = "";
                    else str = str.Substring(0, str.Length - 2);
                    str += lastChar;
                    return str;
                }
                return str;
            }
            if (onlyOperands.Contains(lastChar))
            {
                if (prevChar == '.') return str.Substring(0, str.Length - 1);
                if (prevChar == '(') return str.Substring(0, str.Length - 1);
                if (operands.Contains(prevChar))
                {
                    if (str.Length == 2) str = "";
                    else str = str.Substring(0, str.Length - 2);
                    str += lastChar;

                    if (str.Length == 1) return "";

                    prevChar = str[str.Length - 2];
                    if (prevChar == '(') return str.Substring(0, str.Length - 1);
                    return str;
                }
                return str;
            }
            if (lastChar == '(')
            {
                if (prevChar == ')' || prevChar == '.' || digits.Contains(prevChar)) return str.Substring(0, str.Length - 1);
                return str;
            }
            if (lastChar == ')')
            {
                if (prevChar == '(' || prevChar == '.' || operands.Contains(prevChar)) return str.Substring(0, str.Length - 1);
                return str;
            }

            if (lastChar == '.')
            {
                if (prevChar == '.' || prevChar == '(' || prevChar == ')' || operands.Contains(prevChar))
                    return str.Substring(0, str.Length - 1);

                if (str.Length == 2) return str;

                int i = str.Length - 2;
                do
                {
                    prevChar = str[i--];
                } while (i != -1 && prevChar != '.' && !operands.Contains(prevChar));
                if (prevChar == '.') return str.Substring(0, str.Length - 1);
            }

            return str;
        }

        // форматирование вывода
        public static string FormatText(string str)
        {
            str = str.Replace("-", " - ");
            str = str.Replace("+", " + ");
            str = str.Replace("×", " × ");
            str = str.Replace("÷", " ÷ ");
            return str;
        }
    }
}
