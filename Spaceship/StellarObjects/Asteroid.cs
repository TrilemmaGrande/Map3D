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
            this.owner = owner;
            Random rand = new Random();
            CreateRandomResources(rand.Next(5), rand.Next(4));
        }
        private void CreateRandomResources(int amountOfResourceNodes, int amountOfResourceTypes)
        {
            Random rand = new Random();
            
            for (int i = 0; i < amountOfResourceNodes; i++)
            {
                Resource[] resourcesArray = { new Copper(rand.Next(10000)), new Silver(rand.Next(5000)), new Gold(rand.Next(1000)) };
                resources.Add(resourcesArray[rand.Next(amountOfResourceTypes)]);               
            }
        }
        public List<Resource> GetResourceList()
        {
            return resources;
        }
    }
}
