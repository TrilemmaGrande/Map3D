using ProjectSpaceship.StellarObjects;
using ProjectSpaceship.TableBuilder;

namespace ProjectSpaceship
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
            Random rand = new Random();

            StellarObject[] stellarObjectsArray = {
                new BlackHole(new Coordinate(x, y, z), "TestOwner"),
                new Planet(new Coordinate(x, y, z), "TestOwner"),
                new Asteroid(new Coordinate(x, y, z), "TestOwner"),
                new Station(new Coordinate(x, y, z), "TestOwner" )};


            stellarObjects.Add(stellarObjectsArray[rand.Next(stellarObjectsArray.Length)]);
        }
        public void OrderStellarObjectsByCoordinates()
        {
            stellarObjects = stellarObjects.OrderBy(p => p.GetStellarObjectCoordinate().GetCoordinateX())
                                    .ThenBy(p => p.GetStellarObjectCoordinate().GetCoordinateY())
                                    .ThenBy(p => p.GetStellarObjectCoordinate().GetCoordinateZ())
                                    .ToList();
        }
        public void PrintStellarObjectsCoordinates()
        {
            OrderStellarObjectsByCoordinates();
            Table coordinateTable = new Table(stellarObjects.Count(), 2, 13);
            foreach (var stellarObject in stellarObjects)
            {
                coordinateTable.AddCells(
                    new Cell(
                        $"{stellarObject.GetStellarObjectCoordinate().GetCoordinateX(),3} |" +
                        $"{stellarObject.GetStellarObjectCoordinate().GetCoordinateY(),3} |" +
                        $"{stellarObject.GetStellarObjectCoordinate().GetCoordinateZ(),3}"),
                    new Cell(stellarObject.GetType().Name));
            }
            Console.WriteLine(coordinateTable.GetTable());
        }
        public void PrintStellarObjectsMap()
        {
            OrderStellarObjectsByCoordinates();
            bool stellarObjectPrinted = false;
            for (int y = sectorMinY; y <= sectorMaxY; y++)
            {
                for (int x = sectorMinX; x <= sectorMaxX; x++)
                {
                    foreach (var stellarObject in stellarObjects)
                    {
                        if (stellarObject.GetStellarObjectCoordinate().GetCoordinateX() == x && stellarObject.GetStellarObjectCoordinate().GetCoordinateY() == y)
                        {
                            Console.Write(
                                $"{stellarObject.GetStellarObjectCoordinate().GetCoordinateX(),3}|" +
                                $"{stellarObject.GetStellarObjectCoordinate().GetCoordinateY(),3}|" +
                                $"{stellarObject.GetStellarObjectCoordinate().GetCoordinateZ(),3}");
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
        public bool StellarObjectListContains(StellarObject searchedStellarObject)
        {
            foreach (var stellarObject in stellarObjects)
            {
                if (stellarObject.GetStellarObjectCoordinate().GetCoordinateX() == searchedStellarObject.GetStellarObjectCoordinate().GetCoordinateX() &&
                    stellarObject.GetStellarObjectCoordinate().GetCoordinateY() == searchedStellarObject.GetStellarObjectCoordinate().GetCoordinateY() &&
                    stellarObject.GetStellarObjectCoordinate().GetCoordinateZ() == searchedStellarObject.GetStellarObjectCoordinate().GetCoordinateZ())
                {
                    return true;
                }
            }
            return false;
        }
        public bool StellarObjectListContains(Coordinate searchedCoordinate)
        {
            foreach (var stellarObject in stellarObjects)
            {
                if (stellarObject.GetStellarObjectCoordinate().GetCoordinateX() == searchedCoordinate.GetCoordinateX() &&
                    stellarObject.GetStellarObjectCoordinate().GetCoordinateY() == searchedCoordinate.GetCoordinateY() &&
                    stellarObject.GetStellarObjectCoordinate().GetCoordinateZ() == searchedCoordinate.GetCoordinateZ())
                {
                    return true;
                }
            }
            return false;
        }
        public StellarObject GetStellarObjectFromSectorList(Coordinate inputCoordinate)
        {
            foreach (var stellarObject in stellarObjects)
            {
                if (stellarObject.GetStellarObjectCoordinate().GetCoordinateX() == inputCoordinate.GetCoordinateX() &&
                    stellarObject.GetStellarObjectCoordinate().GetCoordinateY() == inputCoordinate.GetCoordinateY() &&
                    stellarObject.GetStellarObjectCoordinate().GetCoordinateZ() == inputCoordinate.GetCoordinateZ())
                {
                    return stellarObject;
                }
            }
            return null;
        }
    }
}
