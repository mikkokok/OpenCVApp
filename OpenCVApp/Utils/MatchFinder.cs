using System.Linq;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Features2D;
using Emgu.CV.Flann;
using Emgu.CV.Structure;
using Emgu.CV.Util;

namespace OpenCVApp.Utils
{
    public static class MatchFinder
    {
        private const int KNearestMatch = 2;
        private const double UniquenessThreshold = 0.80;
        private static VectorOfKeyPoint _modelKeyPoints;
        private static VectorOfKeyPoint _observedKeyPoints;
        private static Mat _mask;

        public static FindMatchResult FindMatch(Mat modelImage, Mat observedImage, VectorOfVectorOfDMatch matches)
        {
            _modelKeyPoints = new VectorOfKeyPoint();
            _observedKeyPoints = new VectorOfKeyPoint();

            using (var uModelImage = modelImage.GetUMat(AccessType.Read))
            using (var uObservedImage = observedImage.GetUMat(AccessType.Read))
            {
                var featureDetector = new KAZE();
                var modelDescriptors = new Mat();
                var observedDescriptors = new Mat();

                // Detects keypoints and computes the descriptors
                featureDetector.DetectAndCompute(uModelImage, null, _modelKeyPoints, modelDescriptors, false);
                featureDetector.DetectAndCompute(uObservedImage, null, _observedKeyPoints, observedDescriptors, false);

                // KdTree for faster results / less accuracy
                using (var ip = new KdTreeIndexParams())
                using (var sp = new SearchParams())
                using (DescriptorMatcher matcher = new FlannBasedMatcher(ip, sp))
                {
                    matcher.Add(modelDescriptors);
                    // KnnMatch" is looking for KNearestMatch-Nearest Neighbor for each point of image A in image B.
                    matcher.KnnMatch(observedDescriptors, matches, KNearestMatch, null);
                    _mask = new Mat(matches.Size, 1, DepthType.Cv8U, 1);
                    _mask.SetTo(new MCvScalar(255));
                    // In image B, if the 1st and 2nd neighbors are too similar (the distance ratio is less than 0.8), we consider that we don't know which one is the best matching, so the matching is filtered (deleted).
                    Features2DToolbox.VoteForUniqueness(matches, UniquenessThreshold, _mask);
                }
            }
            return new FindMatchResult(_modelKeyPoints, _observedKeyPoints, _mask, MatchAmountCalculator(matches, _mask));
        }

        private static long MatchAmountCalculator(VectorOfVectorOfDMatch matches, Mat mask)
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
