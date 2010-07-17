using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AntiCulturePlanet
{
    class Program
    {
        static void Main(string[] args)
        {
            PlanetGenerator planetGenerator = new PlanetGenerator();
            Planet planet = planetGenerator.Build(new Random());
        }
    }
}
