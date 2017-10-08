using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using OpenCVApp.Utils;
using OpenCVApp.ViewModels;

namespace OpenCVApp.Commands
{
    internal class SearchCommandHandler : CommandHandlerBase
    {
        private List<MatchAndFeatureResult> _foundImageFiles;
        private static bool _canExecute = true;

        public SearchCommandHandler(MainViewModel mainViewModel, bool canExecute) : base(mainViewModel, canExecute)
        {

        }

        public override void Execute(object parameter)
        {
            if (!_canExecute) return;

            DisableExecution();
            AppendMessageToView("Search command executed");
            DoSearchAsync();
        }

        private async Task DoSearchAsync()
        {
            var searchedFile = GetSelectedImageFile();
            var imageMatcher = new ImageMatcher(GetFoundImageFiles(), searchedFile, MainViewModel);
            UpdateButtonContentWhenSearchingAsync();
            _foundImageFiles = await imageMatcher.BeginSearchAsync();

            EnableExecution();
            if (!_foundImageFiles.Any())
                return;

            foreach (var result in _foundImageFiles)
                AppendMessageToView($"Found match: {searchedFile.Name} matches with {result.Name} ");

            MainViewModel.FoundImageFiles = _foundImageFiles;
        }

        public static void DisableExecution()
        {
            _canExecute = false;
        }

        public static void EnableExecution()
        {
            _canExecute = true;
        }
        public async Task UpdateButtonContentWhenSearchingAsync()
        {
            await Task.Run(() =>
            {
                while (!_canExecute)
                {
                    MainViewModel.SearchButtonContent = "Searching...";
                    Thread.Sleep(500);
                    MainViewModel.SearchButtonContent = "Searching..";
                    Thread.Sleep(500);
                    MainViewModel.SearchButtonContent = "Searching.";
                    Thread.Sleep(500);
                }
                MainViewModel.SearchButtonContent = "Search";
            });
        }
    }
}