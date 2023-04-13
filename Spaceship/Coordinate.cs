﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spaceship
{
    class Coordinate
    {
        private int xCoord;
        private int yCoord;
        private int zCoord;

        public Coordinate(int x, int y, int z)
        {
            xCoord = x;
            yCoord = y;
            zCoord = z;
        }
        public int GetCoordinateX()
        {
            return xCoord;
        }
        public int GetCoordinateY()
        {
            return yCoord;
        }
        public int GetCoordinateZ()
        {
            return zCoord;
        }
        public string CoordinateToString()
        {
            return $"{xCoord,3}|{yCoord,3}|{zCoord,3}";
        }
        public void SetCoordinate(int x, int y, int z)
        {
            this.xCoord = x;
            this.yCoord = y;
            this.zCoord = z;
        }
    }
}
