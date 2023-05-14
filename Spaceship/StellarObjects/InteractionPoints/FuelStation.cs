namespace ProjectSpaceship.StellarObjects.InteractionPoints
{
    internal class FuelStation
    {
        double fuel;
        double fuelMax;
        double fuelPrice;
        public FuelStation()
        {
            Random rand = new Random();
            this.fuelMax = rand.Next(1000, 10000);
            this.fuel = fuelMax;
            this.fuelPrice = 0.05;
        }
        public double GetFuelPrice()
        {
            return fuelPrice;
        }
        public void SetFuelPrice(double fuel)
        {
            this.fuelPrice = fuelPrice;
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
