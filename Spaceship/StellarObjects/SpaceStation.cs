using ProjectSpaceship.StellarObjects.InteractionPoints;

namespace ProjectSpaceship.StellarObjects
{
    internal class SpaceStation : StellarObject
    {
        private FuelStation fuelStation = new FuelStation();

        public SpaceStation(Coordinate coordinate, string owner) : base(coordinate)
        {
            type = "Spacestation";
            this.owner = owner;
        }
        public FuelStation GetFuelstation()
        {
            return fuelStation;
        }
    }
}
