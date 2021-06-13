using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathParserWPF.Model.MathOperations;

namespace MathParserWPF.Model
{
    class MathInterpreter
    {
        // "культуронезависимый" формат для чисел (с разделителем ".")
        public static readonly NumberFormatInfo NFI =
        new NumberFormatInfo();
        // корневой узел AST-дерева программы
        private AstNode programNode = null;
        private IMathCommand addCommand;
        private IMathCommand subCommand;
        private IMathCommand mulCommand;
        private IMathCommand divCommand;

        // конструктор
        public MathInterpreter(AstNode programNode)
        {
            this.programNode = programNode;
            addCommand = new AdditionCommand();
            subCommand = new SubtractionCommand();
            mulCommand = new MultiplicationCommand();
            divCommand = new DivisionCommand();
        }
        // рекурсивный метод, который вызывается для каждого узла дерева
        private double ExecuteNode(AstNode node)
        {
            if (node.NodeType == AstNode.Type.Unknown)
                throw new InterpreterException("Неопределенный тип узла AST-дерева");
            if (node.NodeType == AstNode.Type.Number)
            {
                bool ok = double.TryParse(node.Text, NumberStyles.Any, NFI, out double res);
                if (!ok) throw new ComputingException("Программа не может обработать такое большое или маленькое число.");
                return res;
            }
            double a = ExecuteNode(node.GetChild(0));
            double b = ExecuteNode(node.GetChild(1));
            switch (node.NodeType)
            {
                case AstNode.Type.Add:
                    return addCommand.Execute(a, b);
                case AstNode.Type.Sub:
                    return subCommand.Execute(a, b);
                case AstNode.Type.Mul:
                    return mulCommand.Execute(a, b);
                case AstNode.Type.Div:
                    return divCommand.Execute(a, b);
                default:
                    throw new InterpreterException(
                        "Неизвестный тип узла AST-дерева");
            }
        }
        // public-метод для вызова интерпретации
        public double Execute()
        {
            return ExecuteNode(programNode);
        }
        // статическая реализации предыдузего метода (для удобства)
        public static double Execute(AstNode programNode)
        {
            MathInterpreter mei = new MathInterpreter(programNode);
            return mei.Execute();
        }
    }
}
