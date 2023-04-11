using System.Security.Cryptography.X509Certificates;

namespace Map3D
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Random rand = new Random();
            Map map = new Map(201, 2);

            for (int i = 0; i < rand.Next(1000); i++)
            {
                int randX = rand.Next(-99, 100);
                int randY = rand.Next(-99, 100);
                int randZ = rand.Next(-99, 100);

                map.SetCoordinate(randX, randY, randZ);
            }
            map.PrintCoordinates();
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
    class Map
    {
        private List<Coordinate> coordinates = new List<Coordinate>();
        private int mapScaleFormat;
        private int innerBorderRadius;
        private int mapMaxX;
        private int mapMinX;
        private int mapMaxY;
        private int mapMinY;
        private int mapMaxZ;
        private int mapMinZ;

        public Map(int mapScaleFormat, int innerBorderRadius)
        {
            if (mapScaleFormat % 2 != 0)
            {
                mapScaleFormat++;
                Console.WriteLine($"MapScale Format nicht zulässig! Format wurde auf {mapScaleFormat} aufgerundet!");
            }
            this.mapScaleFormat = mapScaleFormat;
            this.innerBorderRadius = innerBorderRadius;
            this.mapMaxX = mapScaleFormat / 2;
            this.mapMinX = mapScaleFormat / -2;
            this.mapMaxY = mapScaleFormat / 2;
            this.mapMinY = mapScaleFormat / -2;
            this.mapMaxZ = mapScaleFormat / 2;
            this.mapMinZ = mapScaleFormat / -2;
        }

        public bool SetCoordinate(int x, int y, int z)
        {
            if (x <= mapScaleFormat && y <= mapScaleFormat && z <= mapScaleFormat)
            {
                foreach (var coordinate in coordinates)
                {
                    if (coordinate.GetCoordinateX() == x &&
                        coordinate.GetCoordinateY() == y && 
                        coordinate.GetCoordinateZ() == z)
                    {
                        return false;
                    }
                }
                coordinates.Add(new Coordinate(x, y, z));
                coordinates = coordinates.OrderBy(p => p.GetCoordinateX())
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
        public List<Coordinate> GetCoordinates()
        {
            return coordinates;
        }
        public void PrintCoordinates()
        {
            foreach (var coordinate in coordinates)
            {
                Console.WriteLine(
                    $"{coordinate.GetCoordinateX(),2} \t" +
                    $"{coordinate.GetCoordinateY(),2} \t" +
                    $"{coordinate.GetCoordinateZ(),2}");
            }
        }
        public void PrintMap()
        {
            // for(y){ for (x) print Z at X,Y}}
            // Printing will get our of scale (xy)
        }
    }
}