namespace Map3D
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Travel travel = new Travel(new Coordinate(20, 20, 20));
            for (int i = 0; i < 100; i++)
            {
                Console.Clear();
                travel.TravelAnimation();
                Thread.Sleep(33);
            }

            int mapScale = 100;
            int mapMinXY = mapScale / -2;
            int mapMaxXY = mapScale / 2;

            Random rand = new Random();

            Sector sector = new Sector(new Coordinate(0, 0, 0));

            for (int i = 0; i < rand.Next(1, 100); i++)
            {
                int randX = rand.Next(mapMinXY, mapMaxXY + 1);
                int randY = rand.Next(mapMinXY, mapMaxXY + 1);
                int randZ = rand.Next(mapMinXY, mapMaxXY + 1);

                sector.SetStellarObjectCoordinate(randX, randY, randZ);
            }
            sector.PrintSector();
            sector.PrintStellarObjectsCoordinates();

        }
    }

    class Coordinate
    {
        private int xCoord;
        private int yCoord;
        private int zCoord;

        public Coordinate(int x, int y, int z)
        {
            xCoord = x;
            yCoord = y;
            zCoord = z;
        }
        public int GetCoordinateX()
        {
            return xCoord;
        }
        public int GetCoordinateY()
        {
            return yCoord;
        }
        public int GetCoordinateZ()
        {
            return zCoord;
        }
    }
    class Sector
    {
        private List<Coordinate> stellarObjects = new List<Coordinate>();
        private Coordinate sectorPosition;
        private const int sectorScaleFormat = 100;
        private int sectorMaxX = sectorScaleFormat / 2;
        private int sectorMinX = sectorScaleFormat / -2;
        private int sectorMaxY = sectorScaleFormat / 2;
        private int sectorMinY = sectorScaleFormat / -2;
        private int sectorMaxZ = sectorScaleFormat / 2;
        private int sectorMinZ = sectorScaleFormat / -2;

        public Sector(Coordinate sectorPosition)
        {
            this.sectorPosition = sectorPosition;
        }

        public bool SetStellarObjectCoordinate(int x, int y, int z)
        {
            if (x <= sectorScaleFormat && y <= sectorScaleFormat && z <= sectorScaleFormat)
            {
                foreach (var stellarObject in stellarObjects)
                {
                    if (stellarObject.GetCoordinateX() == x &&
                        stellarObject.GetCoordinateY() == y &&
                        stellarObject.GetCoordinateZ() == z)
                    {
                        return false;
                    }
                }
                stellarObjects.Add(new Coordinate(x, y, z));
                stellarObjects = stellarObjects.OrderBy(p => p.GetCoordinateX())
                                         .ThenBy(p => p.GetCoordinateY())
                                         .ThenBy(p => p.GetCoordinateZ())
                                         .ToList<Coordinate>();
                return true;
            }
            else
            {
                return false;
            }
        }
        public List<Coordinate> GetPlanetCoordinates()
        {
            return stellarObjects;
        }
        public void PrintStellarObjectsCoordinates()
        {
            foreach (var stellarObject in stellarObjects)
            {
                Console.WriteLine(
                    $"{stellarObject.GetCoordinateX(),3} \t" +
                    $"{stellarObject.GetCoordinateY(),3} \t" +
                    $"{stellarObject.GetCoordinateZ(),3}");
            }
        }
        public void PrintSector()
        {
            bool stellarObjectPrinted = false;
            for (int y = sectorMinY; y <= sectorMaxY; y++)
            {
                for (int x = sectorMinX; x <= sectorMaxX; x++)
                {
                    foreach (var stellarObject in stellarObjects)
                    {
                        if (stellarObject.GetCoordinateX() == x && stellarObject.GetCoordinateY() == y)
                        {
                            Console.Write(
                                $"{stellarObject.GetCoordinateX(),3}|" +
                                $"{stellarObject.GetCoordinateY(),3}|" +
                                $"{stellarObject.GetCoordinateZ(),3}");
                            stellarObjectPrinted = true;
                            break;
                        }
                    }
                    if (!stellarObjectPrinted)
                    {
                        Console.Write(" ");
                    }
                    stellarObjectPrinted = false;
                }
                Console.WriteLine();
            }
        }
    }
    class Travel
    {
        private Coordinate travelDestination;
        private double travelTime;
        private double travelDistance;

        public Travel(Coordinate travelTarget)
        {
            this.travelDestination = travelTarget;
            //this.travelDistance = Spaceship.Position -> travelTarget
            //this.travelTime =  travelDistance / Spaceship.Speed
            //Spaceship.Fuel -= travelDistance * Spaceship.FuelConsumption
        }

        public void TravelAnimation()
        {
            string[] bottomLeft = { "  ", "  ", " /", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  " };
            string[] topLeft = { " `", "  ", " \\", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  " };
            string[] topRight = { " ´", "  ", " /", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  " };
            string[] bottomRight = { "  ", " \\", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  " };
            string[] middleHorizontal = { " _", "- ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  " };
            string[] middleVertikal = { " |", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  " };

            Random rand = new Random();

            for (int y = 20; y >= -20; y--)
            {
                for (int x = -20; x < 20; x++)
                {
                    if (y < -5 && x < -5)
                    {
                        Console.Write(bottomLeft[rand.Next(0, 15)]);
                    }
                    else if (y > 5 && x < -5)
                    {
                        Console.Write(topLeft[rand.Next(0, 15)]);
                    }
                    else if (y > 5 && x > 5)
                    {
                        Console.Write(topRight[rand.Next(0, 15)]);
                    }
                    else if (y < -5 && x > 5)
                    {
                        Console.Write(bottomRight[rand.Next(0, 14)]);
                    }
                    else if ((x < -5 || x > 5) && y < 5 && y > -5 && y != 0)
                    {
                        Console.Write(middleHorizontal[rand.Next(0, 15)]);
                    }
                    else if ((y < -5 || y > 5) && x > -5 && x < 5)
                    {
                        Console.Write(middleVertikal[rand.Next(0, 14)]);
                    }
                    else if (y > -5 && y < 5 && x > -5 && x < 5)
                    {
                        Console.Write("  ");
                    }
                    if (y == 0 && x == 15)
                    {
                        Console.Write(
                                 $"{travelDestination.GetCoordinateX(),17}|" +
                                 $"{travelDestination.GetCoordinateY(),3}|" +
                                 $"{travelDestination.GetCoordinateZ(),3}");
                    }
                }
                Console.WriteLine();
            }
        }
    }
}