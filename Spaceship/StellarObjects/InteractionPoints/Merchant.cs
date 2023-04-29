using ProjectSpaceship.IngameResources;

namespace ProjectSpaceship.StellarObjects.InteractionPoints
{
    internal class Merchant
    {
        private double credits;
        private List<Resource> resources = new List<Resource>();

        public Merchant()
        {
            Random rand = new Random();
            this.credits = rand.Next(1, 11) * 1000;
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
        public double GetCredits()
        {
            return credits;
        }
        public void SetCredits(double credits)
        {
            this.credits = credits;
        }
        public void IncreaseCredits(double amountToAdd)
        {
            this.credits += amountToAdd;
        }
        public void DecreaseCredits(double amountToSub)
        {
            this.credits -= amountToSub;
        }
        public bool ResourceListContains(Resource searchedResource)
        {
            foreach (Resource resource in resources)
            {
                if (searchedResource.GetResourceType() == resource.GetResourceType())
                {
                    return true;
                }
            }
            return false;
        }
        public Resource GetResourceFromResourceList(Resource searchedResource)
        {
            foreach (Resource resource in resources)
            {
                if (searchedResource.GetResourceType() == resource.GetResourceType())
                {
                    return resource;
                }
            }
            return null;
        }
        public void MergeAddResource(Resource resource)
        {
            if (ResourceListContains(resource))
            {
                GetResourceFromResourceList(resource).IncreaseAmount(resource.GetAmount());
            }
            else
            {
                this.resources.Add(resource);
            }
        }
        public void RemoveResource(Resource resource)
        {
            this.resources.Remove(resource);
        }
    }
}
