using ProjectSpaceship.Spaceships.Modules;
using ProjectSpaceship.Travel;

namespace ProjectSpaceship.Spaceships
{
    class Spaceship
    {
        private string name;
        private double health;
        private Engine engine;
        private Tank tank;
        private Cargo cargo;
        private double fuelConsumption;
        private Position position;
        public event EventHandler<TravelingEventArgs> OnTraveling;

        public Spaceship(string name, Tank tank, Engine engine, Cargo cargo, Position position)
        {
            this.name = name;
            this.engine = engine;
            this.tank = tank;
            this.cargo = cargo;
            fuelConsumption = GetWeight() / 1000 * engine.GetPowerMax();
            this.position = position;
            this.health = 100;
        }
        public void Travel(ITravelingType travelingType, Coordinate destination)
        {
            Traveling travel = new Traveling(travelingType, this, destination);
            OnTraveling?.Invoke(this, new TravelingEventArgs(travel.GetNewPositionInWorld()));
            position.SetCoordinate(travel.GetNewPositionInSector());
            tank.SetFuel(tank.GetFuel() - travel.CalcFuelConsumption());
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
        public double GetHealth()
        {
            return health;
        }
        public void SetHealth(double health)
        {
            this.health = health;
        }
        public void SetFuel(double fuel)
        {
            tank.SetFuel(fuel);
        }
        public double GetFuel()
        {
            return tank.GetFuel();
        }
        public void IncreaseFuel(double fuelToAdd)
        {
            this.tank.SetFuel(tank.GetFuel() + fuelToAdd);
        }
        public void DecreaseFuel(double fuelToSub)
        {
            this.tank.SetFuel(tank.GetFuel() - fuelToSub);
        }
        public double GetFuelMax()
        {
            return tank.GetFuelMax();
        }
        public double GetPowerMax()
        {
            return engine.GetPowerMax();
        }
        public double GetPower()
        {
            return engine.GetPower();
        }
        public void SetPower(double power)
        {
            engine.SetPower(power);
        }
        public double GetFuelConsumption()
        {
            return fuelConsumption;
        }
        public double GetWeight()
        {
            return tank.GetWeight() + engine.GetWeight() + cargo.GetWeight() + cargo.GetLoadWeight();
        }
        public string GetName()
        {
            return name;
        }
    }
}
