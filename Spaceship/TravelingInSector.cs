using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spaceship
{
    internal class TravelingInSector : ITravelingType
    {
        public double CalcDistance(Coordinate spaceshipPositionInSector, Coordinate spaceshipPositionInWorld, Coordinate destination)
        {
            return Math.Sqrt(
                 Math.Pow(spaceshipPositionInSector.GetCoordinateX() - destination.GetCoordinateX(), 2) +
                 Math.Pow(spaceshipPositionInSector.GetCoordinateY() - destination.GetCoordinateY(), 2) +
                 Math.Pow(spaceshipPositionInSector.GetCoordinateZ() - destination.GetCoordinateZ(), 2));
        }
        public Coordinate CalcNewPositionInSector(Coordinate spaceshipPositionInSector, Coordinate destination)
        {           
            return destination;
        }
        public Coordinate CalcNewPositionInWorld(Coordinate spaceshipPositionInWorld, Coordinate destination)
        {
            return destination;
        }
    }
}
