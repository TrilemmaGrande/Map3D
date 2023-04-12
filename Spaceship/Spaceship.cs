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
        private Coordinate positionInWorld;
        private Sector sector;
        private World world;

        public Spaceship(string name, double speedMax, double weight, double fuelMax, double enginePower, Coordinate positionInSector, Coordinate positionInWorld, World world)
        {
            this.name = name;
            this.speedMax = speedMax;
            this.fuelMax = fuelMax;
            fuel = fuelMax;
            this.weight = weight;
            fuelConsumption = weight / 10000 * enginePower;
            this.positionInSector = positionInSector;
            this.positionInWorld = positionInWorld;
            this.world = world;
            if (!world.SectorListContains(positionInWorld))
            {
                world.CreateSector(positionInWorld);
                this.sector = world.GetSectorFromSectorList(positionInWorld);
            }
            else
            {
                this.sector = world.GetSectorFromSectorList(positionInWorld);
            }
        }
        public void Travel(ITravelingType travelingType, Coordinate destination)
        {
            Traveling travel = new Traveling(travelingType, fuelConsumption, speedMax, positionInSector, positionInWorld, destination);
            if (fuel - travel.CalcFuelConsumption() > 0)
            {
                Console.WriteLine($"Treibstoffverbrauch: { travel.CalcFuelConsumption()}");
                Console.WriteLine("Treibstoff reicht nicht aus");
            }
            else
            {
                if (!world.SectorListContains(destination))
                {
                    world.CreateSector(destination);
                    SetSector(world.GetSectorFromSectorList(destination));
                }
                positionInWorld = travel.GetNewPositionInWorld();
                positionInSector = travel.GetNewPositionInSector();
                travel.TravelWithAnimation();
                fuel -= travel.CalcFuelConsumption();
                sector.PrintStellarObjectsMap();

            }
        }
        public Coordinate GetPositionInWorld()
        {
            return positionInWorld;
        }
        public Coordinate GetPositionInSector()
        {
            return positionInSector;
        }
        public Sector GetSector()
        {
            return sector;
        }
        private void SetSector(Sector sector)
        {
            this.sector = sector;
        }
    }
}
