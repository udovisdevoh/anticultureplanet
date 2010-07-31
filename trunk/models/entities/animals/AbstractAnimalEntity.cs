using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AntiCulturePlanet
{
    /// <summary>
    /// Represents an animal (human, monkey, horse, snake)
    /// </summary>
    abstract class AbstractAnimalEntity : AbstractEntity
    {
        #region Override
        /// <summary>
        /// Return animal's corpse
        /// </summary>
        /// <param name="planet">planet</param>
        /// <returns>animal's corpse</returns>
        protected override IEnumerable<AbstractEntity> GetDecayEntities(Planet planet)
        {
            return new AbstractEntity[] { new CorpseEntity() };
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
            return ZIndexLayer.OnFloor;
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
            return true;
        }

        /// <summary>
        /// Animal's sprite
        /// </summary>
        /// <returns>Animal's sprite</returns>
        protected override EntitySprite BuildEntitySprite()
        {
            return SpriteManager.GetSprite(this.GetType());
        }
        #endregion

        #region Abstract
        /// <summary>
        /// Get list of entity type that can be eaten by this animal (null if none)
        /// </summary>
        /// <returns>list of entity type that can be eaten by this animal (null if none)</returns>
        public abstract IEnumerable<Type> GetPreyTypeList();

        /// <summary>
        /// Get list of entity type that can eat this animal (null if none)
        /// </summary>
        /// <returns>list of entity type that can eat this animal (null if none)</returns>
        public abstract IEnumerable<Type> GetPredatorTypeList();
        #endregion
    }
}