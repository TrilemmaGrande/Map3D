using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSpaceship.IngameResources
{
    internal class Gold : Resource
    {
        public Gold(double amount) : base(amount)
        {
            this.resourceType = "Gold";
            this.value = 5.0;
        }
    }
}
