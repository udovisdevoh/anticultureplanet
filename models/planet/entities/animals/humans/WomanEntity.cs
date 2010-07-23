using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AntiCulturePlanet
{
    /// <summary>
    /// Represents a woman
    /// </summary>
    class WomanEntity : AbstractHumanEntity
    {
        #region Internal Methods
        /// <summary>
        /// Build decay time
        /// </summary>
        /// <returns>Decay time</returns>
        internal override double BuildDecayTime()
        {
            return 1.1;
        }

        /// <summary>
        /// Build size
        /// </summary>
        /// <returns>size</returns>
        internal override double BuildSize()
        {
            return 0.9;
        }

        /// <summary>
        /// Build mass
        /// </summary>
        /// <returns>mass</returns>
        internal override double BuildMass()
        {
            return 0.9;
        }
        #endregion
    }
}
