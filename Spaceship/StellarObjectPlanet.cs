using Spaceship;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSpaceship
{
    internal class StellarObjectPlanet : StellarObject
    {
        protected List<Ressource> ressources = new List<Ressource>();
        protected Merchant merchant;
        protected FuelStation fuelStation = new FuelStation();

        public StellarObjectPlanet(Coordinate coordinate, string owner) : base(coordinate)
        {
            this.type = "Planet";
            this.owner = owner;
        }
        public FuelStation GetFuelstation()
        {
            return fuelStation;
        }
    }
}
