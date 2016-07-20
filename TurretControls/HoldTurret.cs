
using System;
using Robocode;
using TYLER.Tanks;

namespace TYLER.TurretControls
{
    internal class HoldTurret : TurretControl
    {
        public override void Fire(TyTank tyTank, ScannedRobotEvent evnt)
        {
            // don't fire
        }

        public override void Turn(TyTank tyTank)
        {
            // move the gun to straight ahead
            var diff = tyTank.Heading - tyTank.GunHeading;

            var absDiff = Math.Abs(diff);

            if(absDiff <= .001)
                return;

            if (diff > 180)
                diff -= 180;
            else if (diff < -180)
                diff += 180;

            tyTank.SetTurnGunRight(diff);
        }
    }
}
