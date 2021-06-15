using System.Windows.Input;

namespace MathParserWPF.ViewModel
{
    public interface IDelegateCommand : ICommand
    {
        void RaiseCanExecuteChanged();
    }

}
