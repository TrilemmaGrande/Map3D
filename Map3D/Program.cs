using System.Security.Cryptography.X509Certificates;

namespace Map3D
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Random rand = new Random();
            Map map = new Map(99, 2);

            for (int i = 0; i < rand.Next(1000); i++)
            {
                int randX = rand.Next(99);
                int randY = rand.Next(99);
                int randZ = rand.Next(99);

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

        public Map(int mapScaleFormat, int innerBorderRadius)
        {
            this.mapScaleFormat = mapScaleFormat;
            this.innerBorderRadius = innerBorderRadius;
        }

        public bool SetCoordinate(int x, int y, int z)
        {
            if (x <= mapScaleFormat && y <= mapScaleFormat && z <= mapScaleFormat)
            {
                coordinates.Add(new Coordinate(x, y, z));
                coordinates = coordinates.OrderBy(p => p.GetCoordinateX()).ThenBy(p => p.GetCoordinateY()).ThenBy(p => p.GetCoordinateZ()).ToList<Coordinate>();
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
                Console.WriteLine($"{coordinate.GetCoordinateX(),2} \t {coordinate.GetCoordinateY(),2} \t {coordinate.GetCoordinateZ(),2}");
            }
        }
        public void PrintMap()
        {           
            // foreach(x){ foreach (y) print Z at X,Y}}
            // Printing will get our of scale (xy)
        }
    }
}