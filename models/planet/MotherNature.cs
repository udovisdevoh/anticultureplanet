using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AntiCulturePlanet
{
    /// <summary>
    /// Regulates quantity of natural things
    /// </summary>
    class MotherNature
    {
        #region Parts
        /// <summary>
        /// List of entity regulators
        /// </summary>
        private List<EntityRegulator> entityRegulatorList;

        /// <summary>
        /// Last time decay was updated (globally)
        /// </summary>
        private DateTime lastDecayUpdateTime;

        /// <summary>
        /// Last time reproduction was updated (globally)
        /// </summary>
        private DateTime lastReproductionUpdateTime;
        #endregion

        #region Constructor
        /// <summary>
        /// Build a regulator for amount of natural stuff
        /// </summary>
        public MotherNature()
        {
            lastDecayUpdateTime = DateTime.Now;
            lastReproductionUpdateTime = DateTime.Now;
            entityRegulatorList = new List<EntityRegulator>();

            //Minerals
            entityRegulatorList.Add(new EntityRegulator(new LargeStoneEntity(), 0.001, 1));
            entityRegulatorList.Add(new EntityRegulator(new MediumStoneEntity(), 0.002, 1));
            entityRegulatorList.Add(new EntityRegulator(new SmallStoneEntity(), 0.004, 1));

            //Plants
            AbstractEntity[] otherKindFruitTree = { new SmallFruitTreeEntity(), new MediumFruitTreeEntity(), new LargeFruitTreeEntity() };
            entityRegulatorList.Add(new EntityRegulator(new SeedFruitTreeEntity(), otherKindFruitTree, 0.004, 1));
        }
        #endregion

        #region Internal Methods
        /// <summary>
        /// Update natural stuff
        /// </summary>
        /// <param name="planet">planet</param>
        /// <param name="currentTime">current time</param>
        internal void Update(Planet planet, DateTime currentTime)
        {
            foreach (EntityRegulator entityRegulator in entityRegulatorList)
            {
                entityRegulator.Update(planet, currentTime);
            }
        }

        /// <summary>
        /// Update entities for decay and phase transformation
        /// </summary>
        /// <param name="planet">planet</param>
        /// <param name="currentTime">current time</param>
        internal void UpdateForTransformations(Planet planet, DateTime currentTime)
        {
            TimeSpan timeSpanSinceLastDecayUpdate = (TimeSpan)(currentTime - lastDecayUpdateTime);
            if (timeSpanSinceLastDecayUpdate.Seconds * Program.SpeedMultiplier > Program.EntityTransformationRefreshTime)
            {
                foreach (AbstractEntity entity in new List<AbstractEntity>(planet.EntityCollection))
                {
                    if (entity.DecayTime < 0)//Some entites never decay
                        continue;

                    TimeSpan timeSpanSinceCreation = (TimeSpan)(currentTime - entity.CreationTime);
                    if (timeSpanSinceCreation.TotalSeconds * Program.SpeedMultiplier > entity.DecayTime)
                    {
                        if (entity is AbstractPlantEntity)
                        {
                            ((AbstractPlantEntity)(entity)).GoToNextPhaseOrDecay(planet);
                        }
                        else
                        {
                            entity.Decay(planet);
                        }
                    }
                }

                lastDecayUpdateTime = DateTime.Now;
            }
        }

        /// <summary>
        /// Update plants for reproduction
        /// </summary>
        /// <param name="planet">planet</param>
        /// <param name="currentTime">current time</param>
        internal void UpdatePlantsForReproduction(Planet planet, DateTime currentTime)
        {
            TimeSpan timeSpanSinceLastReproductionUpdate = (TimeSpan)(currentTime - lastReproductionUpdateTime);
            if (timeSpanSinceLastReproductionUpdate.Seconds * Program.SpeedMultiplier > Program.EntityTransformationRefreshTime)
            {
                foreach (AbstractEntity entity in new List<AbstractEntity>(planet.EntityCollection))
                {
                    if (entity is AbstractPlantEntity)
                    {
                        AbstractPlantEntity plant = (AbstractPlantEntity)entity;
                        plant.TryReproduce(planet, currentTime);
                    }
                }
                lastReproductionUpdateTime = DateTime.Now;
            }
        }
        #endregion
    }
}
