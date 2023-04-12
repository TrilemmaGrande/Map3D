namespace Spaceship
{
    class Traveling
    {
        private Coordinate destination;
        private Coordinate spaceshipPositionInSector;
        private Coordinate spaceshipPositionInWorld;
        private ITravelingType travelingType;
        private double travelTime;
        private double travelDistance;
        private double speed;
        private double fuelConsumption;
        public Traveling(ITravelingType travelingType, double fuelConsumption, double speed, Coordinate spaceshipPositionInSector, Coordinate spaceshipPositionInWorld, Coordinate destination)
        {
            this.speed = speed;
            this.travelingType = travelingType;
            this.fuelConsumption = fuelConsumption;
            this.spaceshipPositionInWorld = spaceshipPositionInWorld;
            this.spaceshipPositionInSector = spaceshipPositionInSector;
            travelDistance = travelingType.CalcDistance(spaceshipPositionInSector, spaceshipPositionInWorld, destination);
            travelTime = travelDistance / speed;
        }
        public double CalcFuelConsumption()
        {
            Console.WriteLine($"Traveldistance: {travelDistance}");
            return travelDistance / speed * fuelConsumption;
        }
        public void TravelWithAnimation()
        {
            for (int i = 0; i < travelTime; i++)
            {
                Console.Clear();
                TravelAnimation();
                Thread.Sleep(33);
            }
        }
        public Coordinate GetNewPositionInSector()
        {
            return travelingType.CalcNewPositionInSector(spaceshipPositionInSector, destination);
        }
        public Coordinate GetNewPositionInWorld()
        {
            return travelingType.CalcNewPositionInWorld(spaceshipPositionInWorld, destination);
        }
        public void TravelAnimation()
        {
            string[] bottomLeft =       { "  ", "  ", " /",  "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  " };
            string[] topLeft =          { " `", "  ", " \\", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  " };
            string[] topRight =         { " ´", "  ", " /",  "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  " };
            string[] bottomRight =      { "  ", " \\", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  " };
            string[] middleHorizontal = { " _", "- ", "  ",  "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  " };
            string[] middleVertikal =   { " |", "  ", "  ",  "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  " };
            string newPositionInSector = 
                                        $"{GetNewPositionInSector().GetCoordinateX(),17}|" +
                                        $"{GetNewPositionInSector().GetCoordinateY(),3}|" +
                                        $"{GetNewPositionInSector().GetCoordinateZ(),3}";
            string newPositionInWorld = 
                                        $"{GetNewPositionInWorld().GetCoordinateX(),17}|" +
                                        $"{GetNewPositionInWorld().GetCoordinateY(),3}|" +
                                        $"{GetNewPositionInWorld().GetCoordinateZ(),3}";

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
                    if (y == 1 && x == 15)
                    {
                        Console.Write(newPositionInWorld);
                    }
                    if (y == 0 && x == 15)
                    {
                        Console.Write(newPositionInSector);
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
