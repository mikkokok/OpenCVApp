using System;
using System.Windows.Input;

namespace OpenCVApp
{
    internal class SearchCommandHandler : CommandHandlerBase
    {
       
        public SearchCommandHandler(MainViewModel mainViewModel, bool canExecute) : base(mainViewModel,canExecute)
        {
           
        }

        public override void Execute(object parameter)
        {
            appendMessageToView("Search command executed");
        }
    }
}