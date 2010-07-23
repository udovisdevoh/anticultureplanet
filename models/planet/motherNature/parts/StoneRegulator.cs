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
    enum PositionCriteria {OnGround, OnWater, Anywhere};

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
        /// How many seconds before stones are updated
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
        /// <param name="positionCriteria">position criteria</param>
        /// <param name="minimumPercentage">minimum percentage</param>
        /// <param name="timeInterval">time interval (in seconds)</param>
        /// </summary>
        public EntityRegulator(AbstractEntity entity, double minimumPercentage, int timeInterval, PositionCriteria positionCriteria)
        {
            latestRefreshTime = DateTime.Now;
            this.positionCriteria = positionCriteria;
            this.minimumPercentage = minimumPercentage;
            this.timeInterval = timeInterval;
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
            if (howManySecondSpan >= timeInterval)
            {
                int minimumEntityCount = (int)Math.Round(minimumPercentage * (double)planet.Width * (double)planet.Height);
                if (entityCollection.CountType(entityType) < minimumEntityCount)
                {
                    Point point;

                    if (positionCriteria == PositionCriteria.OnGround)
                        point = planet.GetRandomGroundPosition();
                    else if (positionCriteria == PositionCriteria.OnWater)
                        point = planet.GetRandomWaterPosition();
                    else
                        point = planet.GetRandomPosition();

                    AbstractEntity entity = (AbstractEntity)Activator.CreateInstance(entityType);
                    entity.X = point.X;
                    entity.Y = point.Y;

                    entityCollection.Add(entity);

                    latestRefreshTime = DateTime.Now;
                }
            }
        }
        #endregion
    }
}