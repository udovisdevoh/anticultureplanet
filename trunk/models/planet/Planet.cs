using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace AntiCulturePlanet
{
    /// <summary>
    /// Represents a planet on which prehistoric people live
    /// </summary>
    internal class Planet
    {
        #region Parts
        /// <summary>
        /// Tile grid
        /// </summary>
        private Tile[,] tileGrid;

        /// <summary>
        /// Contains the entities on the planet
        /// </summary>
        private EntityCollection entityCollection;

        /// <summary>
        /// Random number generator
        /// </summary>
        private Random random;
        #endregion

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
        private int minTemperature;

        /// <summary>
        /// Temperature at equator (celsius)
        /// </summary>
        private int maxTemperature;

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
        /// Wather threshold altitude
        /// </summary>
        private int waterThresholdAltitude = 0;

        /// <summary>
        /// Altitude of water
        /// </summary>
        private float waterAltitude;

        /// <summary>
        /// Whether planet must be redrawn
        /// </summary>
        private bool isNeedRefresh = true;
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
        /// <param name="minTemperature">min temperature (at pole)</param>
        /// <param name="maxTemperature">max temperature (at equator)</param>
        /// <param name="waterPercentage">percentage of water</param>
        internal Planet(int width, int height, int minTemperature, int maxTemperature, int dayLength, int yearLength, int minAltitude, int maxAltitude, int softnessPassCount, float waterPercentage, Random random)
        {
            this.random = random;
            entityCollection = new EntityCollection(width,height);

            this.width = width;
            this.height = height;
            this.minTemperature = minTemperature;
            this.maxTemperature = maxTemperature;
            this.dayLength = dayLength;
            this.yearLength = yearLength;
            this.minAltitude = minAltitude;
            this.maxAltitude = maxAltitude;
            this.softnessPassCount = softnessPassCount;
            this.waterAltitude = waterPercentage;

            this.waterThresholdAltitude = (int)Math.Round((((((double)maxAltitude) - ((double)minAltitude)) * waterPercentage)) + minAltitude);

            tileGrid = new Tile[width, height];
            for (int y = 0; y < height; y++)
                for (int x = 0; x < width; x++)
                    tileGrid[x, y] = new Tile(x, y);
        }
        #endregion

        #region Internal Methods
        /// <summary>
        /// Tile at left
        /// </summary>
        /// <param name="tile">from tile</param>
        /// <returns>tile at left</returns>
        internal Tile GetLeftTile(Tile tile)
        {
            return this[tile.X - 1, tile.Y];
        }

        /// <summary>
        /// Tile at right
        /// </summary>
        /// <param name="tile">from tile</param>
        /// <returns>tile at right</returns>
        internal Tile GetRightTile(Tile tile)
        {
            return this[tile.X + 1, tile.Y];
        }

        /// <summary>
        /// Tile on top
        /// </summary>
        /// <param name="tile">from tile</param>
        /// <returns>tile on top</returns>
        internal Tile GetTopTile(Tile tile)
        {
            return this[tile.X, tile.Y  - 1];
        }

        /// <summary>
        /// Tile on bottom
        /// </summary>
        /// <param name="tile">from tile</param>
        /// <returns>tile on bottom</returns>
        internal Tile GetBottomTile(Tile tile)
        {
            return this[tile.X, tile.Y + 1];
        }

        /// <summary>
        /// Tile on bottom right
        /// </summary>
        /// <param name="tile">from tile</param>
        /// <returns>tile on bottom right</returns>
        internal Tile GetBottomRightTile(Tile tile)
        {
            return this[tile.X + 1, tile.Y + 1];
        }

        /// <summary>
        /// Tile on bottom left
        /// </summary>
        /// <param name="tile">from tile</param>
        /// <returns>tile on bottom left</returns>
        internal Tile GetBottomLeftTile(Tile tile)
        {
            return this[tile.X - 1, tile.Y + 1];
        }

        /// <summary>
        /// Tile on top left
        /// </summary>
        /// <param name="tile">from tile</param>
        /// <returns>tile on top left</returns>
        internal Tile GetTopLeftTile(Tile tile)
        {
            return this[tile.X - 1, tile.Y - 1];
        }

        /// <summary>
        /// Tile on top right
        /// </summary>
        /// <param name="tile">from tile</param>
        /// <returns>tile on top right</returns>
        internal Tile GetTopRightTile(Tile tile)
        {
            return this[tile.X + 1, tile.Y - 1];
        }

        /// <summary>
        /// Get random ground position
        /// throws OutOfMapSpaceException if no space is available
        /// </summary>
        /// <returns>random ground position</returns>
        internal PointF GetRandomGroundPosition()
        {
            int tryCount = 0;
            PointF point;
            do
            {
                point = GetRandomPosition();
                tryCount++;

                if (tryCount > Program.MaxTryFindRandomTilePosition)
                    throw new NoAvailableSpaceException();

            } while (this[(int)Math.Round(point.X), (int)Math.Round(point.Y)].IsWater);
            return point;
        }

        /// <summary>
        /// Get random water position
        /// throws OutOfMapSpaceException if no space is available
        /// </summary>
        /// <returns>random water position</returns>
        internal PointF GetRandomWaterPosition()
        {
            int tryCount = 0;
            PointF point;
            do
            {
                point = GetRandomPosition();
                tryCount++;

                if (tryCount > Program.MaxTryFindRandomTilePosition)
                    throw new NoAvailableSpaceException();

            } while (!this[(int)Math.Round(point.X), (int)Math.Round(point.Y)].IsWater);
            return point;
        }

        /// <summary>
        /// Random position
        /// </summary>
        /// <returns>Random position</returns>
        internal PointF GetRandomPosition()
        {
            return new PointF((float)(random.NextDouble() * width), (float)(random.NextDouble() * height));
        }

        /// <summary>
        /// Get random position for new entity to decay from old entity
        /// </summary>
        /// <param name="oldEntity">old entity</param>
        /// <returns>random position for new entity to decay from old entity</returns>
        internal PointF GetRandomDecayPosition(AbstractEntity oldEntity)
        {
            double xOffset = random.NextDouble() * oldEntity.Size - (oldEntity.Size / 2.0);
            double yOffset = random.NextDouble() * oldEntity.Size - (oldEntity.Size / 2.0);

            double xPosition = oldEntity.X - xOffset;
            double yPosition = oldEntity.Y - yOffset;

            while (xPosition < 0)
                xPosition += width;
            while (yPosition < 0)
                yPosition += height;

            while (xPosition >= width)
                xPosition -= width;
            while (yPosition >= height)
                yPosition -= height;

            return new PointF((float)xPosition, (float)yPosition);
        }

        /// <summary>
        /// Get random surrounding position for entity
        /// </summary>
        /// <param name="entity">entity</param>
        /// <returns>random surrounding position for entity</returns>
        internal PointF GetRandomSurroundingPosition(AbstractEntity entity)
        {
            double xOffset = random.NextDouble() * (entity.Size * 3.0) - (entity.Size * 1.5);
            double yOffset = random.NextDouble() * (entity.Size * 3.0) - (entity.Size * 1.5);

            double xPosition = entity.X - xOffset;
            double yPosition = entity.Y - yOffset;

            while (xPosition < 0)
                xPosition += width;
            while (yPosition < 0)
                yPosition += height;

            while (xPosition >= width)
                xPosition -= width;
            while (yPosition >= height)
                yPosition -= height;

            return new PointF((float)xPosition, (float)yPosition);
        }

        /// <summary>
        /// Return tile on which the entity is
        /// </summary>
        /// <param name="abstractEntity">entity</param>
        /// <returns>tile on which the entity is</returns>
        internal Tile GetTile(AbstractEntity abstractEntity)
        {
            return this[(int)Math.Round(abstractEntity.X), (int)Math.Round(abstractEntity.Y)];
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
        /// Wather threshold altitude
        /// </summary>
        internal int WaterThresholdAltitude
        {
            get { return waterThresholdAltitude; }
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
                while (x >= width)
                    x -= width;
                while (y >= height)
                    y -= height;

                return tileGrid[x, y];
            }
        }

        /// <summary>
        /// Whether planet must be redrawn
        /// </summary>
        public bool IsNeedRefresh
        {
            get { return isNeedRefresh; }
            set { isNeedRefresh = value; }
        }

        /// <summary>
        /// All the entities on planet
        /// </summary>
        public EntityCollection EntityCollection
        {
            get { return entityCollection; }
        }
        #endregion
    }
}
