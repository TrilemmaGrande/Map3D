using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spaceship
{
    internal interface ITravelingType
    {        
        public double CalcDistance(Traveling traveling);
        public Coordinate CalcNewPositionInSector(Traveling traveling));
        public Sector CalcNewPositionInWorld(Traveling traveling));
        public void TravelWithAnimation(Traveling traveling));
    }
}
