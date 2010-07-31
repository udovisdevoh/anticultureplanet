﻿using System;
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