namespace ProjectSpaceship.Spaceships.Modules
{
    internal class Engine : Module
    {
        private double power;
        private double powerMax;

        public Engine(double priceValue, double powerMax, double healthMax, double armor, double weight, double shieldMax) : base(priceValue, healthMax, armor, weight, shieldMax)
        {
            power = 0.00;
            this.powerMax = powerMax;
        }

        public double GetPower()
        {
            return power;
        }
        public void SetPower(double power)
        {
            this.power = power;
        }
        public double GetPowerMax()
        {
            return powerMax;
        }
    }
}
