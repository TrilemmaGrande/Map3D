namespace Spaceship
{
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
        private Sector sector;
        private World world;

        public Spaceship(string name, double speedMax, double weight, double fuelMax, double enginePower, Coordinate positionInSector, Sector sector, World world)
        {
            this.name = name;
            this.speedMax = speedMax;
            this.enginePower = enginePower;
            this.fuelMax = fuelMax;
            this.fuel = fuelMax;
            this.weight = weight;
            fuelConsumption = weight / 1000 * enginePower;
            this.positionInSector = positionInSector;
            this.sector = sector;
            this.world = world;            
        }
        public void Travel(ITravelingType travelingType, Coordinate destination)
        {
            Traveling travel = new Traveling(travelingType, this, destination);      
            sector = travel.GetNewPositionInWorld();
            positionInSector = travel.GetNewPositionInSector();
            fuel -= travel.CalcFuelConsumption();
            travel.TravelAnimation();
        }
        public double CalcTravelingFuelConsumption(ITravelingType travelingType, Coordinate destination)
        {
            return new Traveling(travelingType, this, destination).CalcFuelConsumption();
        }
        public Sector GetPositionInWorld()
        {
            return sector;
        }
        public Coordinate GetPositionInSector()
        {
            return positionInSector;
        }
        public void SetFuel(double fuel)
        {
            this.fuel = fuel;
        }
        public double GetFuel()
        {
            return fuel;
        }
        public double GetSpeedMax()
        {
            return speedMax;
        }
        public World GetWorld()
        {
            return world;
        }
        public double GetFuelConsumption()
        {
            return fuelConsumption;
        }
      

    }
}
