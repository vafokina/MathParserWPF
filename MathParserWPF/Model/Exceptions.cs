using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
}
