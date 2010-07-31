using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AntiCulturePlanet
{
    /// <summary>
    /// Large pine tree
    /// </summary>
    class LargePineTreeEntity : AbstractPlantEntity
    {
        protected override double BuildDecayTime()
        {
            return 1000;
        }

        protected override double BuildSize()
        {
            return 3;
        }

        protected override IEnumerable<AbstractEntity> GetDecayEntities(Planet planet)
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
            return null;
        }

        /// <summary>
        /// Build minimum water percentage (from 0 to 1) for next growing phase
        /// </summary>
        /// <returns>minimum water percentage (from 0 to 1) for next growing phase</returns>
        protected override double BuildMinimumWaterPercentageOnTileForNextGrowingPhase()
        {
            return 0.032;
        }

        protected override int BuildMinimumTemperatureForNextGrowingPhase()
        {
            return -6;
        }

        /// <summary>
        /// Get reproduction spore for entity
        /// (null if there is no reproduction spore)
        /// </summary>
        /// <returns>reproduction spore for entity (null if there is no reproduction spore)</returns>
        protected override AbstractEntity GetReproductionSporeEntity()
        {
            return new SeedPineTreeEntity();
        }

        /// <summary>
        /// Build reproduction cycle time (seconds)
        /// </summary>
        /// <returns>reproduction cycle time (seconds) (0 = never)</returns>
        protected override double BuildReproductionCycleTime()
        {
            return 10;
        }

        protected override double BuildDefaultIntegrity()
        {
            return 15;
        }
    }
}
