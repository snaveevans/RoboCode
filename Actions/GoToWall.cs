using System;
using Robocode;
using Robocode.Util;
using TYLER.Scanners;
using TYLER.Tanks;
using TYLER.TurretControls;

namespace TYLER.Actions
{
    internal class GoToWall : Action
    {
        internal Wall WallDestination { get; set; }
        public override ActionType ActionsType { get { return ActionType.Mover; } }

        public GoToWall(Action previousAction, Wall wallDestination) 
            : base(previousAction, new FullScanner(), new ScanHoldTurret())
        {
            WallDestination = wallDestination;
        }

        public GoToWall(Action previousAction) 
            : base(previousAction, new FullScanner(), new ScanHoldTurret())
        {
            WallDestination = Wall.Top;
        }

        public override bool IsSatisfied(TyTank tyTank)
        {
            return false;
        }

        public override void Execute(TyTank tyTank)
        {
            double turn = (int) WallDestination - tyTank.Heading;
            turn = Utils.NormalRelativeAngleDegrees(turn);
            tyTank.SetTurnRight(turn);
            if(!Utils.IsNear(turn, 0))
                return;

            tyTank.SetAhead(Rules.MAX_VELOCITY);
        }

        public override Action NextAction()
        {
            throw new NotImplementedException();
        }


        internal enum Wall
        {
            Top = 0,
            Right = 90,
            Bottom = 180,
            Left = 270
        }
    }
}
