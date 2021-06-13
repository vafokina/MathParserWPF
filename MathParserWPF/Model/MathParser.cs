using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathParserWPF.Model
{
    public class MathParser : ParserBase
    {
        // конструктор
        public MathParser(string source)
        : base(source)
        {
        }
        // далее идет реализация в виде функций правил грамматики
        // NUMBER -> <число>
        public AstNode NUMBER()
        {
            string number = "";
            while (Current == '.' || Current == ',' || char.IsDigit(Current))
            {
                number += Current;
                Next();
            }
            if (number.Length == 0)
                throw new ParserBaseException(
                string.Format("Ожидалось число (pos={0})", Pos));
            Skip();
            return new AstNode(AstNode.Type.Number, number);
        }
        // group -> "(" term ")" | NUMBER
        public AstNode Group()
        {
            if (IsMatch("("))
            { // выбираем альтернативу
                Match("("); // это выражение в скобках
                AstNode result = Term();
                Match(")");
                return result;
            }
            else
                return NUMBER(); // число
        }
        // mult -> group ( ( "*" | "/" ) group )*
        public AstNode Mult()
        {
            AstNode result = Group();
            while (IsMatch("*", "/"))
            { // повторяем нужное кол-во раз
                string oper = Match("*", "/"); // здесь выбор альтернативы
                AstNode temp = Group(); // реализован иначе
                result =
                oper == "*" ? new AstNode(AstNode.Type.Mul, result, temp)
                : new AstNode(AstNode.Type.Div, result, temp);
            }
            return result;
        }
        // add -> mult ( ( "+" | "-" ) mult )*
        public AstNode Add()
        { // реализация аналогично правилу mult
            AstNode result = Mult();
            while (IsMatch("+", "-"))
            {
                string oper = Match("+", "-");
                AstNode temp = Mult();
                result =
                oper == "+" ? new AstNode(AstNode.Type.Add, result, temp)
                : new AstNode(AstNode.Type.Sub, result, temp);
            }
            return result;
        }
        // term -> add
        public AstNode Term()
        {
            return Add();
        }
        // result -> term
        public AstNode Result()
        {
            return Term();
        }
        // метод, вызывающий начальное и правило грамматики и
        // соответствующий парсинг
        public AstNode Parse()
        {
            Skip();
            AstNode result = Result();
            if (End)
                return result;
            else
                throw new ParserBaseException( // разобрали не всю строку
                string.Format("Лишний символ '{0}' (pos={1})",
                Current, Pos)
                );
        }
        // статическая реализации предыдузего метода (для удобства)
        public static AstNode Parse(string source)
        {
            MathParser mlp = new MathParser(source);
            return mlp.Parse();
        }
    }
}
