using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSpaceship.IngameResources
{
    internal class Copper : Resource
    {
        public Copper(double amount) : base(amount)
        {
            this.resourceType = "Copper";
            this.value = 0.5;
        }
    }
}
