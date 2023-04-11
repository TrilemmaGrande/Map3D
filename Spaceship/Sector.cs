using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Map3D
{
    class Sector
    {
        private List<Coordinate> stellarObjects = new List<Coordinate>();
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
        public Coordinate GetSectorPosition()
        {
            return sectorPosition;
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
        public List<Coordinate> GetStellarObjectsCoordinates()
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
        public void PrintStellarObjectsMap()
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
