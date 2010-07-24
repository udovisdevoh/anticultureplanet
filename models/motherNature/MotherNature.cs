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
            entityRegulatorList.Add(new EntityRegulator(new LargeStoneEntity(), 0.001, 1, PositionCriteria.OnGround));
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
        #endregion
    }
}
