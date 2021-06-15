using System;

namespace MathParserWPF.Model.MathOperations
{
    class MultiplicationCommand : IMathCommand
    {

        //public bool CanExecute(double a, double b)
        //{
        //    return true;
        //}

        public decimal Execute(decimal a, decimal b)
        {
            try
            {
                decimal res = a * b;
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
