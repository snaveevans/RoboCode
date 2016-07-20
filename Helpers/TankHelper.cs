using Robocode;
using TYLER.Tanks;

namespace TYLER
{
    internal class TankHelper
    {
        private readonly double _radarHeading;

        private readonly string _name;
        private readonly double _heading;
        private readonly double _velocity;

        #region Properties

        public double Bearing { get; set; }

        public long TurnLastSeen { get; set; }

        public double RadarHeading
        {
            get { return _radarHeading; }
        }

        public string Name
        {
            get { return _name; }
        }

        public double Heading
        {
            get { return _heading; }
        }

        public double Distance { get; set; }

        public double Velocity
        {
            get { return _velocity; }
        }
        #endregion

        #region Constructors
        public TankHelper()
        {
            _name = "";
            _heading = 0;
            Distance = 999999999;
            TurnLastSeen = 1;
        }

        public TankHelper(ScannedRobotEvent evnt, TyTank tyTank)
        {
            _name = evnt.Name;
            Distance = evnt.Distance;
            _heading = evnt.Heading;
            _velocity = evnt.Velocity;
            _radarHeading = tyTank.RadarHeading;
            Bearing = evnt.Bearing;
            TurnLastSeen = tyTank.Turn;
        } 
        #endregion

    }
}
