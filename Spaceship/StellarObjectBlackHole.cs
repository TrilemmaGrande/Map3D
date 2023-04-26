
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSpaceship
{
    internal class StellarObjectBlackHole : StellarObject
    {
        public StellarObjectBlackHole(Coordinate coordinate, string owner) : base(coordinate)
        {
            this.type = "Black Hole";
            this.owner = owner;
        }
    }
}
