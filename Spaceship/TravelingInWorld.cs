using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spaceship
{
    internal class TravelingInWorld : ITravelingType
    {
        public double CalcDistance(Coordinate spaceshipPositionInSector, Coordinate spaceshipPositionInWorld, Coordinate destination)
        {
            //this is only point to point in sector, but not from point to sector.
            return Math.Sqrt(
                 Math.Pow(spaceshipPositionInWorld.GetCoordinateX() - destination.GetCoordinateX(), 2) +
                 Math.Pow(spaceshipPositionInWorld.GetCoordinateY() - destination.GetCoordinateY(), 2) +
                 Math.Pow(spaceshipPositionInWorld.GetCoordinateZ() - destination.GetCoordinateZ(), 2));
        }
        public Coordinate CalcNewPositionInSector(Coordinate spaceshipPositionInSector, Coordinate destination)
        {
            return spaceshipPositionInSector;
        }
        public Coordinate CalcNewPositionInWorld(Coordinate spaceshipPositionInWorld, Coordinate destination)
        {
            return destination;
        }
    }
}
