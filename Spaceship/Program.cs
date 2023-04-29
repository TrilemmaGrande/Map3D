using ProjectSpaceship.Spaceships;
using ProjectSpaceship.Spaceships.Modules;
using ProjectSpaceship.StellarObjects;
using ProjectSpaceship.Travel;

namespace ProjectSpaceship
{
    //TODO:

    //Maybe in Traveling dynamically adjust time/fuelConsumption with EngineSpeed for EcoTraveling (more Time = less Fuelconsumption), only MaxSpeed right now.
    //Code is written, make it beautiful!


    // BELOW IS JUST FOR TESTING (maybe use chunks of this later).
    internal class Program
    {
        static void Main(string[] args)
        {
            bool gameRunning = true;

            Coordinate spaceShipSpawnPoint = new Coordinate(5, 10, 10);
            Sector spaceShipSpawnSector = new Sector(new Coordinate(1, 1, 1));

            string spaceshipName = "Apollo1";

            World world = new World();
            Player player = new Player("TestPlayer");
            Spaceship spaceship;          

            world.CreatePlayerSpaceship(
                spaceshipName,
                new Tank(100, 100, 70, 2, 10, 30),
                new Engine(150, 10, 100, 5, 20, 50),
                new Cargo(50, 100, 50, 2, 5, 20),
                new Position(spaceShipSpawnSector, spaceShipSpawnPoint));

            spaceship = world.GetPlayerSpaceship();
            player.SetSpaceship(spaceship);

            while (gameRunning)
            {
                if (spaceship.GetPosition().GetSector().StellarObjectListContains(spaceship.GetPosition().GetCoordinate()))
                {
                    if (spaceship.GetPosition().GetSector().GetStellarObjectFromSectorList(spaceship.GetPosition().GetCoordinate()) is SpaceStation)
                    {
                        SpaceStationMenu(world, spaceship);
                    }
                    if (spaceship.GetPosition().GetSector().GetStellarObjectFromSectorList(spaceship.GetPosition().GetCoordinate()) is Asteroid)
                    {
                        AsteroidMenu(world, spaceship);
                    }
                    if (spaceship.GetPosition().GetSector().GetStellarObjectFromSectorList(spaceship.GetPosition().GetCoordinate()) is Planet)
                    {
                        PlanetMenu(world, spaceship);
                    }
                }
                PrintHeader(spaceship);
                Console.WriteLine("1 = travel \t 2 = scan sector \t 0 = quit");
                string userInput = Console.ReadLine();
                Console.Clear();
                if (userInput == "1")
                {
                    TravelMenu(world);
                }
                else if (userInput == "2")
                {
                    world.GetSectorFromSectorList(spaceship.GetPosition().GetSector().GetSectorCoordinate()).PrintStellarObjectsMap();
                    world.GetSectorFromSectorList(spaceship.GetPosition().GetSector().GetSectorCoordinate()).PrintStellarObjectsCoordinates();
                    Console.WriteLine();
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

        private static void SpaceStationMenu(World world, Spaceship spaceship)
        {
            Console.WriteLine("we found a spacestation!");
            SpaceStation spacestation = spaceship.GetPosition().GetSector().GetStellarObjectFromSectorList(spaceship.GetPosition().GetCoordinate()) as SpaceStation;
            bool inSpacestation = true;
            string refuelOption = "";
            while (inSpacestation)
            {
                string userInput;
                PrintHeader(spaceship);
                Console.WriteLine($"1 = travel \t 2 = scan sector  \t 3 = scan spacestation \t {refuelOption} \t 0 = back to universe");
                userInput = Console.ReadLine();
                Console.Clear();
                if (userInput == "1")
                {
                    inSpacestation = false;
                    TravelMenu(world);
                }
                else if (userInput == "2")
                {
                    world.GetSectorFromSectorList(spaceship.GetPosition().GetSector().GetSectorCoordinate()).PrintStellarObjectsMap();
                    world.GetSectorFromSectorList(spaceship.GetPosition().GetSector().GetSectorCoordinate()).PrintStellarObjectsCoordinates();
                    Console.WriteLine();
                }
                else if (userInput == "3")
                {
                    refuelOption = "4 refuel";
                    Console.WriteLine($"Owner: {spacestation.GetOwner()}");
                    Console.WriteLine($"Fuel: {spacestation.GetFuelstation().GetFuel()}");
                    Console.WriteLine();
                }
                else if (userInput == "4" || refuelOption != "")
                {
                    double usedFuel = spaceship.GetFuelMax() - spaceship.GetFuel();
                    spacestation.GetFuelstation().DecreaseFuel(usedFuel);
                    spaceship.IncreaseFuel(usedFuel);
                }
                else if (userInput == "0")
                {
                    inSpacestation = false;                    
                }
                else
                {
                    Console.WriteLine("input invalid!");
                    continue;
                }
            }
        }
        private static void AsteroidMenu(World world, Spaceship spaceship)
        {
            Console.WriteLine("we found an Asteroid!");
            bool onAsteroid = true;
            while (onAsteroid)
            {
                string userInput;
                PrintHeader(spaceship);
                Console.WriteLine("1 = travel \t 2 = scan sector \t 3 = scan asteroid \t 4 = mine \t 0 = back to universe");
                userInput = Console.ReadLine();
                Console.Clear();
                if (userInput == "1")
                {
                    onAsteroid = false;
                    TravelMenu(world);
                }
                else if (userInput == "2")
                {
                    world.GetSectorFromSectorList(spaceship.GetPosition().GetSector().GetSectorCoordinate()).PrintStellarObjectsMap();
                    world.GetSectorFromSectorList(spaceship.GetPosition().GetSector().GetSectorCoordinate()).PrintStellarObjectsCoordinates();
                    Console.WriteLine();
                }
                else if (userInput == "3")
                {
                    Console.WriteLine($"Owner: {spaceship.GetPosition().GetSector().GetStellarObjectFromSectorList(spaceship.GetPosition().GetCoordinate()).GetOwner()}");
                    //this must be done...
                }
                else if (userInput == "4")
                {
                    MiningMenu(world, spaceship);
                }
                else if (userInput == "0")
                {
                    onAsteroid = false;
                }
                else
                {
                    Console.WriteLine("input invalid!");
                    continue;
                }
            }
        }
        private static void PlanetMenu(World world, Spaceship spaceship)
        {
            Console.WriteLine("we found a Planet!");
            bool onPlanet = true;
            while (onPlanet)
            {
                string userInput;
                PrintHeader(spaceship);
                Console.WriteLine("1 = travel \t 2 = scan sector \t 3 = scan planet \t 4 = trade \t 0 = back to universe");
                userInput = Console.ReadLine();
                Console.Clear();
                if (userInput == "1")
                {
                    onPlanet = false;
                    TravelMenu(world);
                }
                else if (userInput == "2")
                {
                    world.GetSectorFromSectorList(spaceship.GetPosition().GetSector().GetSectorCoordinate()).PrintStellarObjectsMap();
                    world.GetSectorFromSectorList(spaceship.GetPosition().GetSector().GetSectorCoordinate()).PrintStellarObjectsCoordinates();
                    Console.WriteLine();
                }
                else if (userInput == "3")
                {
                    Console.WriteLine($"Owner: {spaceship.GetPosition().GetSector().GetStellarObjectFromSectorList(spaceship.GetPosition().GetCoordinate()).GetOwner()}");
                    //this must be done...
                }
                else if (userInput == "4")
                {
                    MerchantMenu(world, spaceship);
                }
                else if (userInput == "0")
                {
                    onPlanet = false;
                }
                else
                {
                    Console.WriteLine("input invalid!");
                    continue;
                }
            }
        }
        static void TravelMenu(World world)
        {
            bool travelMenu = true;
            while (travelMenu)
            {
                PrintHeader(world.GetPlayerSpaceship());
                Console.WriteLine("1 = travel in Sector \t 2 = travel to new Sector \t 0 = return");
                string userInput = Console.ReadLine();
                Console.Clear();
                if (userInput == "1")
                {
                    world.GetSectorFromSectorList(world.GetPlayerSpaceship().GetPosition().GetSector().GetSectorCoordinate()).PrintStellarObjectsCoordinates();
                    Travel(world, new TravelingInSector());
                    travelMenu = false;
                }
                else if (userInput == "2")
                {
                    Travel(world, new TravelingInWorld());
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
        static void Travel(World world, ITravelingType travelingType)
        {
            PrintHeader(world.GetPlayerSpaceship());
            Console.WriteLine("travel destination: (\"x,y,z\") between -50 and +50");
            Coordinate destination = userInputToCoordinate();
            if (world.GetPlayerSpaceship().GetFuel() - world.GetPlayerSpaceship().CalcTravelingFuelConsumption(travelingType, destination) >= 0)
            {
                world.GetPlayerSpaceship().Travel(travelingType, destination);
            }
            else
            {
                Console.WriteLine("Treibstoff reicht nicht aus");
            }
        }
        static Coordinate userInputToCoordinate()
        {
            string userInput = Console.ReadLine();
            string[] destination = userInput.Split(",");
            Coordinate travelDestination = new Coordinate(
                Convert.ToInt32(destination[0]),
                Convert.ToInt32(destination[1]),
                Convert.ToInt32(destination[2]));
            return travelDestination;
        }
        static void PrintHeader(Spaceship spaceship)
        {
            Console.WriteLine($"sector: {spaceship.GetPosition().GetSector().GetSectorCoordinate().CoordinateToString()} \t " +
                 $"coordinate: {spaceship.GetPosition().GetCoordinate().CoordinateToString()} \tfuel: {spaceship.GetFuel()} ");
            Console.WriteLine();
        }
        static void MiningMenu(World world, Spaceship spaceship)
        {
            bool mining = true;
            while (mining)
            {
                string userInput;
                PrintHeader(spaceship);
                Console.WriteLine("1 = select ore vein \t 2 = mine \t 0 = back to Asteroid");
                userInput = Console.ReadLine();
                Console.Clear();
                if (userInput == "1")
                {
                    //this must be done...
                }
                else if (userInput == "2")
                {
                    //this must be done...
                }
                else if (userInput == "0")
                {
                    mining = false;
                }
                else
                {
                    Console.WriteLine("input invalid!");
                    continue;
                }
            }
        }
        static void MerchantMenu(World world, Spaceship spaceship)
        {
            bool trading = true;
            while (trading)
            {
                string userInput;
                PrintHeader(spaceship);
                Console.WriteLine("1 = buy \t 2 = sell \t 0 = back to Planet");
                userInput = Console.ReadLine();
                Console.Clear();
                if (userInput == "1")
                {
                    //this must be done...
                }
                else if (userInput == "2")
                {
                    //this must be done...
                }
                else if (userInput == "0")
                {
                    trading = false;
                }
                else
                {
                    Console.WriteLine("input invalid!");
                    continue;
                }
            }
        }
    }
}
