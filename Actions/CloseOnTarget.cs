using Robocode.Util;
using TYLER.Scanners;
using TYLER.TurretControls;

namespace TYLER.Actions
{
    internal class CloseOnTarget : Action
    {
        public override ActionType ActionsType { get {return ActionType.Mover;} }

        public CloseOnTarget(Action previousAction)
            : base(previousAction, new FrontConeScanner(), new HoldTurret())
        { }

        public override void Execute(Tanks.TyTank tyTank)
        {
            var trajectoryHelper = new TrajectoryHelper(tyTank.TargetTank.Bearing, tyTank.Heading, tyTank.TargetTank.Heading,
                tyTank.TargetTank.Velocity, tyTank.TargetTank.Distance);

            var turn = Utils.NormalRelativeAngleDegrees(trajectoryHelper.IntersectHeading - tyTank.Heading);

            tyTank.SetAhead(trajectoryHelper.IntersectDistance);
            tyTank.SetTurnRight(turn);
        }

        public override bool IsSatisfied(Tanks.TyTank tyTank)
        {
            return tyTank.TargetTank.Distance <= 150;
        }

        public override Action NextAction()
        {
            return new DestroyTarget(this);
        }
    }
}
