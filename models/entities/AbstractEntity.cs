﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace AntiCulturePlanet
{
    /// <summary>
    /// Represents entities on the planet (people, trees, animals, rocks, tools)
    /// </summary>
    internal abstract class AbstractEntity
    {
        #region Fields and Parts
        /// <summary>
        /// X position
        /// </summary>
        private double x;

        /// <summary>
        /// Y position
        /// </summary>
        private double y;

        /// <summary>
        /// Z position
        /// </summary>
        private double z;

        /// <summary>
        /// Mass
        /// </summary>
        private double mass;

        /// <summary>
        /// Size
        /// </summary>
        private double size;

        /// <summary>
        /// Decay Time (seconds)
        /// </summary>
        private double decayTime;

        /// <summary>
        /// Entity's sprite
        /// </summary>
        private EntitySprite entitySprite;

        /// <summary>
        /// Creation time
        /// </summary>
        private DateTime creationTime;

        /// <summary>
        /// Position criteria
        /// </summary>
        private PositionCriteria positionCriteria;
        #endregion

        #region Constructor
        /// <summary>
        /// Build abstract entity
        /// </summary>
        public AbstractEntity()
        {
            creationTime = DateTime.Now;
            mass = BuildMass();
            size = BuildSize();
            decayTime = BuildDecayTime();
            entitySprite = BuildEntitySprite();
            positionCriteria = BuildPositionCriteria();
        }
        #endregion

        #region Internal Methods
        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="currentTime">current time</param>
        /// <param name="planet">planet</param>
        /// <param name="entityCollection">entity collection</param>
        internal void Update(DateTime currentTime, Planet planet, EntityCollection entityCollection)
        {
            TimeSpan timeSpanSinceCreation = (TimeSpan)(currentTime - creationTime);

            if (timeSpanSinceCreation.TotalSeconds > decayTime)
            {
                Decay(planet, entityCollection);
            }
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Occurs when an entity decays
        /// </summary>
        /// <param name="planet">planet</param>
        /// <param name="entityCollection">entity collection</param>
        private void Decay(Planet planet, EntityCollection entityCollection)
        {
            entityCollection.Remove(this);
            IEnumerable<AbstractEntity> decayEntityList = GetDecayEntities(planet, entityCollection);
            foreach (AbstractEntity decayEntity in decayEntityList)
            {
                try
                {
                    int tryCount = 0;
                    do
                    {
                        PointF position = planet.GetRandomDecayPosition(this);
                        decayEntity.x = position.X;
                        decayEntity.y = position.Y;
                        tryCount++;
                        if (tryCount > Program.MaxTryFindRandomTilePosition)
                            throw new NoAvailableSpaceException();
                    } while (entityCollection.IsDetectCollision(decayEntity,planet));

                    entityCollection.Add(decayEntity);
                }
                catch (NoAvailableSpaceException)
                {
                    //There was no available space for new decay entity
                }
            }
        }
        #endregion

        #region Abstract Methods
        /// <summary>
        /// Build decay time
        /// </summary>
        /// <returns>Build decay time</returns>
        protected abstract double BuildDecayTime();

        /// <summary>
        /// Build size
        /// </summary>
        /// <returns>Build size</returns>
        protected abstract double BuildSize();

        /// <summary>
        /// Build mass
        /// </summary>
        /// <returns>Build mass</returns>
        protected abstract double BuildMass();

        /// <summary>
        /// Build sprite for entity
        /// </summary>
        /// <returns>sprite for entity</returns>
        protected abstract EntitySprite BuildEntitySprite();

        /// <summary>
        /// Occurs when entity decays, returns new entities
        /// </summary>
        /// <param name="planet">planet</param>
        /// <param name="entityCollection">entity collection</param>
        protected abstract IEnumerable<AbstractEntity> GetDecayEntities(Planet planet, EntityCollection entityCollection);

        /// <summary>
        /// Build position criteria (whether it's water, ground or both)
        /// </summary>
        /// <returns>position criteria</returns>
        protected abstract PositionCriteria BuildPositionCriteria();
        #endregion

        #region Properties
        /// <summary>
        /// X position
        /// </summary>
        internal double X
        {
            get { return x; }
            set { x = value; }
        }

        /// <summary>
        /// X position
        /// </summary>
        internal double Y
        {
            get { return y; }
            set { y = value; }
        }

        /// <summary>
        /// Z position
        /// </summary>
        internal double Z
        {
            get { return z; }
            set { z = value; }
        }

        /// <summary>
        /// Mass
        /// </summary>
        internal double Mass
        {
            get { return mass; }
            set { mass = value; }
        }

        /// <summary>
        /// Size
        /// </summary>
        internal double Size
        {
            get { return size; }
            set { size = value; }
        }

        /// <summary>
        /// Decay Time (seconds)
        /// </summary>
        internal double DecayTime
        {
            get { return decayTime; }
            set { decayTime = value; }
        }

        /// <summary>
        /// Entity's sprite definition
        /// </summary>
        internal EntitySprite EntitySprite
        {
            get { return entitySprite; }
        }

        /// <summary>
        /// Entity's radius
        /// </summary>
        internal double Radius
        {
            get { return size / 2.0; }
        }

        /// <summary>
        /// Position criteria (water, ground, both)
        /// </summary>
        internal PositionCriteria PositionCriteria
        {
            get { return positionCriteria; }
        }
        #endregion
    }
}