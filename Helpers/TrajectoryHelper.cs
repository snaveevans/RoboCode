using Robocode;
using System;
using Robocode.Util;

namespace TYLER
{
    class TrajectoryHelper
    {
        public double IntersectDistance { get; set; }
        public double IntersectHeading { get; set; }
        private bool _isTravelingRight;

        public TrajectoryHelper(double bearing, double heading, double targetHeading, double velocity, double distance, double bulletPower = 0)
        {
            var alpha = GetAlpha(bearing, heading, targetHeading);
            var s3 = Rules.MAX_VELOCITY;

            if (bulletPower > 0)
            {
                s3 = 20 - 3*bulletPower;
            }

            var theta = GetTheta(velocity, s3, alpha);
            var gamma = GetGamma(theta, alpha);

            IntersectDistance = GetIntersectDistance(distance, gamma, alpha);

            if (_isTravelingRight)
                theta *= -1;

            IntersectHeading = Utils.NormalAbsoluteAngleDegrees(bearing + heading + theta);
        }

        #region  Calculations
        private static double GetIntersectDistance(double distance, double gamma, double alpha)
        {
            var gammaRad = Utils.ToRadians(gamma);
            var alphaRad = Utils.ToRadians(alpha);
            var denominator = Math.Sqrt(Math.Sin(gammaRad) * Math.Sin(alphaRad));

            if (denominator == 0)
                return 0;

            var result = distance / denominator;

            return result;
        }

        private static double GetGamma(double theta, double alpha)
        {
            return 180 - theta - alpha;
        }

        private static double GetTheta(double s2, double s3, double alpha)
        {
            if (s3 == 0D)
                return 0;
            var alphaRad = Utils.ToRadians(alpha);
            var result = s2 * Math.Sin(alphaRad) / s3;
            if (result == 0D)
                return 0;
            result = Math.Asin(result);
            result = Utils.ToDegrees(result);

            return result;
        }

        private double GetAlpha(double bearingToTank, double myHeading, double enemyHeading)
        {
            var headingToTarget = myHeading + bearingToTank;
            var sub1 = enemyHeading - headingToTarget;
            var sub2 = Utils.NormalAbsoluteAngleDegrees(sub1);

            var sub3 = 180 - sub2;
            _isTravelingRight = sub3 < 0;
            var alpha = Math.Abs(sub3);

            return alpha;
        }

        #endregion
    }
}
