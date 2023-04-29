namespace ProjectSpaceship.Spaceships
{
    internal class SpaceshipTank
    {
        private double priceValue;
        private double fuel;
        private double fuelMax;
        private double health;
        private double healthMax;
        private double armor;
        private double weight;
        private double shield;
        private double shieldMax;

        public SpaceshipTank(double priceValue, double fuelMax, double healthMax, double armor, double weight, double shieldMax)
        {
            this.priceValue = priceValue;
            fuel = fuelMax;
            this.fuelMax = fuelMax;
            health = healthMax;
            this.healthMax = healthMax;
            this.armor = armor;
            this.weight = weight;
            shield = shieldMax;
            this.shieldMax = shieldMax;
        }

        public double GetPriceValue()
        {
            return priceValue;
        }
        public void SetPriceValue(double priceValue)
        {
            this.priceValue = priceValue;
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
        public double GetHealth()
        {
            return health;
        }
        public void SetHealth(double health)
        {
            this.health = health;
        }
        public double GetHealthMax()
        {
            return healthMax;
        }
        public double GetArmor()
        {
            return armor;
        }
        public double GetWeight()
        {
            return weight;
        }
        public double GetShield()
        {
            return shield;
        }
        public void SetShield(double shield)
        {
            this.shield = shield;
        }
        public double GetShieldMax()
        {
            return shieldMax;
        }
    }
}
