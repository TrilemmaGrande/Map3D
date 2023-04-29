using ProjectSpaceship.IngameResources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSpaceship.Spaceships.Modules
{
    internal class Cargo : Module
    {
        private double cargoSpaceMax;
        private double cargoSpaceUsed;
        private List<Resource> resources = new List<Resource>();

        public Cargo(double priceValue, double cargoSpaceMax, double healthMax, double armor, double weight, double shieldMax) : base(priceValue, healthMax, armor, weight, shieldMax)
        {
            cargoSpaceUsed = 0.00;
            this.cargoSpaceMax = cargoSpaceMax;
        }
        public double GetCargoSpaceUsed()
        {
            return cargoSpaceUsed;
        }
        public void SetCargoSpaceUsed(double cargoSpaceUsed)
        {
            this.cargoSpaceUsed = cargoSpaceUsed;
        }
        public double GetCargoSpaceMax()
        {
            return cargoSpaceMax;
        }
        public List<Resource> GetResourceList()
        {
            return resources;
        }
        public void AddResource(Resource resource)
        {
            this.resources.Add(resource);
            cargoSpaceUsed += resource.GetAmount() * resource.GetWeightPerAmount();
        }
        public double GetLoadWeight()
        {
            double loadWeight = 0.00;
            foreach (Resource resource in resources)
            {
                loadWeight += resource.GetAmount() * resource.GetWeightPerAmount();
            }
            return loadWeight;
        }
    }
}
