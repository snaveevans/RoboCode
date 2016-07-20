using TYLER.Actions;
using TYLER.Tanks;

// ReSharper disable once CheckNamespace
namespace TYLER
{
    class TestTank : TyTank
    {

        public TestTank()
        {
            CurrentAction = new GoToWall(new SearchForTarget(null), GoToWall.Wall.Bottom);
        }

    }
}
