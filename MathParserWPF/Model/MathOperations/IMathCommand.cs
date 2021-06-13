using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathParserWPF.Model.MathOperations
{
    interface IMathCommand
    {
       // bool CanExecute(double a, double b);
        double Execute(double a, double b);
    }
}
