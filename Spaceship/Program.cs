using ProjectSpaceship.Spaceships;
using ProjectSpaceship.Spaceships.Modules;
using ProjectSpaceship.StellarObjects;
using ProjectSpaceship.Travel;
using System.Numerics;

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
            Player player;
            Spaceship spaceship;

            world.CreatePlayerSpaceship(
                spaceshipName,
                new Tank(100, 100, 70, 2, 10, 30),
                new Engine(150, 10, 100, 5, 20, 50),
                new Cargo(50, 100, 50, 2, 5, 20),
                new Position(spaceShipSpawnSector, spaceShipSpawnPoint));
            world.CreatePlayer("Testplayer");

            spaceship = world.GetPlayerSpaceship();
            player = world.GetPlayer();
            player.SetSpaceship(spaceship);

            while (gameRunning)
            {               
                PrintHeader(world);
                Console.WriteLine("1 = travel \t 2 = scan sector \t 3 = scan position \t 0 = quit");
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
                else if (userInput == "3")
                {
                    if (spaceship.GetPosition().GetSector().StellarObjectListContains(spaceship.GetPosition().GetCoordinate()))
                    {
                        if (spaceship.GetPosition().GetSector().GetStellarObjectFromSectorList(spaceship.GetPosition().GetCoordinate()) is SpaceStation)
                        {
                            SpaceStationMenu(world);
                        }
                        else if(spaceship.GetPosition().GetSector().GetStellarObjectFromSectorList(spaceship.GetPosition().GetCoordinate()) is Asteroid)
                        {
                            AsteroidMenu(world);
                        }
                        else if(spaceship.GetPosition().GetSector().GetStellarObjectFromSectorList(spaceship.GetPosition().GetCoordinate()) is Planet)
                        {
                            PlanetMenu(world);
                        }                      
                    }
                    else
                    {
                        Console.WriteLine("there is nothing here!");
                        Console.WriteLine();
                    }
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

        private static void SpaceStationMenu(World world)
        {
            Console.Clear();
            Console.WriteLine("we found a spacestation! \n");
            Spaceship spaceship = world.GetPlayerSpaceship();
            SpaceStation spacestation = spaceship.GetPosition().GetSector().GetStellarObjectFromSectorList(spaceship.GetPosition().GetCoordinate()) as SpaceStation;
            bool inSpacestation = true;
            string refuelOption = "";
            while (inSpacestation)
            {
                string userInput;
                PrintHeader(world);
                Console.WriteLine($"1 = travel \t 2 = scan sector  \t 3 = scan spacestation \t {refuelOption} \t 0 = back to orbit");
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
                    Console.WriteLine();
                    Console.WriteLine($"Fuel: {spacestation.GetFuelstation().GetFuel()}");
                    Console.WriteLine();
                }
                else if (userInput == "4" && refuelOption != "")
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
        private static void AsteroidMenu(World world)
        {
            Console.Clear();
            Console.WriteLine("we found an Asteroid! \n");
            Spaceship spaceship = world.GetPlayerSpaceship();
            Asteroid asteroid = spaceship.GetPosition().GetSector().GetStellarObjectFromSectorList(spaceship.GetPosition().GetCoordinate()) as Asteroid;
            bool onAsteroid = true;
            while (onAsteroid)
            {
                string userInput;
                PrintHeader(world);
                Console.WriteLine("1 = travel \t 2 = scan sector \t 3 = scan asteroid \t 4 = mine \t 0 = back to orbit");
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
                    Console.WriteLine();
                    Console.WriteLine("Type\tAmount\tValue");
                    foreach (var resource in asteroid.GetResourceList())
                    {
                        Console.Write($"{resource.GetType().Name} \t {resource.GetAmount()} \t {resource.GetValue()}");
                        Console.WriteLine();
                    }
                    Console.WriteLine();
                }
                else if (userInput == "4")
                {
                    MiningMenu(world);
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
        private static void PlanetMenu(World world)
        {
            Console.Clear();
            Console.WriteLine("we found a Planet! \n");
            Spaceship spaceship = world.GetPlayerSpaceship();
            Planet planet = spaceship.GetPosition().GetSector().GetStellarObjectFromSectorList(spaceship.GetPosition().GetCoordinate()) as Planet;
            bool onPlanet = true;
            while (onPlanet)
            {
                string userInput;
                PrintHeader(world);
                Console.WriteLine("1 = travel \t 2 = scan sector \t 3 = scan planet \t 4 = trade \t 0 = back to orbit");
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
                    Console.WriteLine();
                    Console.WriteLine($"Merchant with {planet.GetMerchant().GetCredits()} Credits");
                    Console.WriteLine();
                }
                else if (userInput == "4")
                {
                    MerchantMenu(world);
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
                PrintHeader(world);
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
            bool inTravelMenu = true;
            while (inTravelMenu)
            {
                PrintHeader(world);
                Console.WriteLine("travel destination: (\"x,y,z\") between -50 and +50\t 0 = return");
                Coordinate destination = userInputToCoordinate();
                if (destination is null)
                {
                    inTravelMenu = false;
                    Console.Clear();
                    break;
                }
                if (world.GetPlayerSpaceship().GetFuel() - world.GetPlayerSpaceship().CalcTravelingFuelConsumption(travelingType, destination) >= 0)
                {
                    world.GetPlayerSpaceship().Travel(travelingType, destination);
                    inTravelMenu = false;
                    Console.Clear();
                    break;
                }
                else
                {
                    Console.WriteLine("Treibstoff reicht nicht aus");
                    continue;
                }
            }
        }
        static Coordinate userInputToCoordinate()
        {
            bool inMenu = true;
            while (inMenu)
            {
                string userInput = Console.ReadLine();
                if (userInput == "0")
                {
                    break;
                }
                if (userInput.Split(',').Length-1 != 2)
                {
                    Console.WriteLine("input invalid! Wrong Coordinate format!");
                    continue;
                }
                string[] destination = userInput.Split(",");
                foreach (var item in destination)
                {
                    if (item == null || Convert.ToInt32(item) < -50 || Convert.ToInt32(item) > 50)
                    {
                        Console.WriteLine("input invalid! Coordinate out of range!");
                        continue;
                    }
                }
                Coordinate travelDestination = new Coordinate(
                    Convert.ToInt32(destination[0]),
                    Convert.ToInt32(destination[1]),
                    Convert.ToInt32(destination[2]));
                return travelDestination;
            }
            return null;
        }
        static void PrintHeader(World world)
        {
            Player player = world.GetPlayer();
            Spaceship spaceship = world.GetPlayerSpaceship();
            string line = new String('-', 80);

            string printPlayer =        $"Player:     {player.GetName(),10}";
            string printLevel =         $"Level:      {player.GetLevel(),10}";
            string printExperience =    $"Exp:        {player.GetExperience(),10}";
            string printSpaceshipName = $"Spaceship:  {spaceship.GetName(),10}";
            string printSpaceshipWeight = $"Weight:     {spaceship.GetWeight(),10}";
            string printSpaceshipFuel = $"Fuel:       {spaceship.GetFuel(),10}";
            string printSector =        $"sector:     {spaceship.GetPosition().GetSector().GetSectorCoordinate().CoordinateToString(),15}";
            string printCoordinate =    $"Coordinate: {spaceship.GetPosition().GetCoordinate().CoordinateToString(),15}";
            string whiteSpace =     new String(' ',27);

            Console.WriteLine(line);
            Console.WriteLine($"| {printSector} | {printPlayer} | {printSpaceshipName}|");
            Console.WriteLine($"| {printCoordinate} | {printLevel} | {printSpaceshipWeight}|");
            Console.WriteLine($"| {whiteSpace} | {printExperience} | {printSpaceshipFuel}|");
            Console.WriteLine(line);
        }
        static void MiningMenu(World world)
        {
            bool mining = true;
            while (mining)
            {
                string userInput;
                PrintHeader(world);
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
        static void MerchantMenu(World world)
        {
            bool trading = true;
            while (trading)
            {
                string userInput;
                PrintHeader(world);
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
