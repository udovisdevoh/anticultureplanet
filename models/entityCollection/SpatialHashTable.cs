using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace AntiCulturePlanet
{
    /// <summary>
    /// Spatial hash table
    /// </summary>
    class SpatialHashTable
    {
        #region Fields and Parts
        /// <summary>
        /// Bucket grid
        /// </summary>
        private HashSet<AbstractEntity>[,] bucketGrid;

        /// <summary>
        /// Total width
        /// </summary>
        private int totalWidth;

        /// <summary>
        /// Total height
        /// </summary>
        private int totalHeight;

        /// <summary>
        /// Row count
        /// </summary>
        private int rowCount;

        /// <summary>
        /// Column count
        /// </summary>
        private int columnCount;

        /// <summary>
        /// Bucket size
        /// </summary>
        private int bucketSize;
        #endregion

        #region Constructor
        /// <summary>
        /// Create spatial hash table
        /// </summary>
        /// <param name="totalWidth">total width</param>
        /// <param name="totalHeight">total height</param>
        /// <param name="bucketSize">bucket size</param>
        public SpatialHashTable(int totalWidth, int totalHeight, int bucketSize)
        {
            this.totalWidth = totalWidth;
            this.totalHeight = totalHeight;
            this.bucketSize = bucketSize;

            columnCount = (int)Math.Floor(((double)totalWidth) / ((double)bucketSize));
            rowCount = (int)Math.Floor(((double)totalHeight) / ((double)bucketSize));

            bucketGrid = new HashSet<AbstractEntity>[columnCount, rowCount];
            for (int x = 0; x < columnCount; x++)
                for (int y = 0; y < rowCount; y++)
                    bucketGrid[x, y] = new HashSet<AbstractEntity>();
        }
        #endregion

        #region Internal Methods
        /// <summary>
        /// Add entity to spatial hash table
        /// </summary>
        /// <param name="entity">entity to add</param>
        internal void Add(AbstractEntity entity)
        {
            int leftBound = (int)(entity.X / bucketSize);
            int topBound = (int)(entity.Y / bucketSize);
            int rightBound = (int)((entity.X + entity.Size) / (double)bucketSize);
            int bottomBound = (int)((entity.Y + entity.Size) / (double)bucketSize);

            while (leftBound < 0)
                leftBound += columnCount;

            while (rightBound >= columnCount)
                rightBound -= columnCount;

            while (topBound < 0)
                topBound += rowCount;

            while (bottomBound >= rowCount)
                bottomBound -= rowCount;

            for (int x = leftBound; x <= rightBound; x++)
                for (int y = topBound; y <= bottomBound; y++)
                    bucketGrid[x, y].Add(entity);
        }
        #endregion
    }
}
