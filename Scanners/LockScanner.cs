
using System;
using Robocode.Util;
using TYLER.Tanks;

namespace TYLER.Scanners
{
    internal class LockScanner : Scanner
    {
        private bool _isLeftsTurn;

        public LockScanner()
        {
            _isLeftsTurn = true;
        }

        public override void Scan(TyTank tyTank)
        {
            var headingToTarget  = Utils.NormalAbsoluteAngleDegrees(tyTank.Heading + tyTank.TargetTank.Bearing);
            
            var radarTurn = headingToTarget - tyTank.RadarHeading;

            radarTurn = Utils.NormalRelativeAngleDegrees(radarTurn);

            // roughly zero it
            if (Math.Abs(radarTurn) > 40)
            {
                tyTank.SetTurnRadarRight(radarTurn);
                return;
            }

            var turnHeight = 20;

            if (_isLeftsTurn)
            {
                turnHeight *= -1;
            }

            var radarLimit = Utils.NormalAbsoluteAngleDegrees(headingToTarget + turnHeight);
            radarTurn = Utils.NormalRelativeAngleDegrees(radarLimit - tyTank.RadarHeading);

            _isLeftsTurn = !_isLeftsTurn;

            tyTank.SetTurnRadarRight(radarTurn);
        }
    }
}
