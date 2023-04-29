namespace ProjectSpaceship.Travel
{
    internal class TravelAnimation
    {
        double travelTime;
        string newPositionInSector;
        string newPositionInWorld;
        Random rand = new Random();

        public TravelAnimation(double travelTime, Coordinate positionInSector, Coordinate positionInWorld)
        {
            this.travelTime = travelTime;
            newPositionInSector =
                                        $"{positionInSector.GetCoordinateX(),3}|" +
                                        $"{positionInSector.GetCoordinateY(),3}|" +
                                        $"{positionInSector.GetCoordinateZ(),3}";
            newPositionInWorld =
                                        $"{positionInWorld.GetCoordinateX(),3}|" +
                                        $"{positionInWorld.GetCoordinateY(),3}|" +
                                        $"{positionInWorld.GetCoordinateZ(),3}";
        }
        public void StartTravelAnimation()
        {
            Console.SetCursorPosition('\b', '\b');
            for (int i = 0; i < travelTime; i++)
            {
                PrintTravelAnimation();
                Console.SetCursorPosition('\b', '\b');
            }
            Console.Clear();
        }
        public void PrintTravelAnimation()
        {
            string[] bottomLeft = { "  ", "  ", " /", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  " };
            string[] topLeft = { " `", "  ", " \\", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  " };
            string[] topRight = { " ´", "  ", " /", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  " };
            string[] bottomRight = { "  ", " \\", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  " };
            string[] middleHorizontal = { " _", "- ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  " };
            string[] middleVertikal = { " |", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  " };

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
                    else
                    {
                        Console.Write("  ");
                    }
                    if (y == 1 && x == -10)
                    {
                        Console.Write("Sector:\t" + newPositionInWorld);
                    }
                    if (y == 0 && x == -10)
                    {
                        Console.Write("Pos:\t" + newPositionInSector);
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
