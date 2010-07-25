using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AntiCulturePlanet
{
    /// <summary>
    /// Represents seeds, plants and trees
    /// </summary>
    abstract class AbstractPlantEntity : AbstractEntity
    {
        #region Fields
        /// <summary>
        /// Minimum water percentage on tile for next growing phase
        /// </summary>
        private double minimumWaterPercentageOnTileForNextGrowingPhase;
        #endregion

        #region Constructor
        /// <summary>
        /// Build abstract plant entity
        /// </summary>
        public AbstractPlantEntity() : base()
        {
            minimumWaterPercentageOnTileForNextGrowingPhase = BuildMinimumWaterPercentageOnTileForNextGrowingPhase();
        }
        #endregion

        #region Override Methods
        protected override PositionCriteria BuildPositionCriteria()
        {
            return PositionCriteria.Ground;
        }

        protected override ZIndexLayer BuildZIndexLayer()
        {
            return ZIndexLayer.OnFloor;
        }

        protected override bool BuildIsKeepSpriteOfPreviousEntity()
        {
            return false;
        }

        protected override bool BuildIsKeepMassOfPreviousEntity()
        {
            return false;
        }

        protected override bool BuildIsKeepSizeOfPreviousEntity()
        {
            return false;
        }

        protected override bool BuildIsAffectedByCollision()
        {
            return true;
        }
        #endregion

        #region Abstract Methods
        /// <summary>
        /// Get next phase entity for plant
        /// (null if there is no next phase)
        /// </summary>
        /// <returns>next phase entity for plant (or null if there is no next phase)</returns>
        protected abstract AbstractEntity GetNextGrowingPhaseEntity();

        /// <summary>
        /// Build minimum water percentage (from 0 to 1) for next growing phase
        /// </summary>
        /// <returns>minimum water percentage (from 0 to 1) for next growing phase</returns>
        protected abstract double BuildMinimumWaterPercentageOnTileForNextGrowingPhase();
        #endregion

        #region Internal Methods
        /// <summary>
        /// Go to next phase or decay
        /// </summary>
        /// <param name="planet">Planet</param>
        /// <param name="entityCollection">Entity Collection</param>
        internal void GoToNextPhaseOrDecay(Planet planet, EntityCollection entityCollection)
        {
            if (planet.GetTile(this).WaterPercentage < this.minimumWaterPercentageOnTileForNextGrowingPhase)
            {
                Decay(planet, entityCollection);
                return;
            }

            AbstractEntity nextPhaseEntity = GetNextGrowingPhaseEntity();
            if (nextPhaseEntity == null)
            {
                Decay(planet, entityCollection);
                return;
            }

            nextPhaseEntity.X = this.X;
            nextPhaseEntity.Y = this.Y;

            if (nextPhaseEntity.IsKeepMassOfPreviousEntity)
                nextPhaseEntity.Mass = this.Mass;

            if (nextPhaseEntity.IsKeepSizeOfPreviousEntity)
                nextPhaseEntity.Size = this.Size;

            if (nextPhaseEntity.IsKeepSpriteOfPreviousEntity)
                nextPhaseEntity.EntitySprite = this.EntitySprite;

            entityCollection.Remove(this);
            entityCollection.Add(nextPhaseEntity);
        }
        #endregion
    }
}
