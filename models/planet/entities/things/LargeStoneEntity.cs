using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AntiCulturePlanet
{
    /// <summary>
    /// Large stone entity
    /// </summary>
    class LargeStoneEntity : AbstractEntity
    {
        #region Internal Methods
        /// <summary>
        /// Build decay time
        /// </summary>
        /// <returns>Decay time</returns>
        internal override double BuildDecayTime()
        {
            return 100.0;
        }

        /// <summary>
        /// Build size
        /// </summary>
        /// <returns>size</returns>
        internal override double BuildSize()
        {
            return 1.5;
        }

        /// <summary>
        /// Build mass
        /// </summary>
        /// <returns>mass</returns>
        internal override double BuildMass()
        {
            return 6.0;
        }
        #endregion
    }
}