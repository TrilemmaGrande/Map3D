using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSpaceship
{
    internal class TravelingEventArgs : EventArgs
    {
        Coordinate coordinate;

        public TravelingEventArgs(Coordinate coordinate)
        {
            this.coordinate = coordinate;
        }
        public Coordinate GetCoordinate()
        {
            return coordinate;
        }
    }
}
