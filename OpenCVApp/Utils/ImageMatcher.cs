using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Features2D;
using Emgu.CV.Structure;
using Emgu.CV.Util;
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
            var matchFinder = new MatchFinder();

            await Task.Run(() =>
            {
                stopwatch.Start();
                var searchedImageDescriptorsDict = matchFinder.DetectAndComputeDescriptors(searchedImage).First();
                foreach (var imageFile in _listOfSearchableImageFiles)
                {
                    try
                    {
                        using (var searchableImageFile = ImageConverter.ResizeImageIfTooBig(CvInvoke.Imread(imageFile.FullName, ImreadModes.AnyColor)))
                        {
                            using (var matches = new VectorOfVectorOfDMatch())
                            {
                                var searchableImageDescriptorsDict = matchFinder.DetectAndComputeDescriptors(searchableImageFile).First();
                                var matchCalculationResultDict = matchFinder.CalculateMatches(searchedImageDescriptorsDict.Key, searchableImageDescriptorsDict.Key, matches).First();
                                var matchAmount = matchFinder.MatchAmountCalculator(matches, matchCalculationResultDict.Key);
                                var drawnMatches = DrawMatches(searchableImageFile, searchedImage, searchableImageDescriptorsDict.Value, searchedImageDescriptorsDict.Value, matches, matchCalculationResultDict.Key);
                                imageFileSearchResults.Add(new MatchAndFeatureResult(drawnMatches, matchAmount, imageFile.Name, imageFile.FullName));
                            }
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
        private static Mat DrawMatches(IInputArray searchableImageFile, IInputArray searchedImage, VectorOfKeyPoint searchableImageKeyPoint, VectorOfKeyPoint searchedImageKeyPoint, VectorOfVectorOfDMatch matches, IInputArray mask)
        {
            var result = new Mat();
            Features2DToolbox.DrawMatches(searchableImageFile, searchedImageKeyPoint, searchedImage, searchableImageKeyPoint, matches, result, new MCvScalar(255, 255, 255), new MCvScalar(255, 255, 255), mask);

            return result;

        }
    }
}
