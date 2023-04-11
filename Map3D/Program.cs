namespace Map3D
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Travel travel = new Travel(new Coordinate(20, 20, 20));
            //for (int i = 0; i < 100; i++)
            //{
            //    Console.Clear();
            //    travel.TravelAnimation();
            //    Thread.Sleep(33);
            //}
            World world = new World();
            world.CreateSector(new Coordinate(0, 0, 0));
            world.CreateSector(new Coordinate(0, 0, 1));

            foreach (var sector in world.GetSectors())
            {
                sector.PrintStellarObjectsMap();
                sector.PrintStellarObjectsCoordinates();
            }

        }
    }





}