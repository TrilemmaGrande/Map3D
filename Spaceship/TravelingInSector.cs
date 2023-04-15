using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spaceship
{
    internal class TravelingInSector : ITravelingType
    {
        public double CalcDistance(Traveling traveling)
        {
            Coordinate spaceshipPositionInSector = traveling.GetSpaceship().GetPositionInSector();
            Coordinate destination = traveling.GetDestination();

            return Math.Sqrt(
                 Math.Pow(spaceshipPositionInSector.GetCoordinateX() - destination.GetCoordinateX(), 2) +
                 Math.Pow(spaceshipPositionInSector.GetCoordinateY() - destination.GetCoordinateY(), 2) +
                 Math.Pow(spaceshipPositionInSector.GetCoordinateZ() - destination.GetCoordinateZ(), 2));
        }

        public Coordinate CalcNewPositionInSector(Traveling traveling)
        {           
            return traveling.GetDestination();
        }
        public Sector CalcNewPositionInWorld(Traveling traveling)
        {
            return traveling.GetSpaceship().GetPositionInWorld();
        }
        public void TravelWithAnimation(Traveling traveling)
        {
            //no animation for Traveling inside Sector
        }
    }
}
