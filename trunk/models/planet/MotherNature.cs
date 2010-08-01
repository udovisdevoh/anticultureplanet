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
        #endregion

        #region Constructor
        /// <summary>
        /// Build a regulator for amount of natural stuff
        /// </summary>
        public MotherNature()
        {
            entityRegulatorList = new List<EntityRegulator>();

            //Minerals
            entityRegulatorList.Add(new EntityRegulator(new LargeStoneEntity(), 0.0005, 1));
            entityRegulatorList.Add(new EntityRegulator(new MediumStoneEntity(), 0.001, 1));
            entityRegulatorList.Add(new EntityRegulator(new SmallStoneEntity(), 0.002, 1));

            //Plants
            AbstractEntity[] otherKindFruitTree = { new SmallFruitTreeEntity(), new MediumFruitTreeEntity(), new LargeFruitTreeEntity() };
            entityRegulatorList.Add(new EntityRegulator(new SeedFruitTreeEntity(), otherKindFruitTree, 0.001, 1));

            AbstractEntity[] otherKindPineTree = { new SmallPineTreeEntity(), new MediumPineTreeEntity(), new LargePineTreeEntity() };
            entityRegulatorList.Add(new EntityRegulator(new SeedPineTreeEntity(), otherKindPineTree, 0.0005, 1));

            AbstractEntity[] otherKindCocoTree = { new SmallCocoTreeEntity(), new MediumCocoTreeEntity(), new LargeCocoTreeEntity() };
            entityRegulatorList.Add(new EntityRegulator(new SeedCocoTreeEntity(), otherKindCocoTree, 0.0005, 1));

            AbstractEntity[] otherKindPlant = { new PlantEntity() };
            entityRegulatorList.Add(new EntityRegulator(new SeedPlantEntity(), otherKindPlant, 0.0005, 1));

            //Animals
            entityRegulatorList.Add(new EntityRegulator(new RatEntity(), 0.0001, 1));
            entityRegulatorList.Add(new EntityRegulator(new BeaverEntity(), 0.0001, 1));
            entityRegulatorList.Add(new EntityRegulator(new TigerEntity(), 0.0001, 1));
        }
        #endregion

        #region Internal Methods
        /// <summary>
        /// Update population regulator
        /// </summary>
        /// <param name="planet">planet</param>
        /// <param name="currentTime">current time</param>
        internal void UpdatePopulationRegualtor(Planet planet, DateTime currentTime)
        {
            foreach (EntityRegulator entityRegulator in entityRegulatorList)
            {
                entityRegulator.Update(planet, currentTime);
            }
        }
        #endregion
    }
}
