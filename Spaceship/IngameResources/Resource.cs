namespace ProjectSpaceship.IngameResources
{
    internal class Resource
    {
        protected string resourceType;
        protected double amount;
        protected double value;
        protected double weightPerAmount;

        public Resource(double amount)
        {
            this.amount = amount;
        }

        public string GetResourceType()
        {
            return resourceType;
        }
        public double GetAmount()
        {
            return amount;
        }
        public void SetAmount(double amount)
        {
            this.amount = amount;
        }
        public void IncreaseAmount(double amountToAdd)
        {
            this.amount += amountToAdd;
        }
        public void DecreaseAmount(double amountToSub)
        {
            this.amount -= amountToSub;
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
