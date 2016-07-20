using System;
using Robocode;
using TYLER.TurretControls;
using TYLER.Scanners;
using TYLER.Tanks;

namespace TYLER.Actions
{
    internal class TurnToTarget : Action
    {
        private bool _isInitialized;
        private double _travel;
        private double _turn;
        
        private static readonly double Velocity = Rules.MAX_VELOCITY/4;
        private static readonly double TurnRate = 10 - 0.75*Math.Abs(Velocity);

        public override ActionType ActionsType { get { return ActionType.Mover; } }

        public TurnToTarget(Action previousAction) : base(previousAction, new FullScanner(), new HoldTurret())
        {
            _isInitialized = false;
            _travel = 99999999;
            _turn = 99999999;
        }

        public override void Execute(TyTank me)
        {
            DecrementValues();

            if (_isInitialized) 
                return;

            _isInitialized = true;

            // figure out how many turns it'll take to turn
            var gameTurns = Math.Ceiling(Math.Abs(me.TargetTank.Bearing / TurnRate));
            _turn = me.TargetTank.Bearing;
            _travel = gameTurns * Velocity;

            // slow down speed so we can turn faster
            me.MaxVelocity = Velocity;
            me.SetAhead(_travel);
            me.SetTurnRight(_turn);
        }

        public override bool IsSatisfied(TyTank tyTank)
        {
            var test = Math.Abs(_turn) <= Rules.MAX_TURN_RATE;

            if (!test)
                return false;

            // max speed again
            tyTank.MaxVelocity = Rules.MAX_VELOCITY;

            return true;
        }

        public override Action NextAction()
        {
            return new CloseOnTarget(this);
        }

        private void DecrementValues()
        {
            var maxVelocity = Velocity;
            if (_travel < 0)
            {
                maxVelocity *= -1;
            }
            var maxTurn = TurnRate;
            if (_turn < 0)
            {
                maxTurn *= -1;
            }

            _turn -= maxTurn;
            _travel = maxVelocity;
        }
    }
}
