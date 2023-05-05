using ProjectSpaceship.StellarObjects.InteractionPoints;

namespace ProjectSpaceship.StellarObjects
{
    internal class Station : StellarObject
    {
        private FuelStation fuelStation = new FuelStation();

        public Station(Coordinate coordinate, string owner) : base(coordinate)
        {
            this.owner = owner;
        }
        public FuelStation GetFuelstation()
        {
            return fuelStation;
        }
    }
}
