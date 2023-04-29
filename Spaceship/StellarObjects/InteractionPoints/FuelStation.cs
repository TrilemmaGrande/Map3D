namespace ProjectSpaceship.StellarObjects.InteractionPoints
{
    internal class FuelStation
    {
        double fuel;
        double fuelMax;
        public FuelStation()
        {
            Random rand = new Random();
            this.fuelMax = rand.Next(1000, 10000);
            this.fuel = fuelMax;
        }

        public double GetFuel()
        {
            return fuel;
        }
        public void SetFuel(double fuel)
        {
            this.fuel = fuel;
        }
        public void IncreaseFuel(double fuelToAdd)
        {
            fuel += fuelToAdd;
        }
        public void DecreaseFuel(double fuelToSub)
        {
            fuel -= fuelToSub;
        }
        public double GetFuelMax()
        {
            return fuelMax;
        }
    }
}
