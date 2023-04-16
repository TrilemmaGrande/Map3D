namespace Spaceship
{
    //TODO:

    //Maybe in Traveling dynamically adjust time/fuelConsumption with enginePower and speedMax for EcoTraveling (more Time = less Fuelconsumption), only MaxSpeed right now.
    //Code is written, make it beautiful!


    // BELOW IS JUST FOR TESTING (maybe use chunks of this later).
    internal class Program
    {
        static void Main(string[] args)
        {
            bool gameRunning = true;

            Coordinate spaceShipSpawnPoint = new Coordinate(5, 10, 10);
            Sector spaceShipSpawnSector = new Sector(new Coordinate(1, 1, 1));

            string spaceShipName = "Apollo1";
            double spaceShipSpeedMax = 10.00;
            double spaceShipWeight = 120.00;
            double spaceShipFuelMax = 1000.00;
            double spaceShipEnginePower = 100.00;

            World world = new World();
            Spaceship spaceShip;

            world.CreatePlayerSpaceship(spaceShipName, spaceShipSpeedMax, spaceShipWeight, spaceShipFuelMax, spaceShipEnginePower, spaceShipSpawnPoint, spaceShipSpawnSector);
            spaceShip = world.GetPlayerSpaceship();

            while (gameRunning)
            {
                Console.WriteLine($"sector: {spaceShip.GetPositionInWorld().GetSectorCoordinate().CoordinateToString()} \t " +
                    $"coordinate: {spaceShip.GetPositionInSector().CoordinateToString()} \tfuel: {spaceShip.GetFuel()} ");
                Console.WriteLine();
                Console.WriteLine("1 = travel \t 2 = map of this sector \t 3 = coordinates in this sector  \t 4 = refuel \t 0 = quit");
                string userInput = Console.ReadLine();
                Console.Clear();
                if (userInput == "1")
                {
                    TravelMenu(spaceShip);
                }
                else if (userInput == "2")
                {
                    world.GetSectorFromSectorList(spaceShip.GetPositionInWorld().GetSectorCoordinate()).PrintStellarObjectsMap();
                }
                else if (userInput == "3")
                {
                    world.GetSectorFromSectorList(spaceShip.GetPositionInWorld().GetSectorCoordinate()).PrintStellarObjectsCoordinates();
                }
                else if (userInput == "4")
                {
                    world.GetPlayerSpaceship().SetFuel(spaceShipFuelMax);
                }
                else if (userInput == "0")
                {
                    gameRunning = false;
                }
                else
                {
                    Console.WriteLine("input invalid!");
                    continue;
                }
            }
        }
        static void TravelMenu(Spaceship spaceShip)
        {
            bool travelMenu = true;
            while (travelMenu)
            {
                Console.WriteLine(
                    $"sector: {spaceShip.GetPositionInWorld().GetSectorCoordinate().CoordinateToString()} \t " +
                    $"coordinate: {spaceShip.GetPositionInSector().CoordinateToString()} \tfuel: {spaceShip.GetFuel()} ");
                Console.WriteLine();
                Console.WriteLine("1 = travel in Sector \t 2 = travel to new Sector \t 0 = return");
                string userInput = Console.ReadLine();
                Console.Clear();
                if (userInput == "1")
                {
                    TravelInSector(spaceShip);
                    travelMenu = false;
                }
                else if (userInput == "2")
                {
                    TravelInWorld(spaceShip);
                    travelMenu = false;
                }
                else if (userInput == "0")
                {
                    travelMenu = false;
                }
                else
                {
                    Console.WriteLine("input invalid!");
                    continue;
                }
            }
        }
        static void TravelInSector(Spaceship spaceShip)
        {
            Console.WriteLine("travel destination: (\"x,y,z\") between -50 and +50");
            string userInput = Console.ReadLine();
            string[] destination = userInput.Split(",");
            Coordinate travelDestination = new Coordinate(
                Convert.ToInt32(destination[0]), 
                Convert.ToInt32(destination[1]), 
                Convert.ToInt32(destination[2]));
            spaceShip.Travel(new TravelingInSector(), travelDestination);

        }
        static void TravelInWorld(Spaceship spaceShip)
        {
            Console.WriteLine("travel destination: (\"x,y,z\") between -50 and +50");
            string userInput = Console.ReadLine();
            string[] destination = userInput.Split(",");
            Coordinate travelDestination = new Coordinate(
                Convert.ToInt32(destination[0]), 
                Convert.ToInt32(destination[1]), 
                Convert.ToInt32(destination[2]));
            spaceShip.Travel(new TravelingInWorld(), travelDestination);
        }
    }
}
