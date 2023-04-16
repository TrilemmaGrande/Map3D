namespace Spaceship
{
    class Sector
    {
        private List<StellarObject> stellarObjects = new List<StellarObject>();
        private Coordinate sectorPosition;
        private const int sectorScaleFormat = 100;
        private const int maxStellarObjects = 20;
        private int sectorMaxX = sectorScaleFormat / 2;
        private int sectorMinX = sectorScaleFormat / -2;
        private int sectorMaxY = sectorScaleFormat / 2;
        private int sectorMinY = sectorScaleFormat / -2;
        private int sectorMaxZ = sectorScaleFormat / 2;
        private int sectorMinZ = sectorScaleFormat / -2;

        public Sector(Coordinate sectorPosition)
        {
            this.sectorPosition = sectorPosition;

            Random rand = new Random();

            for (int i = 0; i < rand.Next(1, maxStellarObjects); i++)
            {
                int randX = rand.Next(sectorMinX, sectorMaxX + 1);
                int randY = rand.Next(sectorMinY, sectorMaxY + 1);
                int randZ = rand.Next(sectorMinZ, sectorMaxZ + 1);

                SetStellarObjectCoordinate(randX, randY, randZ);
            }
        }
        public Coordinate GetSectorCoordinate()
        {
            return sectorPosition;
        }
        public void SetStellarObjectCoordinate(int x, int y, int z)
        {
            stellarObjects.Add(new StellarObject(new Coordinate(x, y, z)));
            stellarObjects = stellarObjects.OrderBy(p => p.GetCoordinate().GetCoordinateX())
                                     .ThenBy(p => p.GetCoordinate().GetCoordinateY())
                                     .ThenBy(p => p.GetCoordinate().GetCoordinateZ())
                                     .ToList();
        }
        public void PrintStellarObjectsCoordinates()
        {
            foreach (var stellarObject in stellarObjects)
            {
                Console.WriteLine(
                    $"{stellarObject.GetCoordinate().GetCoordinateX(),3} \t" +
                    $"{stellarObject.GetCoordinate().GetCoordinateY(),3} \t" +
                    $"{stellarObject.GetCoordinate().GetCoordinateZ(),3}");
            }
        }
        public void PrintStellarObjectsMap()
        {
            bool stellarObjectPrinted = false;
            for (int y = sectorMinY; y <= sectorMaxY; y++)
            {
                for (int x = sectorMinX; x <= sectorMaxX; x++)
                {
                    foreach (var stellarObject in stellarObjects)
                    {
                        if (stellarObject.GetCoordinate().GetCoordinateX() == x && stellarObject.GetCoordinate().GetCoordinateY() == y)
                        {
                            Console.Write(
                                $"{stellarObject.GetCoordinate().GetCoordinateX(),3}|" +
                                $"{stellarObject.GetCoordinate().GetCoordinateY(),3}|" +
                                $"{stellarObject.GetCoordinate().GetCoordinateZ(),3}");
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
