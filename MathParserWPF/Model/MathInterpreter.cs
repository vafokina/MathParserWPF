using System.Globalization;
using MathParserWPF.Model.MathOperations;

namespace MathParserWPF.Model
{
    class MathInterpreter
    {
        // "культуронезависимый" формат для чисел (с разделителем ".")
        public static readonly NumberFormatInfo NFI =
        new NumberFormatInfo();
        // корневой узел AST-дерева программы
        private readonly AstNode _programNode = null;
        // команды математических операций
        private readonly IMathCommand _addCommand;
        private readonly IMathCommand _subCommand;
        private readonly IMathCommand _mulCommand;
        private readonly IMathCommand _divCommand;

        // конструктор
        public MathInterpreter(AstNode programNode)
        {
            this._programNode = programNode;
            _addCommand = new AdditionCommand();
            _subCommand = new SubtractionCommand();
            _mulCommand = new MultiplicationCommand();
            _divCommand = new DivisionCommand();
        }

        // рекурсивный метод, который вызывается для каждого узла дерева
        private double ExecuteNode(AstNode node)
        {
            if (node.NodeType == AstNode.Type.Unknown)
                throw new InterpreterException("Неопределенный тип узла AST-дерева");
            if (node.NodeType == AstNode.Type.Number)
            {
                if (node.Text.Length > 12) throw new ComputingException("Программа не может обработать такое большое или маленькое число.");
                bool ok = double.TryParse(node.Text, NumberStyles.Any, NFI, out double res);
                if (!ok) throw new ComputingException("Программа не может обработать такое большое или маленькое число.");
                return res;
            }
            double a = ExecuteNode(node.GetChild(0));
            double b = ExecuteNode(node.GetChild(1));
            switch (node.NodeType)
            {
                case AstNode.Type.Add:
                    return _addCommand.Execute(a, b);
                case AstNode.Type.Sub:
                    return _subCommand.Execute(a, b);
                case AstNode.Type.Mul:
                    return _mulCommand.Execute(a, b);
                case AstNode.Type.Div:
                    return _divCommand.Execute(a, b);
                default:
                    throw new InterpreterException(
                        "Неизвестный тип узла AST-дерева");
            }
        }
        // public-метод для вызова интерпретации
        public double Execute()
        {
            return ExecuteNode(_programNode);
        }
        // статическая реализации предыдузего метода (для удобства)
        public static double Execute(AstNode programNode)
        {
            MathInterpreter mei = new MathInterpreter(programNode);
            return mei.Execute();
        }
    }
}
