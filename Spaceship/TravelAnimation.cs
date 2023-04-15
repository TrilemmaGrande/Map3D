using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spaceship
{
    internal class TravelAnimation
    {
        double travelTime;
        Coordinate positionInSector;
        Sector positionInWorld;

        public TravelAnimation(double travelTime, Coordinate positionInSector, Sector positionInWorld)
        {
            this.travelTime = travelTime;
            this.positionInSector = positionInSector;
            this.positionInWorld = positionInWorld;
        }
        public void StartTravelAnimation()
        {
            Console.Clear();
            for (int i = 0; i < travelTime; i++)
            {                
                PrintTravelAnimation();
                Thread.Sleep(33);
                Console.Clear();
            }
        }       
        public void PrintTravelAnimation()
        {
            string[] bottomLeft = { "  ", "  ", " /", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  " };
            string[] topLeft = { " `", "  ", " \\", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  " };
            string[] topRight = { " ´", "  ", " /", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  " };
            string[] bottomRight = { "  ", " \\", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  " };
            string[] middleHorizontal = { " _", "- ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  " };
            string[] middleVertikal = { " |", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  " };

            string newPositionInSector =
                                        $"{positionInSector.GetCoordinateX(),3}|" +
                                        $"{positionInSector.GetCoordinateY(),3}|" +
                                        $"{positionInSector.GetCoordinateZ(),3}";
            string newPositionInWorld =
                                        $"{positionInWorld.GetSectorCoordinate().GetCoordinateX(),3}|" +
                                        $"{positionInWorld.GetSectorCoordinate().GetCoordinateY(),3}|" +
                                        $"{positionInWorld.GetSectorCoordinate().GetCoordinateZ(),3}";

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
                    else
                    {
                        Console.Write("  ");
                    }
                    if (y == 1 && x == -5)
                    {
                        Console.Write(newPositionInWorld);
                    }
                    if (y == 0 && x == -5)
                    {
                        Console.Write(newPositionInSector);
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
