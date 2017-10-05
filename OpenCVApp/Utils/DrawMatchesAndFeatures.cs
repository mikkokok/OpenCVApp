using System.Collections.Generic;
using Emgu.CV;
using Emgu.CV.Features2D;
using Emgu.CV.Structure;
using Emgu.CV.Util;

namespace OpenCVApp.Utils
{
    public static class DrawMatchesAndFeatures
    {
        public static Dictionary<Mat, long> DrawMatchesAndKeyPoints(Mat modelImage, Mat observedImage)
        {
            using (var matches = new VectorOfVectorOfDMatch())
            {
                var findMatchResult = MatchFinder.FindMatch(modelImage, observedImage, matches);

                var result = new Mat();
                Features2DToolbox.DrawMatches(modelImage, findMatchResult.ModelKeyPoints, observedImage, findMatchResult.ObservedKeyPoints, matches, result, new MCvScalar(255, 255, 255), new MCvScalar(255, 255, 255), findMatchResult.Mask);

                return new Dictionary<Mat, long>
                {
                    { result, findMatchResult.MatchAmount }
                };
            }
        }
    }
}
