
using TYLER.Tanks;

namespace TYLER.Scanners
{
    internal class FullScanner : Scanner
    {
        public override void Scan(TyTank tyTank)
        {
            tyTank.SetTurnRadarLeft(45);
        }
    }
}
