using Emgu.CV;
using Emgu.CV.Util;

namespace OpenCVApp.Utils
{
    public class FindMatchResult
    {
        public VectorOfKeyPoint ModelKeyPoints { get; }
        public VectorOfKeyPoint ObservedKeyPoints { get; }
        public Mat Mask { get; }
        public long MatchAmount { get; }

        public FindMatchResult(VectorOfKeyPoint modelKeyPoints, VectorOfKeyPoint observedKeyPoints, Mat mask, long matchAmount)
        {
            ModelKeyPoints = modelKeyPoints;
            ObservedKeyPoints = observedKeyPoints;
            Mask = mask;
            MatchAmount = matchAmount;
        }
    }
}
