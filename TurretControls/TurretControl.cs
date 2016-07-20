using Robocode;
using TYLER.Tanks;

namespace TYLER.TurretControls
{
    internal abstract class TurretControl
    {
        public abstract void Fire(TyTank tyTank, ScannedRobotEvent evnt);

        public abstract void Turn(TyTank tyTank);
    }
}
