using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AntiCulturePlanet
{
    /// <summary>
    /// Represents a small tree
    /// </summary>
    class SmallFruitTreeEntity : AbstractPlantEntity
    {
        protected override double BuildDecayTime()
        {
            return 50;
        }

        protected override double BuildSize()
        {
            return 1;
        }

        protected override double BuildMass()
        {
            return 0.25;
        }

        protected override EntitySprite BuildEntitySprite()
        {
            return SpriteManager.GetSprite(this.GetType());
        }

        protected override IEnumerable<AbstractEntity> GetDecayEntities(Planet planet, EntityCollection entityCollection)
        {
            return new AbstractEntity[] { new TrunkEntity() };
        }

        /// <summary>
        /// Get next phase entity for plant
        /// (null if there is no next phase)
        /// </summary>
        /// <returns>next phase entity for plant (or null if there is no next phase)</returns>
        protected override AbstractEntity GetNextGrowingPhaseEntity()
        {
            #warning Implement
            return null;
        }

        /// <summary>
        /// Build minimum water percentage (from 0 to 1) for next growing phase
        /// </summary>
        /// <returns>minimum water percentage (from 0 to 1) for next growing phase</returns>
        protected override double BuildMinimumWaterPercentageOnTileForNextGrowingPhase()
        {
            #warning Adjust
            return 0.0000625;
        }

        protected override int BuildMinimumTemperatureForNextGrowingPhase()
        {
            return 4;
        }
    }
}