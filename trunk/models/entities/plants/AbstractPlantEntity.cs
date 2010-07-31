using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

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

        /// <summary>
        /// Minimum temperature needed for next growing stage
        /// </summary>
        private int minimumTemperatureForNextGrowingPhase;

        /// <summary>
        /// Reproduction cycle time (seconds)
        /// 0 = never
        /// </summary>
        private double reproductionCycleTime = 0;

        /// <summary>
        /// Last time entity reproduced
        /// </summary>
        private DateTime latestReproductionTime;
        #endregion

        #region Constructor
        /// <summary>
        /// Build abstract plant entity
        /// </summary>
        public AbstractPlantEntity() : base()
        {
            latestReproductionTime = DateTime.Now;
            minimumWaterPercentageOnTileForNextGrowingPhase = BuildMinimumWaterPercentageOnTileForNextGrowingPhase();
            minimumTemperatureForNextGrowingPhase = BuildMinimumTemperatureForNextGrowingPhase();
            reproductionCycleTime = BuildReproductionCycleTime();
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

        protected override bool BuildIsKeepSizeOfPreviousEntity()
        {
            return false;
        }

        protected override bool BuildIsAffectedByCollision()
        {
            return true;
        }

        /// <summary>
        /// Plant's sprite
        /// </summary>
        /// <returns>Plant's sprite</returns>
        protected override EntitySprite BuildEntitySprite()
        {
            return SpriteManager.GetSprite(this.GetType());
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
        /// Get reproduction spore for entity
        /// (null if there is no reproduction spore)
        /// </summary>
        /// <returns>reproduction spore for entity (null if there is no reproduction spore)</returns>
        protected abstract AbstractEntity GetReproductionSporeEntity();

        /// <summary>
        /// Build minimum water percentage (from 0 to 1) for next growing phase
        /// </summary>
        /// <returns>minimum water percentage (from 0 to 1) for next growing phase</returns>
        protected abstract double BuildMinimumWaterPercentageOnTileForNextGrowingPhase();

        /// <summary>
        /// Build minimum temperature needed for next growing stage
        /// </summary>
        /// <returns>minimum temperature needed for next growing stage</returns>
        protected abstract int BuildMinimumTemperatureForNextGrowingPhase();

        /// <summary>
        /// Build reproduction cycle time (seconds)
        /// </summary>
        /// <returns>reproduction cycle time (seconds) (0 = never)</returns>
        protected abstract double BuildReproductionCycleTime();
        #endregion

        #region Internal Methods
        /// <summary>
        /// Go to next phase or decay
        /// </summary>
        /// <param name="planet">Planet</param>
        internal void GoToNextPhaseOrDecay(Planet planet)
        {
            Tile tile = planet.GetTile(this);
            
            if (tile.WaterPercentage < minimumWaterPercentageOnTileForNextGrowingPhase
                || tile.Temperature < minimumTemperatureForNextGrowingPhase)
            {
                Decay(planet);
                return;
            }

            AbstractEntity nextPhaseEntity = GetNextGrowingPhaseEntity();
            if (nextPhaseEntity == null)
            {
                Decay(planet);
                return;
            }

            //Sometimes the next phase is incompatible with environment (water or ground)
            if (nextPhaseEntity.PositionCriteria == PositionCriteria.Ground && tile.IsWater
                || nextPhaseEntity.PositionCriteria == PositionCriteria.Water && !tile.IsWater)
            {
                Decay(planet);
                return;
            }

            nextPhaseEntity.Move(this.X, this.Y);

            if (nextPhaseEntity.IsKeepSizeOfPreviousEntity)
                nextPhaseEntity.Size = this.Size;

            if (nextPhaseEntity.IsKeepSpriteOfPreviousEntity)
                nextPhaseEntity.EntitySprite = this.EntitySprite;

            planet.EntityCollection.Remove(this);
            planet.EntityCollection.Add(nextPhaseEntity);
        }

        /// <summary>
        /// Engage reproduction for plant if possible
        /// </summary>
        /// <param name="planet">planet</param>
        /// <param name="currentTime">current time</param>
        internal void TryReproduce(Planet planet, DateTime currentTime)
        {
            if (reproductionCycleTime <= 0)
                return;

            TimeSpan timeSpanSinceLastReproduction = (TimeSpan)(currentTime - latestReproductionTime);

            if (timeSpanSinceLastReproduction.TotalSeconds >= reproductionCycleTime)
            {
                AbstractEntity reproductionSpore = GetReproductionSporeEntity();

                if (reproductionSpore != null)
                {
                    double sporeSize = reproductionSpore.Size;
                    reproductionSpore.Size = this.Size;

                    try
                    {
                        int tryCount = 0;
                        do
                        {
                            PointF position = planet.GetRandomSurroundingPosition(this);
                            reproductionSpore.Move(position.X, position.Y);
                            tryCount++;
                            if (tryCount > Program.MaxTryFindRandomTilePosition)
                                throw new NoAvailableSpaceException();
                        } while (planet.EntityCollection.IsDetectCollision(reproductionSpore));

                        reproductionSpore.Size = sporeSize;
                        planet.EntityCollection.Add(reproductionSpore);
                    }
                    catch (NoAvailableSpaceException)
                    {
                        //There was no available space for new decay entity
                    }
                }
                latestReproductionTime = currentTime;
            }
        }
        #endregion
    }
}
