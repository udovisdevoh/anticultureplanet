using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AntiCulturePlanet
{
    /// <summary>
    /// Soil entity
    /// </summary>
    class SoilEntity : AbstractEntity
    {
        #region Internal Methods
        /// <summary>
        /// Build decay time
        /// </summary>
        /// <returns>Decay time</returns>
        protected override double BuildDecayTime()
        {
            return 1000.0;
        }

        /// <summary>
        /// Build size
        /// </summary>
        /// <returns>size</returns>
        protected override double BuildSize()
        {
            return 4;
        }

        /// <summary>
        /// Build mass
        /// </summary>
        /// <returns>mass</returns>
        protected override double BuildMass()
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

        /// <summary>
        /// Get decay entities when this entity decays
        /// can be null if decays into nothing
        /// </summary>
        /// <param name="planet">planet</param>
        /// <param name="entityCollection">entity collection</param>
        /// <returns>decay entities (when this entity decays)</returns>
        protected override IEnumerable<AbstractEntity> GetDecayEntities(Planet planet, EntityCollection entityCollection)
        {
            return null;
        }

        /// <summary>
        /// Position criteria
        /// </summary>
        /// <returns>Position criteria</returns>
        protected override PositionCriteria BuildPositionCriteria()
        {
            return PositionCriteria.Ground;
        }

        /// <summary>
        /// ZIndex layer
        /// </summary>
        /// <returns>ZIndex layer</returns>
        protected override ZIndexLayer BuildZIndexLayer()
        {
            return ZIndexLayer.IsFloor;
        }

        /// <summary>
        /// Build Is keep sprite of previous entity
        /// </summary>
        /// <returns>Is keep sprite of previous entity</returns>
        protected override bool BuildIsKeepSpriteOfPreviousEntity()
        {
            return false;
        }

        /// <summary>
        /// Build Is keep mass of previous entity
        /// </summary>
        /// <returns>Is keep mass of previous entity</returns>
        protected override bool BuildIsKeepMassOfPreviousEntity()
        {
            return false;
        }

        /// <summary>
        /// Build Is keep size of previous entity
        /// </summary>
        /// <returns>Is keep size of previous entity</returns>
        protected override bool BuildIsKeepSizeOfPreviousEntity()
        {
            return false;
        }

        /// <summary>
        /// Whether entity is affected by collisions
        /// </summary>
        /// <returns></returns>
        protected override bool BuildIsAffectedByCollision()
        {
            return false;
        }
        #endregion
    }
}
