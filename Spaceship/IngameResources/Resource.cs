namespace ProjectSpaceship.IngameResources
{
    internal class Resource
    {
        protected string name;
        protected double amount;
        protected double value;
        protected double weightPerAmount;

        public Resource(double amount)
        {
            this.amount = amount;
        }

        public string GetName()
        {
            return name;
        }
        public double GetAmount()
        {
            return amount;
        }
        public void SetAmount(double amount)
        {
            this.amount = amount;
        }
        public double GetValue()
        {
            return value;
        }
        public void SetValue(double value)
        {
            this.value = value;
        }
        public double GetWeightPerAmount()
        {
            return weightPerAmount;
        }
    }
}
