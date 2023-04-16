namespace Spaceship
{
    class Traveling
    {
        private Coordinate destination;
        private Spaceship spaceship;
        private ITravelingType travelingType;
        private double travelTime;
        private double travelDistance;

        public Traveling(ITravelingType travelingType, Spaceship spaceship, Coordinate destination)
        {
            this.spaceship = spaceship;
            this.travelingType = travelingType;
            this.destination = destination;
            travelDistance = travelingType.CalcDistance(this);
            travelTime = travelDistance / spaceship.GetSpeedMax();
        }
        public double CalcFuelConsumption()
        {
            return travelDistance / spaceship.GetSpeedMax() * spaceship.GetFuelConsumption();
        }
        public Coordinate GetNewPositionInSector()
        {
            return travelingType.CalcNewPositionInSector(this);
        }
        public Sector GetNewPositionInWorld()
        {
            return travelingType.CalcNewPositionInWorld(this);
        }
        public void TravelAnimation()
        {
            travelingType.TravelWithAnimation(this);
        }
        public double GetTravelTime()
        {
            return travelTime;
        }
        public Coordinate GetDestination()
        {
            return destination;
        }
        public Spaceship GetSpaceship()
        {
            return spaceship;
        }
    }
}
