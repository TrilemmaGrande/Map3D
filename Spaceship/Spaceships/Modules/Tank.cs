namespace ProjectSpaceship.Spaceships.Modules
{
    internal class Tank : Module
    {
        private double fuel;
        private double fuelMax;

        public Tank(double priceValue, double fuelMax, double healthMax, double armor, double weight, double shieldMax) : base(priceValue, healthMax, armor, weight, shieldMax)
        {
            fuel = fuelMax;
            this.fuelMax = fuelMax;
        }

        public double GetFuel()
        {
            return fuel;
        }
        public void SetFuel(double fuel)
        {
            this.fuel = fuel;
        }
        public double GetFuelMax()
        {
            return fuelMax;
        }
    }
}
