using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Emgu.CV;
using Emgu.CV.CvEnum;
using OpenCVApp.FileObjects;
using OpenCVApp.Properties;
using OpenCVApp.ViewModels;

namespace OpenCVApp.Utils
{
    internal class ImageMatcher
    {
        private readonly List<ImageFile> _listOfSearchableImageFiles;
        private readonly ImageFile _imageFileToBeSearched;
        private readonly MainViewModel _mainViewModel;
        public ImageMatcher(List<ImageFile> listOfImageFiles, ImageFile imageFileToBeSearched, MainViewModel mainViewModel)
        {
            _listOfSearchableImageFiles = listOfImageFiles;
            _imageFileToBeSearched = imageFileToBeSearched;
            _mainViewModel = mainViewModel;
        }

        public async Task<List<MatchAndFeatureResult>> BeginSearchAsync()
        {
            var imageFileSearchResults = new List<MatchAndFeatureResult>();
            if (_imageFileToBeSearched == null || _listOfSearchableImageFiles == null) return imageFileSearchResults;
            var searchedImage = ImageConverter.ResizeImageIfTooBig(CvInvoke.Imread(_imageFileToBeSearched.FullName, ImreadModes.AnyColor));
            var counter = 0;
            var stopwatch = new Stopwatch();
            
            await Task.Run( () =>
            {
                stopwatch.Start();
                foreach (var imageFile in _listOfSearchableImageFiles)
                {
                    try
                    {
                        using (var searchableImageFile = ImageConverter.ResizeImageIfTooBig(CvInvoke.Imread(imageFile.FullName, ImreadModes.AnyColor)))
                        {
                            var result = DrawMatchesAndFeatures.DrawMatchesAndKeyPoints(searchableImageFile, searchedImage).First();
                            imageFileSearchResults.Add(new MatchAndFeatureResult(result.Key, result.Value, imageFile.Name, imageFile.FullName ));
                        }
                    }
                    catch (Exception)
                    {
                        Console.WriteLine(Resources.ImageMatcher_BeginSearchAsync_Error, imageFile.Name);
                    }
                    counter++;
                    _mainViewModel.Message = $"{counter}. of {_listOfSearchableImageFiles.Count} images searched.";
                }
                _mainViewModel.Message = $"It took {stopwatch.Elapsed.TotalMinutes} minutes.";
            });
            
            return imageFileSearchResults;
        }
    }
}
