
using Robocode;
using TYLER.TurretControls;
using TYLER.Scanners;
using TYLER.Tanks;

namespace TYLER.Actions
{
    internal class SearchForTarget : Action
    {
        public override ActionType ActionsType { get { return ActionType.Scanner; } }

        public SearchForTarget(Action previousAction) : base(previousAction, new FullScanner(), new ScanHoldTurret())
        { }

        public override void Execute(TyTank tyTank)
        {
            tyTank.SetTurnLeft(Rules.MAX_TURN_RATE);
        }

        public override bool IsSatisfied(TyTank tyTank)
        {
            return tyTank.TargetTank.Name != "";
        }

        public override Action NextAction()
        {
            return new TurnToTarget(this);
        }
    }
}
