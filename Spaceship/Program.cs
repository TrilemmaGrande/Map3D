namespace Spaceship
{
    //TODO:
    //TravelingInSector and TravelingInWorld Methods
    //Maybe in Traveling dynamically adjust time/fuelConsumption with enginePower and speedMax for EcoTraveling (more Time = less Fuelconsumption)
    internal class Program
    {
        static void Main(string[] args)
        {

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
    class Spaceship
    {
        private string name;
        private double speedMax;
        private double enginePower;
        private double fuelMax;
        private double fuel;
        private double weight;
        private double fuelConsumption;
        private Coordinate positionInSector;
        private Coordinate positionInWorld;

        public Spaceship(string name, double speedMax, double weight, double fuelMax, double enginePower, Coordinate positionInSector, Coordinate positionInWorld)
        {
            this.name = name;
            this.speedMax = speedMax;
            this.fuelMax = fuelMax;
            fuel = fuelMax;
            this.weight = weight;
            fuelConsumption = weight / 1000 * enginePower;
            this.positionInSector = positionInSector;
            this.positionInWorld = positionInWorld;
        }
        public void Travel(ITravelingType travelingType, Coordinate destination)
        {
            Traveling travel = new Traveling(travelingType, fuelConsumption, speedMax, positionInSector, positionInWorld, destination);
            if (fuel - travel.CalcFuelConsumption() > 0)
            {
                Console.WriteLine("Treibstoff reicht nicht aus");
            }
            else
            {
                travel.TravelWithAnimation();
            }
        }

    }





}