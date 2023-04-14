namespace Spaceship
{
    class Traveling
    {
        private Coordinate destination;
        private Coordinate spaceshipPositionInSector;
        private Coordinate spaceshipPositionInWorld;
        private Spaceship spaceship;
        private ITravelingType travelingType;
        private double travelTime;
        private double travelDistance;
        private double speed;
        private double fuelConsumption;
        private World world;
        public Traveling(ITravelingType travelingType, Spaceship spaceship, Coordinate destination)
        {
            this.spaceship = spaceship;
            this.speed = spaceship.GetSpeedMax();
            this.world = spaceship.GetWorld();
            this.travelingType = travelingType;
            this.destination = destination;
            this.fuelConsumption = spaceship.GetFuelConsumption();
            this.spaceshipPositionInWorld = spaceship.GetPositionInWorld();
            this.spaceshipPositionInSector = spaceship.GetPositionInSector();
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
