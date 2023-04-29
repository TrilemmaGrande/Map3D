using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSpaceship.Spaceships.Modules
{
    internal abstract class Module
    {
        protected double priceValue;
        protected double health;
        protected double healthMax;
        protected double armor;
        protected double weight;
        protected double shield;
        protected double shieldMax;

        public Module(double priceValue, double healthMax, double armor, double weight, double shieldMax)
        {
            this.priceValue = priceValue;
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
