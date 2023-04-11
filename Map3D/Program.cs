namespace Map3D
{
    internal class Program
    {
        static void Main(string[] args)
        {

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
}