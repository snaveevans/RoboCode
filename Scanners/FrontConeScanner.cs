
using System;
using TYLER.Tanks;

namespace TYLER.Scanners
{
    internal class FrontConeScanner : Scanner
    {
        private bool _isInitialzed;
        private int _alternator;
        private bool _isZeroed;

        public FrontConeScanner()
        {
            _isInitialzed = false;
            _alternator = 1;
            _isZeroed = false;
        }

        public override void Scan(TyTank tyTank)
        {
            var diff = tyTank.Heading - tyTank.RadarHeading;

            if (diff > 90)
            {
                _isZeroed = false;
            }

            if (!_isZeroed && Math.Abs(diff) > 5)
            {
                tyTank.SetTurnRadarRight(diff);
                return;
            }

            _isZeroed = true;

            if (!_isInitialzed)
            {
                tyTank.SetTurnRadarRight(45);
                _isInitialzed = true;
                return;
            }

            var radarTurn = -45;

            if (_alternator > 2)
            {
                radarTurn *= -1;
            }

            _alternator++;
            if (_alternator >= 4)
                _alternator = 1;

            tyTank.SetTurnRadarRight(radarTurn);
        }
    }
}
