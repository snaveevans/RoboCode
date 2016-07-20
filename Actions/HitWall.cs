
using System;
using Robocode;
using TYLER.TurretControls;
using TYLER.Scanners;
using TYLER.Tanks;

namespace TYLER.Actions
{
    internal class HitWall : Action
    {
        private bool _isInitialized;
        private double _travel;
        private double _turn;

        private static readonly double Velocity = Rules.MAX_VELOCITY * .75;
        private static readonly double TurnRate = 10 - 0.75 * Math.Abs(Velocity);

        public override ActionType ActionsType { get { return ActionType.Reacter; } }

        public HitWall(Action previousAction) : base(previousAction, new FullScanner(), new HoldTurret())
        {
            _isInitialized = false;
            _travel = 99999999;
            _turn = 99999999;
        }

        public override void Execute(TyTank me)
        {
            DecrementValues();

            if (!_isInitialized)
            {
                _isInitialized = true;
                _travel = Velocity * -6;
                _turn = TurnRate * 6;
            }


            me.SetAhead(_travel);
            me.SetTurnRight(_turn);
        }

        public override bool IsSatisfied(TyTank tyTank)
        {
            return Math.Abs(_travel) <= Rules.MAX_TURN_RATE;
        }

        public override Action NextAction()
        {
            return PreviousAction;
        }

        private void DecrementValues()
        {
            var maxVelocity = Velocity;
            if (_travel < 0)
            {
                maxVelocity *= -1;
            }
            var maxTurn = TurnRate;
            if (_travel < 0)
            {
                maxTurn *= -1;
            }

            _turn -= maxTurn;
            _travel = maxVelocity;
        }
    }
}
