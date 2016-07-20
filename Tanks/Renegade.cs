
using TYLER.Actions;
using TYLER.Tanks;

// ReSharper disable once CheckNamespace
namespace TYLER
{
    public class Renegade : TyTank
    {
        public Renegade()
        {
            CurrentAction = new SearchForTarget(new SearchForTarget(null));
        }
    }
}
