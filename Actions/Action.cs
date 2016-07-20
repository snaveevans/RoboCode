
using TYLER.TurretControls;
using TYLER.Scanners;
using TYLER.Tanks;

namespace TYLER.Actions
{
    internal abstract class Action
    {
        public Action PreviousAction { get; set; }
        public Scanner Scanner { get; set; }
        public TurretControl TurretControl { get; set; }
        public abstract ActionType ActionsType { get; }

        protected Action(Action previousAction, Scanner scanner, TurretControl turretControl)
        {
            Scanner = scanner;
            TurretControl = turretControl;

            PreviousAction = previousAction;
            if (PreviousAction != null && PreviousAction.PreviousAction != null)
                PreviousAction.PreviousAction = null; // only hold onto 3 states
        }

        public abstract bool IsSatisfied(TyTank tyTank);

        public abstract void Execute(TyTank tyTank);

        public abstract Action NextAction();

        internal enum ActionType
        {
            Mover,
            Reacter,
            Targeter,
            Scanner
        }
    }
}
