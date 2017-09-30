using System;
using System.Windows.Input;

namespace OpenCVApp.Commands
{
    internal class SearchCommandHandler : CommandHandlerBase
    {
       
        public SearchCommandHandler(MainViewModel mainViewModel, bool canExecute) : base(mainViewModel,canExecute)
        {
           
        }

        public override void Execute(object parameter)
        {
            AppendMessageToView("Search command executed");
        }
    }
}