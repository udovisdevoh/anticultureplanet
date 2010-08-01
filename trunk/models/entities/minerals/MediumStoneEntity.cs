using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AntiCulturePlanet
{
    /// <summary>
    /// Medium stone entity
    /// </summary>
    class MediumStoneEntity : AbstractMineralEntity
    {
        #region Internal Methods
        /// <summary>
        /// Build size
        /// </summary>
        /// <returns>size</returns>
        protected override double BuildSize()
        {
            return 2;
        }
        #endregion
    }
}