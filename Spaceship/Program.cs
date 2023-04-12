namespace Spaceship
{
    //TODO:
    //TravelingInSector and TravelingInWorld Methods
    //Create new Sector if travelled there first time.
    //Maybe in Traveling dynamically adjust time/fuelConsumption with enginePower and speedMax for EcoTraveling (more Time = less Fuelconsumption), only MaxSpeed right now.

    internal class Program
    {
        static void Main(string[] args)
        {
            World world = new World();
            Spaceship spaceShip;
            //layerSpaceShip = new Spaceship(name, speedMax, weight, fuelMax, enginePower, positionInSector, positionInWorld, this);
            world.CreatePlayerSpaceship("Apollo1", 10, 10, 100000, 100, new Coordinate(10, 10, 20), new Coordinate(0, 0, 0));
            spaceShip = world.GetPlayerSpaceship();
            Console.WriteLine(spaceShip.GetSector().GetSectorPosition().GetCoordinateString());
            Console.WriteLine(spaceShip.GetPositionInSector().GetCoordinateString());
            Console.WriteLine(spaceShip.GetPositionInWorld().GetCoordinateString());
            spaceShip.Travel(new TravelingInSector(), new Coordinate(11, 20, 20));
            Console.WriteLine(spaceShip.GetSector().GetSectorPosition().GetCoordinateString());
            Console.WriteLine(spaceShip.GetPositionInSector().GetCoordinateString());
            Console.WriteLine(spaceShip.GetPositionInWorld().GetCoordinateString());
            spaceShip.Travel(new TravelingInWorld(), new Coordinate(0, 0, 1));
            Console.WriteLine(spaceShip.GetSector().GetSectorPosition().GetCoordinateString());
            Console.WriteLine(spaceShip.GetPositionInSector().GetCoordinateString());
            Console.WriteLine(spaceShip.GetPositionInWorld().GetCoordinateString());
        }
    }
}
