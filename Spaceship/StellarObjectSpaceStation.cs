using Spaceship;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSpaceship
{
    internal class StellarObjectSpaceStation : StellarObject
    {        
        protected Merchant merchant;
        protected FuelStation fuelStation = new FuelStation();

        public StellarObjectSpaceStation(Coordinate coordinate, string owner) : base(coordinate)
        {
            this.type = "Spacestation";
            this.owner = owner;
        }
        public FuelStation GetFuelstation()
        {
            return fuelStation;
        }
    }
}
