using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace AntiCulturePlanet
{
    /// <summary>
    /// Possible positions for entities to appear
    /// </summary>
    enum PositionCriteria {Ground, Water, Anywhere};

    /// <summary>
    /// Regulates the amount of stones
    /// </summary>
    class EntityRegulator
    {
        #region Fields and Parts
        /// <summary>
        /// Large stone type
        /// </summary>
        private Type entityType;

        /// <summary>
        /// Minimum percentage of stones
        /// </summary>
        private double minimumPercentage = 0.001;

        /// <summary>
        /// How many seconds before entities are regulated
        /// </summary>
        private int timeInterval = 1;

        /// <summary>
        /// On ground, water or both
        /// </summary>
        private PositionCriteria positionCriteria;

        /// <summary>
        /// Latest refresh time
        /// </summary>
        private DateTime latestRefreshTime;
        #endregion

        #region Constructor
        /// <summary>
        /// Entity regulator
        /// <param name="entity">entity for type</param>
        /// <param name="minimumPercentage">minimum percentage</param>
        /// <param name="timeInterval">time interval (in seconds)</param>
        /// </summary>
        public EntityRegulator(AbstractEntity entity, double minimumPercentage, int timeInterval)
        {
            latestRefreshTime = DateTime.Now;
            this.positionCriteria = entity.PositionCriteria;
            this.minimumPercentage = minimumPercentage;
            this.timeInterval = timeInterval;
            this.positionCriteria = entity.PositionCriteria;
            entityType = entity.GetType();
        }
        #endregion

        #region Internal Methods
        /// <summary>
        /// Regulates the amount of stones
        /// </summary>
        /// <param name="entityCollection">entity collection</param>
        /// <param name="planet">planet</param>
        /// <param name="currentTime">current time</param>
        internal void Update(EntityCollection entityCollection, Planet planet, DateTime currentTime)
        {
            int howManySecondSpan = ((TimeSpan)(currentTime - latestRefreshTime)).Seconds;
            if (howManySecondSpan * Program.SpeedMultiplier >= timeInterval)
            {
                //We create new entities when there are not enough of them
                try
                {
                    int minimumEntityCount = (int)Math.Round(minimumPercentage * (double)planet.Width * (double)planet.Height);
                    if (entityCollection.CountType(entityType) < minimumEntityCount)
                    {
                        PointF pointF;

                        AbstractEntity entity = (AbstractEntity)Activator.CreateInstance(entityType);

                        int tryCount = 0;
                        do
                        {
                            if (positionCriteria == PositionCriteria.Ground)
                                pointF = planet.GetRandomGroundPosition();
                            else if (positionCriteria == PositionCriteria.Water)
                                pointF = planet.GetRandomWaterPosition();
                            else
                                pointF = planet.GetRandomPosition();

                            entity.X = pointF.X;
                            entity.Y = pointF.Y;

                            if (tryCount > Program.MaxTryFindRandomTilePosition)
                                throw new NoAvailableSpaceException();

                            tryCount++;

                        } while (entityCollection.IsDetectCollision(entity, planet));

                        entityCollection.Add(entity);

                        latestRefreshTime = DateTime.Now;
                    }
                }
                catch (NoAvailableSpaceException)
                {
                    //We couldn't add more entities because there was too many collisions
                }
            }
        }
        #endregion
    }
}