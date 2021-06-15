using System;

namespace MathParserWPF.Model.MathOperations
{
    class SubtractionCommand : IMathCommand
    {

        //public bool CanExecute(double a, double b)
        //{
        //    return true;
        //}

        public double Execute(double a, double b)
        {
            try
            {
                double res = a - b;
                return res;
            }
            catch (OverflowException)
            {
                throw new ComputingException("Результат выражения выходит за пределы области допустимых значений. " +
                                             "Программа не может обработать такое большое или маленькое число.");
            }

        }
    }
}
