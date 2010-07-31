using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AntiCulturePlanet
{
    /// <summary>
    /// For optics physics
    /// </summary>
    static class Optics
    {
        /// <summary>
        /// Get radian angle from entity 1 to entity 2
        /// </summary>
        /// <param name="entity1">entity 1</param>
        /// <param name="entity2">entity 2</param>
        /// <returns>radian angle from entity 1 to entity 2</returns>
        internal static double GetAngleRadianTo(AbstractEntity entity1, AbstractEntity entity2)
        {
            double fullVectorX = entity2.X - entity1.X;
            double fullVectorY = entity2.Y - entity1.Y;
            return Math.Atan2(fullVectorY, fullVectorX);
        }
    }
}
