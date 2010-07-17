using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AntiCulturePlanet
{
    /// <summary>
    /// Tile (part of a planet)
    /// </summary>
    internal class Tile
    {
        #region Fields
        /// <summary>
        /// Tile's altitude
        /// </summary>
        private int altitude = 0;

        /// <summary>
        /// Tile's temperature
        /// </summary>
        private int temperature = 15;

        /// <summary>
        /// Whether 
        /// </summary>
        private bool isWater = false;
        #endregion

        #region Internal Methods
        /// <summary>
        /// Soften tile's properties according to neighbors
        /// </summary>
        /// <param name="other1">other tile 1</param>
        /// <param name="other2">other tile 2</param>
        /// <param name="other3">other tile 3</param>
        /// <param name="other4">other tile 4</param>
        /// <param name="planet">planet</param>
        internal void Soften(Tile other1, Tile other2, Tile other3, Tile other4, Planet planet)
        {
            this.altitude = (int)Math.Round((float)(this.altitude + other1.altitude + other2.altitude + other3.altitude + other4.altitude) / 5.0);
            this.temperature = (int)Math.Round((float)(this.temperature + other1.temperature + other2.temperature + other3.temperature + other4.temperature) / 5.0);
            isWater = altitude < planet.WaterThresholdAltitude;
        }

        /// <summary>
        /// Randomize tile according to planet's settings
        /// </summary>
        /// <param name="planet">planet</param>
        /// <param name="random">random number generator</param>
        internal void Randomize(Planet planet, Random random)
        {
            altitude = random.Next(planet.MinAltitude, planet.MaxAltitude);
            temperature = random.Next(planet.MinTemperature, planet.MaxTemperature);
            isWater = altitude < planet.WaterThresholdAltitude;
        }
        #endregion
    }
}
