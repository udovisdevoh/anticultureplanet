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
            return 1.8;
        }

        /// <summary>
        /// Build mass
        /// </summary>
        /// <returns>mass</returns>
        internal override double BuildMass()
        {
            return 0.9;
        }

        /// <summary>
        /// Build entity sprite
        /// </summary>
        /// <returns>entity sprite</returns>
        internal override EntitySprite BuildEntitySprite()
        {
            return SpriteManager.GetSprite(this.GetType());
        }
        #endregion
    }
}
