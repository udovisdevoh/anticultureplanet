using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AntiCulturePlanet
{
    /// <summary>
    /// Contains all the entities on a planet
    /// </summary>
    internal class EntityCollection : IEnumerable<AbstractEntity>
    {
        #region Fields and Parts
        /// <summary>
        /// Internal collection of entities
        /// </summary>
        private HashSet<AbstractEntity> internalCollection;

        /// <summary>
        /// To store relation between entity types and their respective amounts
        /// </summary>
        private Dictionary<Type, int> typeCount;
        #endregion

        #region Constructor
        /// <summary>
        /// Create entity collection
        /// </summary>
        public EntityCollection()
        {
            typeCount = new Dictionary<Type, int>();
            internalCollection = new HashSet<AbstractEntity>();

            Type type = internalCollection.GetType();
        }
        #endregion

        #region Internal methods
        /// <summary>
        /// Gets the distance between 2 entities (1 = 1 tile height or width)
        /// </summary>
        /// <param name="entity1">entity 1</param>
        /// <param name="entity2">entity 2</param>
        /// <returns>distance between 2 entities (1 = 1 tile height or width)</returns>
        internal double GetDistance(AbstractEntity entity1, AbstractEntity entity2, Planet planet)
        {
            double distanceX = Math.Abs(entity1.X - entity2.X);
            if (distanceX > planet.Width / 2)
                distanceX = planet.Width - distanceX;

            double distanceY = Math.Abs(entity1.Y - entity2.Y);
            if (distanceY > planet.Height / 2)
                distanceY = planet.Height - distanceY;

            return Math.Sqrt(Math.Pow(distanceX, 2) + Math.Pow(distanceY, 2));
        }

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
            internalCollection.Add(entity);
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
            if (!internalCollection.Contains(entity))
                return false;
            internalCollection.Remove(entity);
            Type type = entity.GetType();
            typeCount[type]--;
            return true;
        }

        /// <summary>
        /// Whether entity is in collision with other entity
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="planet">planet</param>
        /// <returns>Whether entity is in collision with other entity</returns>
        internal bool IsDetectCollision(AbstractEntity entity, Planet planet)
        {
            if (!entity.IsAffectedByCollision)
                return false;
            foreach (AbstractEntity otherEntity in internalCollection)
                if (IsDetectCollision(entity, otherEntity, planet))
                    return true;
            return false;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Whether entity is in collision with other entity
        /// </summary>
        /// <param name="entity1">entity 1</param>
        /// <param name="entity2">entity 2</param>
        /// <param name="planet">planet</param>
        /// <returns>Whether entity is in collision with other entity</returns>
        private bool IsDetectCollision(AbstractEntity entity1, AbstractEntity entity2, Planet planet)
        {
            if (!entity1.IsAffectedByCollision || !entity2.IsAffectedByCollision)
                return false;

            double distanceFromCenter = GetDistance(entity1, entity2, planet);
            return distanceFromCenter - entity1.Radius - entity2.Radius <= 0;
        }
        #endregion

        #region IEnumerable<AbstractEntity> Members
        public IEnumerator<AbstractEntity> GetEnumerator()
        {
            return internalCollection.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return internalCollection.GetEnumerator();
        }

        public void CopyTo(AbstractEntity[] array, int arrayIndex)
        {
            internalCollection.CopyTo(array, arrayIndex);
        }
        #endregion
    }
}
