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
        /// X coordinate
        /// </summary>
        private int x;

        /// <summary>
        /// Y coordinate
        /// </summary>
        private int y;

        /// <summary>
        /// Tile's altitude
        /// </summary>
        private int altitude = 0;

        /// <summary>
        /// Tile's temperature
        /// </summary>
        private int temperature = 15;

        /// <summary>
        /// Whether tile is water or not
        /// </summary>
        private bool isWater = false;

        /// <summary>
        /// Whether tile must be redrawn
        /// </summary>
        private bool isNeedRefresh = true;
        #endregion

        #region Constructor
        /// <summary>
        /// Create tile
        /// </summary>
        /// <param name="x">x coordinate</param>
        /// <param name="y">y coordinate</param>
        internal Tile(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        #endregion

        #region Internal Methods
        /// <summary>
        /// Soften tile's properties according to neighbors
        /// </summary>
        /// <param name="planet">planet (to get neighbor tiles)</param>
        internal void Soften(Planet planet)
        {
            this.altitude = (int)Math.Round((float)(this.altitude + planet.GetLeftTile(this).altitude + planet.GetRightTile(this).altitude + planet.GetTopTile(this).altitude + planet.GetBottomTile(this).altitude) / 5.0);
            this.temperature = (int)Math.Round((float)(this.temperature + planet.GetLeftTile(this).temperature + planet.GetRightTile(this).temperature + planet.GetTopTile(this).temperature + planet.GetBottomTile(this).temperature) / 5.0);
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

        #region Properties
        /// <summary>
        /// Tile's X coordinate
        /// </summary>
        public int X
        {
            get { return x; }
            set { x = value; }
        }

        /// <summary>
        /// Tile's Y coordinate
        /// </summary>
        public int Y
        {
            get { return y; }
            set { y = value; }
        }

        /// <summary>
        /// Tile's altitude
        /// </summary>
        public int Altitude
        {
            get { return altitude; }
            set { altitude = value; }
        }

        /// <summary>
        /// Tile's temperature
        /// </summary>
        public int Temperature
        {
            get { return temperature; }
            set { temperature = value; }
        }

        /// <summary>
        /// Whether tile is water or not
        /// </summary>
        public bool IsWater
        {
            get { return isWater; }
            set { isWater = value; }
        }

        /// <summary>
        /// Whether tile must be redrawn
        /// </summary>
        public bool IsNeedRefresh
        {
            get { return isNeedRefresh; }
            set { isNeedRefresh = value; }
        }
        #endregion
    }
}
