namespace Spaceship
{
    class Traveling
    {
        private Coordinate destination;
        private Coordinate spaceshipPositionInSector;
        private Coordinate spaceshipPositionInWorld;
        private ITravelingType travelingType;
        private double travelTime;
        private double travelDistance;
        private double speed;
        private double fuelConsumption;
        private World world;
        public Traveling(ITravelingType travelingType, double fuelConsumption, double speed, Coordinate spaceshipPositionInSector, Coordinate spaceshipPositionInWorld, Coordinate destination, World world)
        {
            this.speed = speed;
            this.world = world;
            this.travelingType = travelingType;
            this.destination = destination;
            this.fuelConsumption = fuelConsumption;
            this.spaceshipPositionInWorld = spaceshipPositionInWorld;
            this.spaceshipPositionInSector = spaceshipPositionInSector;
            travelDistance = travelingType.CalcDistance(spaceshipPositionInSector, spaceshipPositionInWorld, destination);
            travelTime = travelingType.CalcTravelTime(travelDistance, speed);
        }
        public double CalcFuelConsumption()
        {
            return travelDistance / speed * fuelConsumption;
        }
        public Coordinate GetNewPositionInSector()
        {
            return travelingType.CalcNewPositionInSector(spaceshipPositionInSector, destination);
        }
        public Coordinate GetNewPositionInWorld()
        {
            return travelingType.CalcNewPositionInWorld(spaceshipPositionInWorld, destination, world);
        }
        public void TravelAnimation()
        {
            travelingType.TravelWithAnimation(travelTime, GetNewPositionInSector(), GetNewPositionInWorld());
        }
    }
}
