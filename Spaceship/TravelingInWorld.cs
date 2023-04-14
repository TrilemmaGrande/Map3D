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
            return Math.Sqrt(
                 Math.Pow(spaceshipPositionInWorld.GetCoordinateX() - destination.GetCoordinateX(), 2) +
                 Math.Pow(spaceshipPositionInWorld.GetCoordinateY() - destination.GetCoordinateY(), 2) +
                 Math.Pow(spaceshipPositionInWorld.GetCoordinateZ() - destination.GetCoordinateZ(), 2)) * 100;
        }
        public double CalcTravelTime(double travelDistance, double speed)
        {
            return travelDistance / speed;
        }
        public Coordinate CalcNewPositionInSector(Coordinate spaceshipPositionInSector, Coordinate destination)
        {
            return spaceshipPositionInSector;
        }
        public Coordinate CalcNewPositionInWorld(Coordinate spaceshipPositionInWorld, Coordinate destination, World world)
        {
            if (!world.SectorListContains(destination))
            {
                world.CreateSector(destination);                
            }          
            return destination;
        }
        public void TravelWithAnimation(double travelTime, Coordinate spaceshipPositionInSector, Coordinate spaceshipPositionInWorld)
        {
            new TravelAnimation(travelTime, spaceshipPositionInSector, spaceshipPositionInWorld).StartTravelAnimation();
        }
    }
}
