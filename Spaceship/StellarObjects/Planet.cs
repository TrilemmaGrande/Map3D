using ProjectSpaceship.IngameResources;
using ProjectSpaceship.StellarObjects.InteractionPoints;

namespace ProjectSpaceship.StellarObjects
{
    internal class Planet : StellarObject
    {
        private Merchant merchant = new Merchant();

        public Planet(Coordinate coordinate, string owner) : base(coordinate)
        {
            this.owner = owner;
        }
        public Merchant GetMerchant()
        {
            return merchant;
        }
    }
}
