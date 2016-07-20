using Robocode;
using TYLER.Tanks;

namespace TYLER.TurretControls
{
    internal class ScanHoldTurret : TurretControl
    {
        public override void Fire(TyTank tyTank, ScannedRobotEvent evnt)
        {
            // don't fire
        }

        public override void Turn(TyTank tyTank)
        {
            tyTank.SetTurnGunLeft(20);
        }
    }
}
