using System.Windows.Input;

namespace OpenCVApp
{
    internal class CancelCommandHandler : CommandHandlerBase
    {
        public CancelCommandHandler(MainViewModel mainViewModel, bool canExecute) : base(mainViewModel, canExecute)
        {
        }

        public override void Execute(object parameter)
        {
            appendMessageToView("CancelCommandHandler executed");
        }
    }
}