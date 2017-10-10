using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Features2D;
using Emgu.CV.Flann;
using Emgu.CV.Structure;
using Emgu.CV.Util;

namespace OpenCVApp.Utils
{
    internal class MatchFinder
    {
        private const int KNearestMatch = 2;
        private const double UniquenessThreshold = 0.80;
        private Mat _mask;
        public Dictionary<Mat, VectorOfKeyPoint> DetectAndComputeDescriptors(Mat image)
        {
            var detectedAndComputedImage = new Mat();
            var imageKeyPoints = new VectorOfKeyPoint();
            using (var uImage = image.GetUMat(AccessType.Read))
            {
                using (var featureDetector = new KAZE())
                {
                    featureDetector.DetectAndCompute(uImage, null, imageKeyPoints, detectedAndComputedImage, false);
                }

            }
            return new Dictionary<Mat, VectorOfKeyPoint>
            {
                { detectedAndComputedImage, imageKeyPoints  }
            };
        }

        public Dictionary<Mat, VectorOfVectorOfDMatch> CalculateMatches(Mat searchedImageDescriptors, Mat searchableImageDescriptors, VectorOfVectorOfDMatch matches)
        {
            // KdTree for faster results / less accuracy
            using (var ip = new KdTreeIndexParams())
            using (var sp = new SearchParams())
            using (DescriptorMatcher matcher = new FlannBasedMatcher(ip, sp))
            {
                matcher.Add(searchedImageDescriptors);
                // KnnMatch" is looking for KNearestMatch-Nearest Neighbor for each point of image A in image B.
                matcher.KnnMatch(searchableImageDescriptors, matches, KNearestMatch, null);
                _mask = new Mat(matches.Size, 1, DepthType.Cv8U, 1);
                _mask.SetTo(new MCvScalar(255));
                // In image B, if the 1st and 2nd neighbors are too similar (the distance ratio is less than 0.8), we consider that we don't know which one is the best matching, so the matching is filtered (deleted).
                Features2DToolbox.VoteForUniqueness(matches, UniquenessThreshold, _mask);
            }
            return new Dictionary<Mat, VectorOfVectorOfDMatch>
            {
                { _mask, matches }
            };
        }


        public long MatchAmountCalculator(VectorOfVectorOfDMatch matches, Mat mask)
        {
            long matchAmount = 0;
            for (var i = 0; i < matches.Size; i++)
            {
                if (mask.GetData(i)[0] == 0) continue;
                matchAmount += matches[i].ToArray().LongCount();
            }
            return matchAmount;
        }
    }
}
