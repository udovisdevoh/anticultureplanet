using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AntiCulturePlanet
{
    /// <summary>
    /// Represents a man
    /// </summary>
    class ManEntity : AbstractHumanEntity
    {
        #region Internal Methods
        /// <summary>
        /// Build decay time
        /// </summary>
        /// <returns>Decay time</returns>
        internal override double BuildDecayTime()
        {
            return 1.0;
        }

        /// <summary>
        /// Build size
        /// </summary>
        /// <returns>size</returns>
        internal override double BuildSize()
        {
            return 2.0;
        }

        /// <summary>
        /// Build mass
        /// </summary>
        /// <returns>mass</returns>
        internal override double BuildMass()
        {
            return 1.0;
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
