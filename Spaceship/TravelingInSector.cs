using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSpaceship
{
    internal class TravelingInSector : ITravelingType
    {
        public double CalcDistance(Traveling traveling)
        {
            Coordinate spaceshipPositionInSector = traveling.GetSpaceship().GetPosition().GetCoordinate();
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
        public Coordinate CalcNewPositionInWorld(Traveling traveling)
        {
            return traveling.GetSpaceship().GetPosition().GetSector().GetSectorCoordinate();
        }
        public void TravelWithAnimation(Traveling traveling)
        {
            //no animation for Traveling inside Sector
        }
    }
}
