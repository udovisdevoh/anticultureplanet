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
        #endregion

        #region Constructor
        /// <summary>
        /// Build a regulator for amount of natural stuff
        /// </summary>
        public MotherNature()
        {
            lastDecayUpdateTime = DateTime.Now;
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
        /// <param name="entityCollection">entity collection</param>
        /// <param name="planet">planet</param>
        /// <param name="currentTime">current time</param>
        internal void Update(EntityCollection entityCollection, Planet planet, DateTime currentTime)
        {
            foreach (EntityRegulator entityRegulator in entityRegulatorList)
            {
                entityRegulator.Update(entityCollection, planet, currentTime);
            }
        }

        /// <summary>
        /// Update entities for decay and phase transformation
        /// </summary>
        /// <param name="entityCollection">entity collection</param>
        /// <param name="planet">planet</param>
        /// <param name="currentTime">current time</param>
        internal void UpdateForTransformations(EntityCollection entityCollection, Planet planet, DateTime currentTime)
        {
            TimeSpan timeSpanSinceLastDecayUpdate = (TimeSpan)(currentTime - lastDecayUpdateTime);
            if (timeSpanSinceLastDecayUpdate.Seconds * Program.SpeedMultiplier > Program.DecayRefreshTime)
            {
                foreach (AbstractEntity entity in new List<AbstractEntity>(entityCollection))
                {
                    if (entity.DecayTime < 0)//Some entites never decay
                        continue;

                    TimeSpan timeSpanSinceCreation = (TimeSpan)(currentTime - entity.CreationTime);
                    if (timeSpanSinceCreation.TotalSeconds * Program.SpeedMultiplier > entity.DecayTime)
                    {
                        if (entity is AbstractPlantEntity)
                        {
                            ((AbstractPlantEntity)(entity)).GoToNextPhaseOrDecay(planet, entityCollection);
                        }
                        else
                        {
                            entity.Decay(planet, entityCollection);
                        }
                    }
                }

                lastDecayUpdateTime = DateTime.Now;
            }
        }
        #endregion
    }
}
