using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathParserWPF.Model.MathOperations
{
    class MultiplicationCommand : IMathCommand
    {

        //public bool CanExecute(double a, double b)
        //{
        //    return true;
        //}

        public double Execute(double a, double b)
        {
            try
            {
                double res = a * b;
                return res;
            }
            catch (OverflowException ex)
            {
                throw new ComputingException("Результат выражения выходит за пределы области допустимых значений. " +
                                             "Программа не может обработать такое большое или маленькое число.");
            }

        }
    }
}
