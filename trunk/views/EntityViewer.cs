using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AntiCulturePlanet
{
    /// <summary>
    /// To view entities
    /// </summary>
    abstract class EntityViewer
    {
        /// <summary>
        /// To view entities
        /// </summary>
        /// <param name="entityCollection">entities to view</param>
        internal abstract void Update(EntityCollection entityCollection);
    }
}
