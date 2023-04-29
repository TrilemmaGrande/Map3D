using ProjectSpaceship.IngameResources;
using ProjectSpaceship.StellarObjects.InteractionPoints;

namespace ProjectSpaceship.StellarObjects
{
    internal class Planet : StellarObject
    {
        protected List<Resource> resources = new List<Resource>();
        protected Merchant merchant;
        protected FuelStation fuelStation = new FuelStation();

        public Planet(Coordinate coordinate, string owner) : base(coordinate)
        {
            type = "Planet";
            this.owner = owner;
        }
        public FuelStation GetFuelstation()
        {
            return fuelStation;
        }
    }
}
