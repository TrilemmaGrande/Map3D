using Spaceship;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSpaceship
{
    internal class TravelingInWorld : ITravelingType
    {
        public double CalcDistance(Traveling traveling)
        {
            Sector spaceshipPositionInWorld = traveling.GetSpaceship().GetPosition().GetSector();
            Coordinate destination = traveling.GetDestination();

            return Math.Sqrt(
                 Math.Pow(spaceshipPositionInWorld.GetSectorCoordinate().GetCoordinateX() - destination.GetCoordinateX(), 2) +
                 Math.Pow(spaceshipPositionInWorld.GetSectorCoordinate().GetCoordinateY() - destination.GetCoordinateY(), 2) +
                 Math.Pow(spaceshipPositionInWorld.GetSectorCoordinate().GetCoordinateZ() - destination.GetCoordinateZ(), 2)) * 100;
        }
        public Coordinate CalcNewPositionInSector(Traveling traveling)
        {
            Random rand = new Random();
            Coordinate randCoord = new Coordinate(rand.Next(-50, 51), rand.Next(-50, 51), rand.Next(-50, 51));
            return randCoord;
        }
        public Coordinate CalcNewPositionInWorld(Traveling traveling)
        {
            return traveling.GetDestination();
        }
        public void TravelWithAnimation(Traveling traveling)
        {
            new TravelAnimation(traveling.GetTravelTime(), traveling.GetNewPositionInSector(), traveling.GetNewPositionInWorld()).StartTravelAnimation();
        }
    }
}
