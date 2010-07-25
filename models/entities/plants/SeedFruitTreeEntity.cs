using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AntiCulturePlanet
{
    class SeedFruitTreeEntity : AbstractPlantEntity
    {
        #region Internal Methods
        /// <summary>
        /// Build decay time
        /// </summary>
        /// <returns>Decay time</returns>
        protected override double BuildDecayTime()
        {
            return 30;
        }

        /// <summary>
        /// Build size
        /// </summary>
        /// <returns>size</returns>
        protected override double BuildSize()
        {
            return 0.5;
        }

        /// <summary>
        /// Build mass
        /// </summary>
        /// <returns>mass</returns>
        protected override double BuildMass()
        {
            return 0.125;
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
        /// </summary>
        /// <param name="planet">planet</param>
        /// <param name="entityCollection">entity collection</param>
        /// <returns>decay entities (when this entity decays)</returns>
        protected override IEnumerable<AbstractEntity> GetDecayEntities(Planet planet, EntityCollection entityCollection)
        {
            return null;
        }

        /// <summary>
        /// Get next phase entity for plant
        /// (null if there is no next phase)
        /// </summary>
        /// <returns>next phase entity for plant (or null if there is no next phase)</returns>
        protected override AbstractEntity GetNextGrowingPhaseEntity()
        {
            return new SmallFruitTreeEntity();
        }

        /// <summary>
        /// Build minimum water percentage (from 0 to 1) for next growing phase
        /// </summary>
        /// <returns>minimum water percentage (from 0 to 1) for next growing phase</returns>
        protected override double BuildMinimumWaterPercentageOnTileForNextGrowingPhase()
        {
            #warning Adjust
            return 0.00003125;
        }
        #endregion
    }
}
