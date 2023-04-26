using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSpaceship
{
    internal class Position
    {
        private Sector sector;
        private Coordinate coordinate;

        public Position(Sector sector, Coordinate coordinate)
        {
            this.sector = sector;
            this.coordinate = coordinate;
        }
        public Sector GetSector()
        {
            return sector;
        }
        public Coordinate GetCoordinate()
        {
            return coordinate;
        }
        public void SetSector(Sector sector)
        {
            this.sector = sector;
        }
        public void SetCoordinate(Coordinate coordinate)
        {
            this.coordinate = coordinate;
        }
    }
}
