using OpenCVApp.ViewModels;

namespace OpenCVApp.Commands
{
    internal class CancelCommandHandler : CommandHandlerBase
    {
        public CancelCommandHandler(MainViewModel mainViewModel, bool canExecute) : base(mainViewModel, canExecute)
        {
        }

        public override void Execute(object parameter)
        {
            AppendMessageToView("CancelCommandHandler executed");
            
        }
    }
}