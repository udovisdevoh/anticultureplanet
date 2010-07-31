using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AntiCulturePlanet
{
    /// <summary>
    /// Small stone entity
    /// </summary>
    class SmallStoneEntity : AbstractMineralEntity
    {
        #region Internal Methods
        /// <summary>
        /// Build size
        /// </summary>
        /// <returns>size</returns>
        protected override double BuildSize()
        {
            return 1;
        }

        /// <summary>
        /// Build entity sprite
        /// </summary>
        protected override EntitySprite BuildEntitySprite()
        {
            return SpriteManager.GetSprite(this.GetType());
        }

        protected override double BuildDefaultIntegrity()
        {
            return 5;
        }
        #endregion
    }
}
