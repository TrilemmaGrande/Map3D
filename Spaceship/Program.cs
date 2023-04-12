namespace Spaceship
{
    //TODO:
    //ASAP: Traveling in Sectors generate new Sectors. Maybe Refactoring the whole thing.
    //Somehow output of StellarObjects is fucked up. 
    //Create fuckin Planets...
    //Maybe in Traveling dynamically adjust time/fuelConsumption with enginePower and speedMax for EcoTraveling (more Time = less Fuelconsumption), only MaxSpeed right now.
    //Code is written, make it beautiful!

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
