using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AntiCulturePlanet
{
    /// <summary>
    /// Build planets
    /// </summary>
    internal class PlanetGenerator
    {
        #region Fields
        /// <summary>
        /// Width (tiles)
        /// </summary>
        private int width = 256;

        /// <summary>
        /// Height (tiles)
        /// </summary>
        private int height = 256;

        /// <summary>
        /// Temperature at pole (celsius)
        /// </summary>
        private int minTemperature = -10;

        /// <summary>
        /// Temperature at equator (celsius)
        /// </summary>
        private int maxTemperature = 50;

        /// <summary>
        /// How many seconds for a day
        /// </summary>
        private int dayLength = 300;

        /// <summary>
        /// How many seconds for a year
        /// </summary>
        private int yearLength = 3600;

        /// <summary>
        /// Minimum altitude
        /// </summary>
        private int minAltitude = -70;

        /// <summary>
        /// Maximum altitude
        /// </summary>
        private int maxAltitude = 70;

        /// <summary>
        /// How many time we soften tiles
        /// </summary>
        private int softnessPassCount = 5;

        /// <summary>
        /// Percentage of water
        /// </summary>
        private float waterPercentage = 0.45f;
        #endregion

        #region Properties
        /// <summary>
        /// Width (tiles)
        /// </summary>
        internal int Width
        {
            get { return width; }
            set { width = value; }
        }

        /// <summary>
        /// Height (tiles)
        /// </summary>
        internal int Height
        {
            get { return height; }
            set { height = value; }
        }

        /// <summary>
        /// Temperature at pole (celsius)
        /// </summary>
        internal int MinTemperature
        {
            get { return minTemperature; }
            set { minTemperature = value; }
        }

        /// <summary>
        /// Temperature at equator (celsius)
        /// </summary>
        internal int MaxTemperature
        {
            get { return maxTemperature; }
            set { maxTemperature = value; }
        }

        /// <summary>
        /// How many seconds for a day
        /// </summary>
        internal int DayLength
        {
            get { return dayLength; }
            set { dayLength = value; }
        }

        /// <summary>
        /// How many seconds for a year
        /// </summary>
        internal int YearLength
        {
            get { return yearLength; }
            set { yearLength = value; }
        }

        /// Minimum altitude
        internal int MinAltitude
        {
            get { return minAltitude; }
            set { minAltitude = value; }
        }

        /// Maximum altitude
        internal int MaxAltitude
        {
            get { return maxAltitude; }
            set { maxAltitude = value; }
        }

        /// <summary>
        /// Softness pass count (how many time we soften tiles)
        /// </summary>
        internal int SoftnessPassCount
        {
            get { return softnessPassCount; }
            set { softnessPassCount = value; }
        }

        /// <summary>
        /// Percentage of water
        /// </summary>
        internal float WaterPercentage
        {
            get { return waterPercentage; }
            set { waterPercentage = value; }
        }
        #endregion

        #region Internal Methods
        /// <summary>
        /// Build a planet
        /// </summary>
        /// <returns>planet</returns>
        internal Planet Build(Random random)
        {
            Planet planet = new Planet(width, height, minTemperature, maxTemperature, dayLength, yearLength, minAltitude, maxAltitude, softnessPassCount, waterPercentage);
            
            //We randomize each tile
            for (int y = 0; y < width; y++)
                for (int x = 0; x < width; x++)
                    planet[x, y].Randomize(planet, random);

            //We soften each tile
            for (int currentSoftnessPassCount = 0; currentSoftnessPassCount < softnessPassCount; currentSoftnessPassCount++)
            {
                for (int y = 0; y < width; y += 2)
                    for (int x = 0; x < width; x += 2)
                        planet[x, y].Soften(planet);
                for (int y = 1; y < width; y += 2)
                    for (int x = 0; x < width; x += 2)
                        planet[x, y].Soften(planet);
                for (int y = 0; y < width; y += 2)
                    for (int x = 1; x < width; x += 2)
                        planet[x, y].Soften(planet);
                for (int y = 1; y < width; y += 2)
                    for (int x = 1; x < width; x += 2)
                        planet[x, y].Soften(planet);
            }

            return planet;
        }
        #endregion
    }
}
