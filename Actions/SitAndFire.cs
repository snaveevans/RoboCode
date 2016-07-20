using TYLER.Scanners;
using TYLER.Tanks;
using TYLER.TurretControls;

namespace TYLER.Actions
{
    internal class SitAndFire : Action
    {
        public override ActionType ActionsType { get { return ActionType.Scanner; } }

        public SitAndFire(Action previousAction) : base(previousAction, new FullScanner(), new ScanHoldTurret())
        { }

        public override bool IsSatisfied(TyTank tyTank)
        {
            return false;
        }

        public override void Execute(TyTank tyTank)
        {
            if (tyTank.TargetTank.Name == "") return;

            if(!(Scanner is LockScanner))
                Scanner = new LockScanner();
            if (!(TurretControl is TrackTurret))
                TurretControl = new TrackTurret();
        }

        public override Action NextAction()
        {
            return this;
        }
    }
}
