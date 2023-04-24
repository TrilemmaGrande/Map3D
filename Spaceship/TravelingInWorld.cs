using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spaceship
{
    internal class TravelingInWorld : ITravelingType
    {
        public double CalcDistance(Traveling traveling)
        {
            Sector spaceshipPositionInWorld = traveling.GetSpaceship().GetPositionInWorld();
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
        public Sector CalcNewPositionInWorld(Traveling traveling)
        {
            World world = new World();
            Coordinate destination = traveling.GetDestination();

            Sector destinationSector = world.GetSectorFromSectorList(destination);
            if (destinationSector == null)
            {
                world.CreateSector(destination);
                destinationSector = world.GetSectorFromSectorList(destination);
            }          
            return destinationSector;
        }
        public void TravelWithAnimation(Traveling traveling)
        {
            new TravelAnimation(traveling.GetTravelTime(), traveling.GetNewPositionInSector(), traveling.GetNewPositionInWorld()).StartTravelAnimation();
        }
    }
}
