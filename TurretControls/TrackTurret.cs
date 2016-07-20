using System;
using Robocode;
using Robocode.Util;
using TYLER.Tanks;

namespace TYLER.TurretControls
{
    internal class TrackTurret : TurretControl
    {
        private const double Power = 2;
        private bool _isOnTarget;

        public TrackTurret()
        {
            _isOnTarget = false;
        }

        public override void Turn(TyTank tyTank)
        {
            var trajectoryHelper = new TrajectoryHelper(tyTank.TargetTank.Bearing, tyTank.Heading, tyTank.TargetTank.Heading, 
                tyTank.TargetTank.Velocity, tyTank.TargetTank.Distance, Power);

            var headingToBeAt = trajectoryHelper.IntersectHeading;

            var turretTurn = headingToBeAt - tyTank.GunHeading;
            if (turretTurn < -180)
                turretTurn = 360 + turretTurn;
            else if (turretTurn > 180)
                turretTurn = -360 + turretTurn;

            _isOnTarget = Math.Abs(turretTurn) <= 8;

            tyTank.SetTurnGunRight(turretTurn);
        }

        public override void Fire(TyTank tyTank, ScannedRobotEvent evnt)
        {
            if(!_isOnTarget)
                return;

            tyTank.Fire(Power);
        }
    }
}
