namespace Spaceship
{
    class Travel
    {
        private Coordinate travelDestination;
        private double travelTime;
        private double travelDistance;
        private double speed;
        private double fuelConsumption;

        public Travel(Coordinate travelDestination, Coordinate spaceshipPositionInSector, Coordinate spaceshipPositionInWorld, double fuelConsumption, double speed)
        {
            this.travelDestination = travelDestination;
            travelDistance = CalcDistance(spaceshipPositionInSector, travelDestination);
            travelTime = travelDistance / speed;
            this.speed = speed;
            this.fuelConsumption = fuelConsumption;
        }
        public double CalcDistance(Coordinate start, Coordinate destination)
        {
            return Math.Sqrt(
                 Math.Pow(start.GetCoordinateX() - destination.GetCoordinateX(), 2) +
                 Math.Pow(start.GetCoordinateY() - destination.GetCoordinateY(), 2) +
                 Math.Pow(start.GetCoordinateZ() - destination.GetCoordinateZ(), 2));
        }
        public double CalcFuelConsumption()
        {
            return travelDistance / speed * fuelConsumption;
        }
        public Coordinate TravelWithAnimation()
        {
            for (int i = 0; i < travelTime; i++)
            {
                Console.Clear();
                TravelAnimation();
                Thread.Sleep(33);
            }
            return travelDestination;
        }
        public void TravelAnimation()
        {
            string[] bottomLeft = { "  ", "  ", " /", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  " };
            string[] topLeft = { " `", "  ", " \\", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  " };
            string[] topRight = { " ´", "  ", " /", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  " };
            string[] bottomRight = { "  ", " \\", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  " };
            string[] middleHorizontal = { " _", "- ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  " };
            string[] middleVertikal = { " |", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  " };

            Random rand = new Random();

            for (int y = 20; y >= -20; y--)
            {
                for (int x = -20; x < 20; x++)
                {
                    if (y < -5 && x < -5)
                    {
                        Console.Write(bottomLeft[rand.Next(0, 15)]);
                    }
                    else if (y > 5 && x < -5)
                    {
                        Console.Write(topLeft[rand.Next(0, 15)]);
                    }
                    else if (y > 5 && x > 5)
                    {
                        Console.Write(topRight[rand.Next(0, 15)]);
                    }
                    else if (y < -5 && x > 5)
                    {
                        Console.Write(bottomRight[rand.Next(0, 15)]);
                    }
                    else if ((x < -5 || x > 5) && y < 5 && y > -5 && y != 0)
                    {
                        Console.Write(middleHorizontal[rand.Next(0, 15)]);
                    }
                    else if ((y < -5 || y > 5) && x > -5 && x < 5)
                    {
                        Console.Write(middleVertikal[rand.Next(0, 15)]);
                    }
                    else if (y > -5 && y < 5 && x > -5 && x < 5)
                    {
                        Console.Write("  ");
                    }
                    if (y == 0 && x == 15)
                    {
                        Console.Write(
                                 $"{travelDestination.GetCoordinateX(),17}|" +
                                 $"{travelDestination.GetCoordinateY(),3}|" +
                                 $"{travelDestination.GetCoordinateZ(),3}");
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
