using ProjectSpaceship.IngameResources;
using ProjectSpaceship.StellarObjects.InteractionPoints;

namespace ProjectSpaceship.StellarObjects
{
    internal class Planet : StellarObject
    {
        private Merchant merchant;

        public Planet(Coordinate coordinate, string owner) : base(coordinate)
        {
            type = "Planet";
            this.owner = owner;
        }
    }
}
