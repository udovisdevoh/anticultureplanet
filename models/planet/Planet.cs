using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AntiCulturePlanet
{
    /// <summary>
    /// Represents a planet on which prehistoric people live
    /// </summary>
    internal class Planet
    {
        #region Fields
        /// <summary>
        /// Planet's width
        /// </summary>
        private int width;

        /// <summary>
        /// Planet's height
        /// </summary>
        private int height;

        /// <summary>
        /// Temperature at pole (celsius)
        /// </summary>
        private int minDegree;

        /// <summary>
        /// Temperature at equator (celsius)
        /// </summary>
        private int maxDegree;

        /// <summary>
        /// How many seconds for a day
        /// </summary>
        private int dayLength;

        /// <summary>
        /// How many seconds for a year
        /// </summary>
        private int yearLength;

        /// <summary>
        /// Minimum altitude
        /// </summary>
        private int minAltitude;

        /// <summary>
        /// Maximum altitude
        /// </summary>
        private int maxAltitude;

        /// <summary>
        /// How many time we soften tiles
        /// </summary>
        private int softnessPassCount = 1;

        /// <summary>
        /// Percentage of water
        /// </summary>
        private float waterPercentage;
        #endregion

        #region Parts
        private Tile[,] tileGrid;
        #endregion

        #region Constructor
        /// <summary>
        /// Build a planet (don't use directly, use PlanetGenerator instead)
        /// </summary>
        /// <param name="width">width (tiles)</param>
        /// <param name="height">height (tiles)</param>
        /// <param name="dayLength">how many seconds for a day</param>
        /// <param name="yearLength">how many seconds for a year</param>
        /// <param name="minAltitude">minimum altitude</param>
        /// <param name="maxAltitude">maximum altitude</param>
        /// <param name="minDegree">min temperature (at pole)</param>
        /// <param name="maxDegree">max temperature (at equator)</param>
        /// <param name="waterPercentage">percentage of water</param>
        internal Planet(int width, int height, int minDegree, int maxDegree, int dayLength, int yearLength, int minAltitude, int maxAltitude, int softnessPassCount, float waterPercentage)
        {
            this.width = width;
            this.height = height;
            this.minDegree = minDegree;
            this.maxDegree = maxDegree;
            this.dayLength = dayLength;
            this.yearLength = yearLength;
            this.minAltitude = minAltitude;
            this.maxAltitude = maxAltitude;
            this.softnessPassCount = softnessPassCount;
            this.waterPercentage = waterPercentage;

            tileGrid = new Tile[width, height];
            for (int y = 0; y < height; y++)
                for (int x = 0; x < width; x++)
                    tileGrid[x, y] = new Tile();
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
        internal int MinDegree
        {
            get { return minDegree; }
            set { minDegree = value; }
        }

        /// <summary>
        /// Temperature at equator (celsius)
        /// </summary>
        internal int MaxDegree
        {
            get { return maxDegree; }
            set { maxDegree = value; }
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
        
        /// <summary>
        /// Tile at position
        /// </summary>
        /// <param name="x">x</param>
        /// <param name="y">y</param>
        /// <returns></returns>
        internal Tile this[int x, int y]
        {
            get
            {
                while (x < 0)
                    x += width;
                while (y < 0)
                    y += height;
                while (x > width)
                    x -= width;
                while (y > height)
                    y -= height;


            }
        }
        #endregion
    }
}
