using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathParserWPF.Model
{
    class MathInterpreter
    {
        // "культуронезависимый" формат для чисел (с разделителем ".")
        public static readonly NumberFormatInfo NFI =
        new NumberFormatInfo();
        // корневой узел AST-дерева программы
        private AstNode programNode = null;
        // конструктор
        public MathInterpreter(AstNode programNode)
        {
            this.programNode = programNode;
        }
        // рекурсивный метод, который вызывается для каждого узла дерева
        private double ExecuteNode(AstNode node)
        {
            switch (node.Type)
            {
                case AstNodeType.UNKNOWN:
                    throw new InterpreterException(
                    "Неопределенный тип узла AST-дерева");
                case AstNodeType.NUMBER:
                    return double.Parse(node.Text, NFI);
                case AstNodeType.ADD:
                    return ExecuteNode(node.GetChild(0)) +
                    ExecuteNode(node.GetChild(1));
                case AstNodeType.SUB:
                    return ExecuteNode(node.GetChild(0)) -
                    ExecuteNode(node.GetChild(1));
                case AstNodeType.MUL:
                    return ExecuteNode(node.GetChild(0)) *
                    ExecuteNode(node.GetChild(1));
                case AstNodeType.DIV:
                    return ExecuteNode(node.GetChild(0)) /
                    ExecuteNode(node.GetChild(1));
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
