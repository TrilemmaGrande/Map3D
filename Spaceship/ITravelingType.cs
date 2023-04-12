using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spaceship
{
    internal interface ITravelingType
    {
        public double CalcDistance(Coordinate spaceshipPositionInSector, Coordinate spaceshipPositionInWorld, Coordinate destination);
        public Coordinate CalcNewPositionInSector(Coordinate spaceshipPositionInSector, Coordinate destination);
        public Coordinate CalcNewPositionInWorld(Coordinate spaceshipPositionInWorld, Coordinate destination);
    }
}
