﻿using System;
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
        #region Parts
        /// <summary>
        /// Random number generator
        /// </summary>
        private Random random;
        #endregion

        #region Fields
        /// <summary>
        /// Width (tiles)
        /// </summary>
        private int width = 512;

        /// <summary>
        /// Height (tiles)
        /// </summary>
        private int height = 512;

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
        /// How many river
        /// </summary>
        private int howManyRiver = 20;

        /// <summary>
        /// River length
        /// </summary>
        private int riverLength = 10000;

        /// <summary>
        /// How many time we soften tiles
        /// </summary>
        private int softnessPassCount = 5;

        /// <summary>
        /// Altitude of water (from 0 to 1)
        /// </summary>
        private float waterAltitude = 0.3f;
        #endregion

        #region Constructor
        /// <summary>
        /// Build planet generator
        /// </summary>
        /// <param name="random">random number generator</param>
        public PlanetGenerator(Random random)
        {
            this.random = random;
        }
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
        /// Altitude of water
        /// </summary>
        internal float WaterAltitude
        {
            get { return waterAltitude; }
            set { waterAltitude = value; }
        }

        /// <summary>
        /// How many river
        /// </summary>
        internal int HowManyRiver
        {
            get { return howManyRiver; }
            set { howManyRiver = value; }
        }

        /// <summary>
        /// River length
        /// </summary>
        private int RiverLength
        {
            get { return riverLength; }
            set { riverLength = value; }
        }
        #endregion

        #region Internal Methods
        /// <summary>
        /// Build a planet
        /// </summary>
        /// <returns>planet</returns>
        internal Planet Build()
        {
            Planet planet = new Planet(width, height, minTemperature, maxTemperature, dayLength, yearLength, minAltitude, maxAltitude, softnessPassCount, waterAltitude, random);
            
            //We randomize each tile
            for (int y = 0; y < width; y++)
                for (int x = 0; x < width; x++)
                    planet[x, y].Randomize(planet, random);

            //We draw some extra random water rivers
            int riverLengthRelative = (int)Math.Round(((double)riverLength) * ((double)width) * ((double)height) / 262144.0);
            for (int riverCount = 0; riverCount < howManyRiver; riverCount++)
            {
                int riverX = random.Next(width);
                int riverY = random.Next(height);
                for (int riverPosition = 0; riverPosition < riverLengthRelative; riverPosition++)
                {
                    planet[riverX, riverY].IsWater = true;
                    planet[riverX, riverY].Altitude = random.Next(minAltitude, planet.WaterThresholdAltitude);
                    riverX += random.Next(-1, 2);
                    riverY += random.Next(-1, 2);
                }
            }

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

            //We set water percentage
            for (int y = 0; y < width; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (planet[x, y].IsWater)
                    {
                        planet[x, y].WaterPercentage = 1.0;
                    }
                    else
                    {
                        planet[x, y].WaterPercentage = 0.001;
                    }
                }
            }

            //We soften water percentage
            for (int currentSoftnessPassCount = 0; currentSoftnessPassCount < softnessPassCount * 8; currentSoftnessPassCount++)
            {
                for (int y = 0; y < width; y += 2)
                    for (int x = 0; x < width; x += 2)
                        planet[x, y].SoftenWaterPercentage(planet);
                for (int y = 1; y < width; y += 2)
                    for (int x = 0; x < width; x += 2)
                        planet[x, y].SoftenWaterPercentage(planet);
                for (int y = 0; y < width; y += 2)
                    for (int x = 1; x < width; x += 2)
                        planet[x, y].SoftenWaterPercentage(planet);
                for (int y = 1; y < width; y += 2)
                    for (int x = 1; x < width; x += 2)
                        planet[x, y].SoftenWaterPercentage(planet);
            }

            //We set water percentage back to 1.0 when it's water, and we normalize it when it's not
            for (int y = 0; y < width; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (planet[x, y].IsWater)
                    {
                        planet[x, y].WaterPercentage = 1.0;
                    }
                    else
                    {
                        planet[x, y].WaterPercentage = Math.Pow(planet[x, y].WaterPercentage, 0.5);
                    }
                }
            }


            return planet;
        }
        #endregion
    }
}
