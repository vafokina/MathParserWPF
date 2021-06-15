namespace MathParserWPF.Model.MathOperations
{
    interface IMathCommand
    {
       // bool CanExecute(double a, double b);
        double Execute(double a, double b);
    }
}
