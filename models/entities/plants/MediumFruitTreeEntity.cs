using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AntiCulturePlanet
{
    /// <summary>
    /// Represents a small tree
    /// </summary>
    class MediumFruitTreeEntity : AbstractPlantEntity
    {
        protected override double BuildDecayTime()
        {
            return 200;
        }

        protected override double BuildSize()
        {
            return 2;
        }

        protected override double BuildMass()
        {
            return 1;
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
            return new LargeFruitTreeEntity();
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
            return 4;
        }

        protected override AbstractEntity GetReproductionSporeEntity()
        {
            return null;
        }

        /// <summary>
        /// Build reproduction cycle time (seconds)
        /// </summary>
        /// <returns>reproduction cycle time (seconds) (0 = never)</returns>
        protected override double BuildReproductionCycleTime()
        {
            return 0;
        }
    }
}