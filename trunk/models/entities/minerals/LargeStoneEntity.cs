using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AntiCulturePlanet
{
    /// <summary>
    /// Large stone entity
    /// </summary>
    class LargeStoneEntity : AbstractMineralEntity
    {
        #region Internal Methods
        /// <summary>
        /// Build size
        /// </summary>
        /// <returns>size</returns>
        protected override double BuildSize()
        {
            return 3;
        }

        protected override double BuildDefaultIntegrity()
        {
            return 20;
        }
        #endregion
    }
}