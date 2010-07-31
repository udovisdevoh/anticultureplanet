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
        private Bucket[,] bucketGrid;

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

            bucketGrid = new Bucket[columnCount, rowCount];
            for (int x = 0; x < columnCount; x++)
                for (int y = 0; y < rowCount; y++)
                    bucketGrid[x, y] = new Bucket();
        }
        #endregion

        #region Internal Methods
        /// <summary>
        /// Add entity to spatial hash table
        /// </summary>
        /// <param name="entity">entity to add</param>
        internal void Add(AbstractEntity entity)
        {
            entity.AddSpatialHashMovementListener(this);

            int leftBound = GetLeftBoundColumn(entity);
            int rightBound = GetRightBoundColumn(entity);
            int topBound = GetTopBoundRow(entity);
            int bottomBound = GetBottomBoundRow(entity);

            for (int x = leftBound; x <= rightBound; x++)
                for (int y = topBound; y <= bottomBound; y++)
                    bucketGrid[x, y].Add(entity);
        }

        /// <summary>
        /// Remove entity from spatial hash table
        /// </summary>
        /// <param name="entity">entity to remove</param>
        internal void Remove(AbstractEntity entity)
        {
            entity.ClearSpatialHashMovementListener();

            int leftBound = GetLeftBoundColumn(entity);
            int rightBound = GetRightBoundColumn(entity);
            int topBound = GetTopBoundRow(entity);
            int bottomBound = GetBottomBoundRow(entity);

            for (int x = leftBound; x <= rightBound; x++)
                for (int y = topBound; y <= bottomBound; y++)
                    if (bucketGrid[x,y].Contains(entity))
                        bucketGrid[x, y].Remove(entity);
        }

        /// <summary>
        /// Whether entity is in collision with another
        /// </summary>
        /// <param name="entity">entity</param>
        /// <returns>Whether entity is in collision with another</returns>
        internal bool IsDetectCollision(AbstractEntity entity)
        {
            int leftBound = GetLeftBoundColumn(entity);
            int rightBound = GetRightBoundColumn(entity);
            int topBound = GetTopBoundRow(entity);
            int bottomBound = GetBottomBoundRow(entity);

            for (int x = leftBound; x <= rightBound; x++)
                for (int y = topBound; y <= bottomBound; y++)
                    foreach (AbstractEntity otherEntity in bucketGrid[x, y])
                        if (otherEntity != entity)
                            if (IsDetectCollision(entity, otherEntity))
                                return true;

            return false;
        }

        /// <summary>
        /// Get random bucket
        /// </summary>
        /// <param name="random">random number generator</param>
        /// <returns>random bucket</returns>
        internal Bucket GetRandomBucket(Random random)
        {
            return bucketGrid[random.Next(columnCount), random.Next(rowCount)];
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Get bottom bound row for entity
        /// </summary>
        /// <param name="entity">entity</param>
        /// <returns>bottom bound row for entity</returns>
        private int GetBottomBoundRow(AbstractEntity entity)
        {
            int bottomBound = (int)Math.Ceiling((entity.Y + entity.Size / 2.0) / (double)bucketSize);
            while (bottomBound < 0) bottomBound += rowCount;
            while (bottomBound >= rowCount) bottomBound -= rowCount;
            return bottomBound;
        }

        /// <summary>
        /// Get top bound row for entity
        /// </summary>
        /// <param name="entity">entity</param>
        /// <returns>top bound row for entity</returns>
        private int GetTopBoundRow(AbstractEntity entity)
        {
            int topBound = (int)((entity.Y - entity.Size / 2.0) / (double)bucketSize);
            while (topBound < 0) topBound += rowCount;
            while (topBound >= rowCount) topBound -= rowCount;
            return topBound;
        }

        /// <summary>
        /// Get right bound row for entity
        /// </summary>
        /// <param name="entity">entity</param>
        /// <returns>right bound row for entity</returns>
        private int GetRightBoundColumn(AbstractEntity entity)
        {
            int rightBound = (int)Math.Ceiling((entity.X + entity.Size / 2.0) / (double)bucketSize);
            while (rightBound < 0) rightBound += columnCount;
            while (rightBound >= columnCount) rightBound -= columnCount;
            return rightBound;
        }

        /// <summary>
        /// Get left bound row for entity
        /// </summary>
        /// <param name="entity">entity</param>
        /// <returns>left bound row for entity</returns>
        private int GetLeftBoundColumn(AbstractEntity entity)
        {
            int leftBound = (int)((entity.X - entity.Size / 2.0) / (double)bucketSize);
            while (leftBound < 0) leftBound += columnCount;
            while (leftBound >= columnCount) leftBound -= columnCount;
            return leftBound;
        }

        /// <summary>
        /// Whether these entities are in collision
        /// </summary>
        /// <param name="entity1">entity 1</param>
        /// <param name="entity2">entity 2</param>
        /// <returns>Whether these entities are in collision</returns>
        private bool IsDetectCollision(AbstractEntity entity1, AbstractEntity entity2)
        {
            if (!entity1.IsAffectedByCollision || !entity2.IsAffectedByCollision)
                return false;

            double distanceFromCenter = GetDistance(entity1, entity2);
            return distanceFromCenter - entity1.Radius - entity2.Radius <= 0;
        }

        /// <summary>
        /// Gets the distance between 2 entities (1 = 1 tile height or width)
        /// </summary>
        /// <param name="entity1">entity 1</param>
        /// <param name="entity2">entity 2</param>
        /// <returns>distance between 2 entities (1 = 1 tile height or width)</returns>
        private double GetDistance(AbstractEntity entity1, AbstractEntity entity2)
        {
            double distanceX = Math.Abs(entity1.X - entity2.X);
            if (distanceX > totalWidth / 2)
                distanceX = totalWidth - distanceX;

            double distanceY = Math.Abs(entity1.Y - entity2.Y);
            if (distanceY > totalHeight / 2)
                distanceY = totalHeight - distanceY;

            return Math.Sqrt(Math.Pow(distanceX, 2) + Math.Pow(distanceY, 2));
        }
        #endregion

        #region Properties
        /// <summary>
        /// Get bucket at x,y
        /// </summary>
        /// <param name="x">x</param>
        /// <param name="y">y</param>
        /// <returns>bucket at x,y</returns>
        internal Bucket this[int x, int y]
        {
            get { return bucketGrid[x, y]; }
        }

        /// <summary>
        /// How many rows of buckets
        /// </summary>
        internal int RowCount
        {
            get { return rowCount; }
        }

        /// <summary>
        /// How many columns of buckets
        /// </summary>
        internal int ColumnCount
        {
            get { return columnCount; }
        }

        /// <summary>
        /// Bucket size
        /// </summary>
        public int BucketSize
        {
            get { return bucketSize; }
        }
        #endregion
    }
}
