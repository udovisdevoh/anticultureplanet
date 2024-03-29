﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace AntiCulturePlanet
{
    enum ZIndexLayer { IsFloor, OnFloor, Air }

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
        /// Angle (radian)
        /// </summary>
        private double angleRadian;

        /// <summary>
        /// Size
        /// </summary>
        private double size;

        /// <summary>
        /// Decay Time (seconds)
        /// </summary>
        private double decayTime;

        /// <summary>
        /// Integrity
        /// </summary>
        private double integrity;

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

        /// <summary>
        /// Z Index layer
        /// </summary>
        private ZIndexLayer zIndexLayer;

        /// <summary>
        /// Whether we keep size of previous entity when this is the result of a decay
        /// </summary>
        private bool isKeepSizeOfPreviousEntity;

        /// <summary>
        /// Whether we keep sprite of previous entity when this is the result of a decay
        /// </summary>
        private bool isKeepSpriteOfPreviousEntity;

        /// <summary>
        /// Whether entity is affected by collisions
        /// </summary>
        private bool isAffectedByCollision;

        /// <summary>
        /// This object is warned when entity's position changes
        /// </summary>
        private SpatialHashTable spatialHashMovementListener = null;
        #endregion

        #region Constructor
        /// <summary>
        /// Build abstract entity
        /// </summary>
        public AbstractEntity()
        {
            creationTime = DateTime.Now;
            size = BuildSize();
            decayTime = BuildDecayTime();
            entitySprite = BuildEntitySprite();
            positionCriteria = BuildPositionCriteria();
            zIndexLayer = BuildZIndexLayer();
            isKeepSizeOfPreviousEntity = BuildIsKeepSizeOfPreviousEntity();
            isKeepSpriteOfPreviousEntity = BuildIsKeepSpriteOfPreviousEntity();
            isAffectedByCollision = BuildIsAffectedByCollision();
            integrity = size;
            angleRadian = 0;
        }
        #endregion

        #region Internal Methods
        /// <summary>
        /// Occurs when an entity decays
        /// </summary>
        /// <param name="planet">planet</param>
        internal void Decay(Planet planet)
        {
            planet.EntityCollection.Remove(this);
            IEnumerable<AbstractEntity> decayEntityList = GetDecayEntities(planet);
            if (decayEntityList != null)
            {
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
                        } while (planet.EntityCollection.IsDetectCollision(decayEntity));

                        if (decayEntity.IsKeepSizeOfPreviousEntity)
                            decayEntity.Size = this.Size;

                        if (decayEntity.IsKeepSpriteOfPreviousEntity)
                            decayEntity.EntitySprite = this.EntitySprite;

                        planet.EntityCollection.Add(decayEntity);
                    }
                    catch (NoAvailableSpaceException)
                    {
                        //There was no available space for new decay entity
                    }
                }
            }
        }

        /// <summary>
        /// Add spatial hash movement listener so spatial hash table is warned when entity moves
        /// </summary>
        /// <param name="spatialHashTable">spatial hash table</param>
        internal void AddSpatialHashMovementListener(SpatialHashTable spatialHashTable)
        {
            spatialHashMovementListener = spatialHashTable;
        }

        /// <summary>
        /// Remove spatial hash movement listener so spatial hash table stops being warned when entity moves
        /// </summary>
        internal void ClearSpatialHashMovementListener()
        {
            spatialHashMovementListener = null;
        }

        /// <summary>
        /// Move entity to specified position and warn spatial hash movement listener if entity is in spatial hash
        /// </summary>
        /// <param name="newX">new X position</param>
        /// /// <param name="newY">new Y position</param>
        internal void Move(double newX, double newY)
        {
            SpatialHashTable currentSpatialHashMovementListener = spatialHashMovementListener;
            if (currentSpatialHashMovementListener != null)
                currentSpatialHashMovementListener.Remove(this);
            this.x = newX;
            this.y = newY;
            if (currentSpatialHashMovementListener != null)
                currentSpatialHashMovementListener.Add(this);
        }
        #endregion

        #region Abstract Methods
        /// <summary>
        /// Build decay time (-1 for immortal items)
        /// </summary>
        /// <returns>Build decay time</returns>
        protected abstract double BuildDecayTime();

        /// <summary>
        /// Build size
        /// </summary>
        /// <returns>Build size</returns>
        protected abstract double BuildSize();

        /// <summary>
        /// Build sprite for entity
        /// </summary>
        /// <returns>sprite for entity</returns>
        protected abstract EntitySprite BuildEntitySprite();

        /// <summary>
        /// Occurs when entity decays, returns new entities
        /// can be null if decays into nothing
        /// </summary>
        /// <param name="planet">planet</param>
        /// <returns>when entity decays, returns new entities, or null if entity decays into nothing</returns>
        protected abstract IEnumerable<AbstractEntity> GetDecayEntities(Planet planet);

        /// <summary>
        /// Build position criteria (whether it's water, ground or both)
        /// </summary>
        /// <returns>position criteria</returns>
        protected abstract PositionCriteria BuildPositionCriteria();

        /// <summary>
        /// Build ZIndex layer
        /// </summary>
        /// <returns>ZIndex layer</returns>
        protected abstract ZIndexLayer BuildZIndexLayer();

        /// <summary>
        /// Build Is Keep sprite of previous entity
        /// </summary>
        /// <returns>Is Keep sprite of previous entity</returns>
        protected abstract bool BuildIsKeepSpriteOfPreviousEntity();

        /// <summary>
        /// Build Is Keep size of previous entity
        /// </summary>
        /// <returns>Is Keep size of previous entity</returns>
        protected abstract bool BuildIsKeepSizeOfPreviousEntity();

        /// <summary>
        /// Build Whether entity is affected by collision
        /// </summary>
        /// <returns>whether entity is affected by collision</returns>
        protected abstract bool BuildIsAffectedByCollision();
        #endregion

        #region Properties
        /// <summary>
        /// X position
        /// </summary>
        internal double X
        {
            get { return x; }
        }

        /// <summary>
        /// X position
        /// </summary>
        internal double Y
        {
            get { return y; }
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
            set { entitySprite = value; }
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

        /// <summary>
        /// ZIndex layer
        /// </summary>
        internal ZIndexLayer ZIndexLayer
        {
            get{return zIndexLayer;}
        }

        /// <summary>
        /// Whether we keep size of previous entity when this is the result of a decay
        /// </summary>
        internal bool IsKeepSizeOfPreviousEntity
        {
            get { return isKeepSizeOfPreviousEntity; }
        }

        /// <summary>
        /// Whether we keep sprite of previous entity when this is the result of a decay
        /// </summary>
        internal bool IsKeepSpriteOfPreviousEntity
        {
            get { return isKeepSpriteOfPreviousEntity; }
        }

        /// <summary>
        /// Whether entity is affected by collisions
        /// </summary>
        internal bool IsAffectedByCollision
        {
            get { return isAffectedByCollision; }
        }

        /// <summary>
        /// Creation time
        /// </summary>
        internal DateTime CreationTime
        {
            get { return creationTime; }
        }

        /// <summary>
        /// Angle (radian)
        /// </summary>
        internal double AngleRadian
        {
            get { return angleRadian; }
            set
            {
                while (value > Math.PI * 2)
                    value -= Math.PI * 2;

                while (value < 0)
                    value += Math.PI * 2;

                angleRadian = value;
            }
        }

        /// <summary>
        /// Angle (degree)
        /// </summary>
        internal double AngleDegree
        {
            get { return angleRadian / Math.PI * 180.0; }
            set { angleRadian = value / 180.0 * Math.PI; }
        }

        /// <summary>
        /// Entity's integrity
        /// </summary>
        internal double Integrity
        {
            get { return integrity; }
            set { integrity = value; }
        }
        #endregion
    }
}
