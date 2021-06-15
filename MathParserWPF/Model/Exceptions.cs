using System;

namespace MathParserWPF.Model
{
    public class ParserBaseException : Exception
    {
        public ParserBaseException(string message) : base(message) { }
    }
    public class InterpreterException : Exception
    {
        public InterpreterException(string message) : base(message) { }
    }
    public class ComputingException : Exception
    {
        public ComputingException(string message) : base(message) { }
    }
}
