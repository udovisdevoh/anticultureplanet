﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AntiCulturePlanet
{
    /// <summary>
    /// Contains all the entities on a planet
    /// </summary>
    internal class EntityCollection
    {
        #region Fields and Parts
        /// <summary>
        /// To store relation between entity types and their respective amounts
        /// </summary>
        private Dictionary<Type, int> typeCount;

        /// <summary>
        /// Spatial hash table
        /// </summary>
        private SpatialHashTable spatialHashTable;
        #endregion

        #region Constructor
        /// <summary>
        /// Create entity collection
        /// </summary>
        /// <param name="width">planet's width</param>
        /// <param name="height">planet's height</param>
        public EntityCollection(int width, int height)
        {
            spatialHashTable = new SpatialHashTable(width, height, 5);
            typeCount = new Dictionary<Type, int>();
        }
        #endregion

        #region Internal methods
        /// <summary>
        /// Count the amount of entity of specified type
        /// </summary>
        /// <param name="type">entity type</param>
        /// <returns>amount of entity of specified type</returns>
        internal int CountType(Type type)
        {
            int count = 0;
            typeCount.TryGetValue(type, out count);
            return count;
        }

        /// <summary>
        /// Add entity
        /// </summary>
        /// <param name="entity">entity to add</param>
        internal void Add(AbstractEntity entity)
        {
            spatialHashTable.Add(entity);
            int typeCountValue = 0;
            Type type = entity.GetType();
            if (typeCount.TryGetValue(type, out typeCountValue))
                typeCount[type]++;
            else
                typeCount.Add(type, 1);
        }

        /// <summary>
        /// Remove entity from entity collection
        /// </summary>
        /// <param name="entity">entity to remove</param>
        internal bool Remove(AbstractEntity entity)
        {
            spatialHashTable.Remove(entity);
            Type type = entity.GetType();
            typeCount[type]--;
            return true;
        }

        /// <summary>
        /// Whether entity is in collision with other entity
        /// </summary>
        /// <param name="entity">entity</param>
        /// <returns>Whether entity is in collision with other entity</returns>
        internal bool IsDetectCollision(AbstractEntity entity)
        {
            if (!entity.IsAffectedByCollision)
                return false;
            return spatialHashTable.IsDetectCollision(entity);
        }

        /// <summary>
        /// Get random bucket
        /// </summary>
        /// <param name="random">random number generator</param>
        /// <returns>random bucket</returns>
        internal Bucket GetRandomBucket(Random random)
        {
            return spatialHashTable.GetRandomBucket(random);
        }
        #endregion

        #region Properties
        /// <summary>
        /// Spatial hash table
        /// </summary>
        public SpatialHashTable SpatialHashTable
        {
            get { return spatialHashTable; }
        }
        #endregion
    }
}