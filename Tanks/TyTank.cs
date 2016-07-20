using Robocode;
using System;
using System.Collections.Generic;
using System.Reflection;
using Action = TYLER.Actions.Action;

namespace TYLER.Tanks
{
    public abstract class TyTank : AdvancedRobot
    {
        internal TankHelper TargetTank { get; set; }
        internal Action CurrentAction { get; set; }
        internal Action HitWallAction { get; set; }
        internal Dictionary<string, TankHelper> Tanks { get; set; }
        internal HashSet<string> DestroyedTanks { get; set; }
        internal long Turn { get; set; }

        internal bool IsTargetLost
        {
            get { return Turn - TargetTank.TurnLastSeen > 12; }
        }

        internal HitWall HitWallDelegate;
        internal delegate void HitWall(HitWallEvent evnt);

        protected TyTank()
        {
            HitWallAction = new Actions.HitWall(null);
            TargetTank = new TankHelper();
            Tanks = new Dictionary<string, TankHelper>();
            DestroyedTanks = new HashSet<string>();
        }

        public override void Run()
        {
            while (true)
            {
                Turn++;
                while (CurrentAction.IsSatisfied(this))
                {
                    CurrentAction = CurrentAction.NextAction();
                }

                CurrentAction.Scanner.Scan(this);
                CurrentAction.Execute(this);
                CurrentAction.TurretControl.Turn(this);

                Execute();
            }
        }

        public override void OnHitWall(HitWallEvent evnt)
        {
            if (HitWallDelegate != null)
            {
                HitWallDelegate(evnt);
                return;
            }

            if (CurrentAction.GetType() ==  HitWallAction.GetType())
                CurrentAction = CurrentAction.PreviousAction;

            Type[] argTypes = { typeof(Action) };
            object[] args = { CurrentAction };
            ConstructorInfo ctor = HitWallAction.GetType().GetConstructor(argTypes);
            Action obj = (Action)ctor.Invoke(args);

            //Action instance = (Action)Activator.CreateInstance(HitWallAction, args);
            CurrentAction = obj;
        }

        public override void OnRobotDeath(RobotDeathEvent evnt)
        {
            DestroyedTanks.Add(evnt.Name);
        }

        public override void OnScannedRobot(ScannedRobotEvent evnt)
        {
            // store all tanks
            Tanks[evnt.Name] = new TankHelper(evnt, this);

            CurrentAction.TurretControl.Fire(this, evnt);

            // determine if we should switch targets
            if (TargetTank.Distance < evnt.Distance && TargetTank.Name != evnt.Name)
                return;

            TargetTank = Tanks[evnt.Name];
        }
    }

}
