using Robocode.Util;
using TYLER.Scanners;
using TYLER.Tanks;
using TYLER.TurretControls;

namespace TYLER.Actions
{
    internal class DestroyTarget : Action
    {
        private bool _tooFar;
        private bool _targetDestroyed;
        public override ActionType ActionsType { get { return ActionType.Targeter; } }

        public DestroyTarget(Action previousAction)
            : base(previousAction, new LockScanner(), new TrackTurret())
        { }

        public override void Execute(TyTank tyTank)
        {
            var trajectoryHelper = new TrajectoryHelper(tyTank.TargetTank.Bearing, tyTank.Heading, tyTank.TargetTank.Heading,
                tyTank.TargetTank.Velocity, tyTank.TargetTank.Distance);

            var turn = Utils.NormalRelativeAngleDegrees(trajectoryHelper.IntersectHeading - tyTank.Heading);

            var travel = trajectoryHelper.IntersectDistance;
            // don't get too close
            travel = travel > 50 ? travel - 50 : 0;

            tyTank.SetAhead(travel);
            tyTank.SetTurnRight(turn);
        }

        public override bool IsSatisfied(TyTank tyTank)
        {
            _tooFar = tyTank.TargetTank.Distance > 150;
            _targetDestroyed = tyTank.DestroyedTanks.Contains(tyTank.TargetTank.Name);
            if (_targetDestroyed)
                tyTank.TargetTank = new TankHelper();

            return _tooFar || _targetDestroyed;
        }

        public override Action NextAction()
        {
            if (_tooFar)
                return new CloseOnTarget(this);

            return new SearchForTarget(this);
        }
    }
}
