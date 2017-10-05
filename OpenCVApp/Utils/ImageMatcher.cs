using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Emgu.CV;
using Emgu.CV.CvEnum;
using OpenCVApp.FileObjects;
using OpenCVApp.Properties;

namespace OpenCVApp.Utils
{
    internal class ImageMatcher
    {
        private readonly List<ImageFile> _listOfSearchableImageFiles;
        private readonly ImageFile _imageFileToBeSearched;
        public ImageMatcher(List<ImageFile> listOfImageFiles, ImageFile imageFileToBeSearched)
        {
            _listOfSearchableImageFiles = listOfImageFiles;
            _imageFileToBeSearched = imageFileToBeSearched;
        }

        public async Task<List<MatchAndFeatureResult>> BeginSearchAsync()
        {
            var imageFileSearchResults = new List<MatchAndFeatureResult>();
            var searchedImage = ImageConverter.ResizeImageIfTooBig(CvInvoke.Imread(_imageFileToBeSearched.FullName, ImreadModes.AnyColor));

            await Task.Run( () =>
            {
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
                }
            });
            return imageFileSearchResults;
        }
    }
}
