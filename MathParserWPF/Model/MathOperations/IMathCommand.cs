namespace MathParserWPF.Model.MathOperations
{
    interface IMathCommand
    {
        // bool CanExecute(double a, double b);
        decimal Execute(decimal a, decimal b);
    }
}
