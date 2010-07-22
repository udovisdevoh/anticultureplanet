using System;
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
        internal abstract float GetDistance(AbstractEntity entity1, AbstractEntity entity2);

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
        #endregion
    }
}
