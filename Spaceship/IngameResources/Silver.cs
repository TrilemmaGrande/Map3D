using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSpaceship.IngameResources
{
    internal class Silver : Resource
    {
        public Silver(double amount) : base(amount)
        {
            this.resourceType = "Silver";
            this.value = 1.0;
        }
    }
}
