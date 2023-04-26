using ProjectSpaceship;

namespace Spaceship
{
    class Spaceship
    {
        private string name;
        private SpaceshipEngine spaceshipEngine;
        private SpaceshipTank spaceshipTank;
        private double weight;
        private double fuelConsumption;
        private Position position;

        public event EventHandler<TravelingEventArgs> OnTraveling;

        public Spaceship(string name, double weight, SpaceshipTank spaceshipTank, SpaceshipEngine spaceshipEngine, Position position)
        {
            this.name = name;
            this.spaceshipEngine = spaceshipEngine;
            this.spaceshipTank = spaceshipTank;
            this.weight = weight + spaceshipTank.GetWeight() + spaceshipEngine.GetWeight();
            fuelConsumption = weight / 1000 * spaceshipEngine.GetSpeedMax();
            this.position = position;
        }
        public void Travel(ITravelingType travelingType, Coordinate destination)
        {
            Traveling travel = new Traveling(travelingType, this, destination);
            OnTraveling?.Invoke(this, new TravelingEventArgs(travel.GetNewPositionInWorld()));
            position.SetCoordinate(travel.GetNewPositionInSector());
            spaceshipTank.SetFuel(spaceshipTank.GetFuel() - travel.CalcFuelConsumption());
            travel.TravelAnimation();
            travel = null;
        }
        public double CalcTravelingFuelConsumption(ITravelingType travelingType, Coordinate destination)
        {
            Traveling travel = new Traveling(travelingType, this, destination);
            travel.CalcFuelConsumption();
            double fuelConsumption = travel.CalcFuelConsumption();
            travel = null;
            return fuelConsumption;
        }
        public Position GetPosition()
        {
            return position;
        }
        public void SetFuel(double fuel)
        {
            spaceshipTank.SetFuel(fuel);
        }
        public double GetFuel()
        {
            return spaceshipTank.GetFuel();
        }
        public double GetFuelMax()
        {
            return spaceshipTank.GetFuelMax();
        }
        public double GetSpeedMax()
        {
            return spaceshipEngine.GetSpeedMax();
        }
        public double GetSpeed()
        {
            return spaceshipEngine.GetSpeed();
        }
        public void SetSpeed(double speed)
        {
            spaceshipEngine.SetSpeed(speed);
        }
        public double GetFuelConsumption()
        {
            return fuelConsumption;
        }
    }
}
