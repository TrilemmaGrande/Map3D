using ProjectSpaceship;
using System;

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

            string spaceShipName = "Apollo1";
            double spaceShipWeight = 120.00;

            World world = new World();
            Spaceship spaceship;

            world.CreatePlayerSpaceship(
                spaceShipName, 
                spaceShipWeight, 
                new SpaceshipTank(100, 100, 70, 2, 10, 30), 
                new SpaceshipEngine(150, 100, 100, 5, 20, 50), 
                new Position(spaceShipSpawnSector, spaceShipSpawnPoint));

            spaceship = world.GetPlayerSpaceship();

            while (gameRunning)
            {
                string userInput;
                bool inSpacestation = true;
                if (spaceship.GetPosition().GetSector().StellarObjectListContains(spaceship.GetPosition().GetCoordinate()))
                {
                    if (spaceship.GetPosition().GetSector().GetStellarObjectFromSectorList(spaceship.GetPosition().GetCoordinate()).GetStellarType() == "Spacestation")
                    {
                        Console.WriteLine("we found a spacestation!");
                        while (inSpacestation)
                        {
                            PrintHeader(spaceship);
                            Console.WriteLine("1 = travel \t 2 = scan sector \t 3 = refuel \t 0 = back to universe");
                            userInput = Console.ReadLine();
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
                                world.GetPlayerSpaceship().SetFuel(spaceship.GetFuelMax());
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
                }
                PrintHeader(spaceship);
                Console.WriteLine("1 = travel \t 2 = scan sector \t 0 = quit");
                userInput = Console.ReadLine();
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
    }
}
