namespace ProjectSpaceship.Spaceships
{
    internal class SpaceshipEngine
    {
        private double priceValue;
        private double speed;
        private double speedMax;
        private double health;
        private double healthMax;
        private double armor;
        private double weight;
        private double shield;
        private double shieldMax;

        public SpaceshipEngine(double priceValue, double speedMax, double healthMax, double armor, double weight, double shieldMax)
        {
            this.priceValue = priceValue;
            speed = 0;
            this.speedMax = speedMax;
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
        public double GetSpeed()
        {
            return speed;
        }
        public void SetSpeed(double speed)
        {
            this.speed = speed;
        }
        public double GetSpeedMax()
        {
            return speedMax;
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
