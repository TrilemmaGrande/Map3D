using ProjectSpaceship.IngameResources;
using ProjectSpaceship.StellarObjects.InteractionPoints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSpaceship.StellarObjects
{
    internal class Asteroid : StellarObject
    {
        private List<Resource> resources = new List<Resource>();
        public Asteroid(Coordinate coordinate, string owner) : base(coordinate)
        {
            type = "Asteroid";
            this.owner = owner;


        }
    }
}
