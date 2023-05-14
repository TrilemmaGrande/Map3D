using ProjectSpaceship.Spaceships;
using ProjectSpaceship.Spaceships.Modules;
using ProjectSpaceship.StellarObjects;
using ProjectSpaceship.TableBuilder;
using ProjectSpaceship.Travel;
using System.Text;

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
            world.CreatePlayer("TestPlayer");

            spaceship = world.GetPlayerSpaceship();
            player = world.GetPlayer();
            player.SetSpaceship(spaceship);

            while (gameRunning)
            {
                PrintHeader(world);
                Console.WriteLine(new Frame("1: travel  |  2: scan sector  |  3: scan position  |  0: quit").GetFrame());
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
                        else if (spaceship.GetPosition().GetSector().GetStellarObjectFromSectorList(spaceship.GetPosition().GetCoordinate()) is Asteroid)
                        {
                            AsteroidMenu(world);
                        }
                        else if (spaceship.GetPosition().GetSector().GetStellarObjectFromSectorList(spaceship.GetPosition().GetCoordinate()) is Planet)
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
                Console.WriteLine(new Frame($"1: travel  |  2: scan sector  |  3: scan spacestation{refuelOption}  |  0: back to orbit").GetFrame());
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
                    refuelOption = "  |  4 refuel";
                    Console.SetCursorPosition(0, 18);
                    Console.WriteLine($"Owner: {spacestation.GetOwner()}");
                    Console.WriteLine($"Fuel: {spacestation.GetFuelstation().GetFuel()}");
                    Console.WriteLine($"Price: { spacestation.GetFuelstation().GetFuelPrice()}");
                }
                else if (userInput == "4" && refuelOption != "")
                {
                    double usedFuel = spaceship.GetFuelMax() - spaceship.GetFuel();
                    spacestation.GetFuelstation().DecreaseFuel(usedFuel);
                    spaceship.IncreaseFuel(usedFuel);
                    world.GetPlayer().DecreaseCredits(usedFuel * spacestation.GetFuelstation().GetFuelPrice());
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
                string mineOption = "";
                string userInput;
                PrintHeader(world);
                Console.WriteLine(new Frame($"1: travel  |  2: scan sector  |  3: scan asteroid{mineOption}  |  0: back to orbit").GetFrame());
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
                    mineOption = "  |  4 mine";
                    Console.SetCursorPosition(0, 18);
                    Console.WriteLine($"Owner: {spaceship.GetPosition().GetSector().GetStellarObjectFromSectorList(spaceship.GetPosition().GetCoordinate()).GetOwner()}");
                    Console.WriteLine("Type\tAmount\tValue");
                    foreach (var resource in asteroid.GetResourceList())
                    {
                        Console.Write($"{resource.GetType().Name} \t {resource.GetAmount()} \t {resource.GetValue()}");
                        Console.WriteLine();
                    }
                    Console.WriteLine();
                }
                else if (userInput == "4" && mineOption != "")
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
            string tradeOption = "";
            while (onPlanet)
            {
                string userInput;
                PrintHeader(world);
                Console.WriteLine(new Frame($"1: travel  |  2: scan sector  |  3: scan planet{tradeOption}  |  0: back to orbit").GetFrame());
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
                    tradeOption = "  |  4 trade";
                    Console.SetCursorPosition(0, 18);
                    Console.WriteLine($"Owner: {spaceship.GetPosition().GetSector().GetStellarObjectFromSectorList(spaceship.GetPosition().GetCoordinate()).GetOwner()}");
                    Console.WriteLine($"Merchant with {planet.GetMerchant().GetCredits()} Credits");
                    Console.WriteLine();
                }
                else if (userInput == "4" && tradeOption != "")
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
                Console.WriteLine(new Frame($"1: travel in Sector  |  2: travel to new Sector  |  0: return").GetFrame());
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
                Console.WriteLine(new Frame($"travel destination: (\"x,y,z\") between -50 and +50  |  0: return").GetFrame());
                Coordinate destination = UserInputToCoordinate();
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
        static Coordinate UserInputToCoordinate()
        {
            bool inMenu = true;
            while (inMenu)
            {
                string userInput = Console.ReadLine();
                if (userInput == "0")
                {
                    break;
                }
                if (userInput.Split(',').Length - 1 != 2)
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
        static void PrintInputOption(params string[] inputOption)
        {
            Table inputOptionTable = new Table(1, inputOption.Length, 15);
            foreach (var item in inputOption)
            {
                inputOptionTable.AddCells(new Cell(item,Alignment.Left,MergeCell.MergeLeft));
            }
            Console.OutputEncoding = Encoding.Unicode;
            Console.SetCursorPosition(0, 2);
            Console.WriteLine(inputOptionTable.GetTable());
        }
        static void PrintHeader(World world)
        {
            Player player = world.GetPlayer();
            Spaceship spaceship = world.GetPlayerSpaceship();
            string whiteSpace = (" ");
            string printPlayer = $"{player.GetName()}";
            string printLevel = $"{player.GetLevel()}";
            string printExperience = $"{player.GetExperience().ToString("F2")}";
            string printCredits = $"{player.GetCredits().ToString("F2")}";
            string printSpaceshipName = $"{spaceship.GetName()}";
            string printSpaceshipWeight = $"{spaceship.GetWeight().ToString("F2")}";
            string printSpaceshipHealth = $"{spaceship.GetHealth().ToString("F2")}";
            string printSpaceshipFuel = $"{spaceship.GetFuel().ToString("F2")}";
            string printSector = $"{spaceship.GetPosition().GetSector().GetSectorCoordinate().CoordinateToString()}";
            string printCoordinate = $"{spaceship.GetPosition().GetCoordinate().CoordinateToString()}";

            Table defaultOutputTable = new Table(4, 6, 15);
            defaultOutputTable.AddCells(
                new Cell("Sector:"), new Cell(printSector,Alignment.Right,MergeCell.MergeLeft), 
                new Cell("Player:"), new Cell(printPlayer,Alignment.Right,MergeCell.MergeLeft),
                new Cell("Spaceship:"), new Cell(printSpaceshipName, Alignment.Right, MergeCell.MergeLeft));
            defaultOutputTable.AddCells(
                new Cell("Coord:"), new Cell(printCoordinate, Alignment.Right, MergeCell.MergeLeft),
                new Cell("Level:"), new Cell(printLevel, Alignment.Right, MergeCell.MergeLeft),
                new Cell("Health:"), new Cell(printSpaceshipHealth, Alignment.Right, MergeCell.MergeLeft));
            defaultOutputTable.AddCells(
                new Cell(" ",Alignment.Left,MergeCell.MergeTop), new Cell(whiteSpace, Alignment.Right, MergeCell.MergeTopLeft),
                new Cell("Exp:", Alignment.Left, MergeCell.MergeTop), new Cell(printExperience, Alignment.Right, MergeCell.MergeTopLeft),
                new Cell("Fuel", Alignment.Left, MergeCell.MergeTop), new Cell(printSpaceshipFuel, Alignment.Right, MergeCell.MergeTopLeft));
            defaultOutputTable.AddCells(
                new Cell(" ", Alignment.Left, MergeCell.MergeTop), new Cell(whiteSpace, Alignment.Right, MergeCell.MergeTopLeft),
                new Cell("Credits:", Alignment.Left, MergeCell.MergeTop), new Cell(printCredits, Alignment.Right, MergeCell.MergeTopLeft),
                new Cell("Weight:", Alignment.Left, MergeCell.MergeTop), new Cell(printSpaceshipWeight, Alignment.Right, MergeCell.MergeTopLeft));

            Console.OutputEncoding = Encoding.Unicode;
            Console.SetCursorPosition(0, 2);
            Console.WriteLine(defaultOutputTable.GetTable());
        }
        static void MiningMenu(World world)
        {
            bool mining = true;
            while (mining)
            {
                string userInput;
                PrintHeader(world);
                Console.WriteLine(new Frame($"1: select ore vein  |  2: mine |  0: back to Asteroid").GetFrame());
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
                Console.WriteLine(new Frame($"1: buy  |  2: sell |  0: back to Planet").GetFrame());
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
