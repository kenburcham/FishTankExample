using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishTankExample
{
   
    class Rock : AquariumObject
    {
        public Rock(string name) : base(name) {}
    }

    class Plant : AquariumObject
    {
        public Plant(string name) : base(name) { }
    }
   
}
