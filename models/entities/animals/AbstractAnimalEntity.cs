using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace AntiCulturePlanet
{
    /// <summary>
    /// Represents an animal (human, monkey, horse, snake)
    /// </summary>
    abstract class AbstractAnimalEntity : AbstractEntity
    {
        #region Fields
        /// <summary>
        /// Latest reproduction time
        /// </summary>
        private DateTime latestReproductionTime;

        /// <summary>
        /// Reproduction cycle time
        /// </summary>
        private double reproductionCycleTime;

        /// <summary>
        /// Size at birth
        /// </summary>
        private double sizeAtBirth;

        /// <summary>
        /// List of possible preys
        /// </summary>
        private HashSet<Type> preyTypeList;

        /// <summary>
        /// List of possible predators
        /// </summary>
        private HashSet<Type> predatorTypeList;

        /// <summary>
        /// Minimum size for reproduction
        /// </summary>
        private double minimumSizeForReproduction;

        /// <summary>
        /// Minimum food reserve for reproduction
        /// </summary>
        private double minimumFoodReserveForReproduction;

        /// <summary>
        /// Minimum food reserve for growth
        /// </summary>
        private double minimumFoodReserveForGrowth;

        /// <summary>
        /// Maximum size
        /// </summary>
        private double maximumSize;

        /// <summary>
        /// Maximum food reserve
        /// </summary>
        private double foodReserve;

        /// <summary>
        /// Maximum food reserve
        /// </summary>
        private double maximumFoodReserve;

        /// <summary>
        /// Growth rate
        /// </summary>
        private double growthRate;

        /// <summary>
        /// Speed
        /// </summary>
        private double speed;

        /// <summary>
        /// Entity's viewing range
        /// </summary>
        private double viewRangeRadius;

        /// <summary>
        /// How much decrement of prey integrity by eating shot
        /// </summary>
        private double eatingRate;
        #endregion

        #region Constructor
        /// <summary>
        /// Build abstract animal entity
        /// </summary>
        public AbstractAnimalEntity() : base()
        {
            latestReproductionTime = DateTime.Now;
            reproductionCycleTime = BuildReproductionCycleTime();
            preyTypeList = BuildPreyTypeList();
            predatorTypeList = BuildPredatorTypeList();
            sizeAtBirth = BuildSizeAtBirth();
            minimumSizeForReproduction = BuildMinimumSizeForReproduction();
            minimumFoodReserveForReproduction = BuildMinimumFoodReserveForReproduction();
            maximumSize = BuildMaximumSize();
            maximumFoodReserve = BuildMaximumFoodReserve();
            growthRate = BuildGrowthRate();
            minimumFoodReserveForGrowth = BuildMinimumFoodReserveForGrowth();
            speed = BuildSpeed();
            viewRangeRadius = BuildViewRangeRadius();
            eatingRate = BuildEatingRate();
            AngleDegree = 90;
            Size = sizeAtBirth;
            foodReserve = Size;
        }
        #endregion

        #region Internal Methods
        /// <summary>
        /// Try make animal reproduce
        /// </summary>
        /// <param name="planet">planet</param>
        /// <param name="currentTime">current time</param>
        internal bool TryReproduce(Planet planet, DateTime currentTime)
        {
            bool isReproduce = false;
            if (reproductionCycleTime <= 0)
                return isReproduce;

            if (Size < minimumSizeForReproduction)
                return isReproduce;

            if (foodReserve < minimumFoodReserveForReproduction)
                return isReproduce;

            TimeSpan timeSpanSinceLastReproduction = (TimeSpan)(currentTime - latestReproductionTime);

            if (timeSpanSinceLastReproduction.TotalSeconds >= reproductionCycleTime)
            {
                AbstractAnimalEntity offspring = GetOffspringEntity();

                if (offspring != null)
                {
                    double offspringSize = offspring.sizeAtBirth;
                    offspring.Size = this.Size;

                    try
                    {
                        int tryCount = 0;
                        do
                        {
                            PointF position = planet.GetRandomSurroundingPosition(this);
                            offspring.Move(position.X, position.Y);
                            tryCount++;
                            if (tryCount > Program.MaxTryFindRandomTilePosition)
                                throw new NoAvailableSpaceException();
                        } while (planet.EntityCollection.IsDetectCollision(offspring));

                        offspring.Size = offspringSize;
                        offspring.foodReserve *= .2;
                        planet.EntityCollection.Add(offspring);
                        isReproduce = true;
                    }
                    catch (NoAvailableSpaceException)
                    {
                        //There was no available space for new decay entity
                    }
                }
                latestReproductionTime = currentTime;
            }
            return isReproduce;
        }

        /// <summary>
        /// Try make entity grow if enough food reserves and not too big
        /// </summary>
        /// <param name="planet">planet</param>
        internal bool TryGrow(Planet planet)
        {
            bool isGrow = false;
            if (Size < maximumSize)
            {
                if (foodReserve >= minimumFoodReserveForGrowth)
                {
                    double oldSize = this.Size;
                    
                    Size = (Math.Min(Size * growthRate, maximumSize));

                    planet.EntityCollection.SpatialHashTable.Remove(this);
                    if (planet.EntityCollection.IsDetectCollision(this))
                    {
                        Size = oldSize;
                    }
                    else
                    {
                        foodReserve /= growthRate;
                        isGrow = true;
                    }
                    planet.EntityCollection.SpatialHashTable.Add(this);
                }
            }
            return isGrow;
        }

        /// <summary>
        /// Try move entity
        /// </summary>
        /// <param name="planet">planet</param>
        /// <param name="random">random number generator</param>
        /// <param name="timeDelta">time delta</param>
        internal void TryMakeWalkFightOrFlight(Planet planet, Random random, double timeDelta, out AbstractEntity nearestPrey)
        {
            nearestPrey = null;
            if (random.Next(0, 5) == 0)
            {
                AbstractEntity nearestPredator;
                bool isNearestPreyCloserThanPredator;
                planet.EntityCollection.SpatialHashTable.GetNearestViewablePreyAndPredator(this, out nearestPrey, out nearestPredator, out isNearestPreyCloserThanPredator);

                if (nearestPrey != null && isNearestPreyCloserThanPredator)
                    this.AngleRadian = Optics.GetAngleRadianTo(this, nearestPrey);
                else if (nearestPredator != null && !isNearestPreyCloserThanPredator)
                    this.AngleRadian = Optics.GetAngleRadianTo(this, nearestPredator) + Math.PI;
            }

            Physics.TryMakeWalk(this, speed, planet, timeDelta * 4.0 * Program.SpeedMultiplier);

            if (planet.EntityCollection.IsDetectCollision(this, planet))
            {
                Physics.TryMakeWalk(this, speed * 1.01, Math.PI, planet, timeDelta * 4.0 * Program.SpeedMultiplier);
                this.AngleRadian = random.NextDouble() * Math.PI * 2.0;
                /*if (random.Next(0, 2) == 0)
                    this.AngleRadian += Math.PI / 2.0;
                else
                    this.AngleRadian -= Math.PI / 2.0;*/
            }
        }

        /// <summary>
        /// Try make an animal eat an entity
        /// </summary>
        /// <param name="prey">entity to eat</param>
        /// <param name="planet">planet</param>
        internal bool TryEat(AbstractEntity prey, Planet planet)
        {
            if (prey.Integrity < 0)
            {
                prey.Decay(planet);
                return false;
            }

            if (planet.EntityCollection.SpatialHashTable.GetDistance(this, prey) < Size + prey.Size)
            {
                if (foodReserve < maximumFoodReserve)
                {
                    prey.Integrity -= eatingRate;
                    foodReserve += eatingRate;
                    AngleRadian = Optics.GetAngleRadianTo(this, prey);
                    return true;
                }
            }

            return false;
        }
        #endregion

        #region Protected Methods
        /// <summary>
        /// Return animal's corpse
        /// </summary>
        /// <param name="planet">planet</param>
        /// <returns>animal's corpse</returns>
        protected override IEnumerable<AbstractEntity> GetDecayEntities(Planet planet)
        {
            return new AbstractEntity[] { new CorpseEntity() };
        }

        /// <summary>
        /// Position criteria
        /// </summary>
        /// <returns>Position criteria</returns>
        protected override PositionCriteria BuildPositionCriteria()
        {
            return PositionCriteria.Ground;
        }

        /// <summary>
        /// ZIndex layer
        /// </summary>
        /// <returns>ZIndex layer</returns>
        protected override ZIndexLayer BuildZIndexLayer()
        {
            return ZIndexLayer.OnFloor;
        }

        /// <summary>
        /// Build Is keep sprite of previous entity
        /// </summary>
        /// <returns>Is keep sprite of previous entity</returns>
        protected override bool BuildIsKeepSpriteOfPreviousEntity()
        {
            return false;
        }

        /// <summary>
        /// Build Is keep size of previous entity
        /// </summary>
        /// <returns>Is keep size of previous entity</returns>
        protected override bool BuildIsKeepSizeOfPreviousEntity()
        {
            return false;
        }

        /// <summary>
        /// Whether entity is affected by collisions
        /// </summary>
        /// <returns></returns>
        protected override bool BuildIsAffectedByCollision()
        {
            return true;
        }

        /// <summary>
        /// Animal's sprite
        /// </summary>
        /// <returns>Animal's sprite</returns>
        protected override EntitySprite BuildEntitySprite()
        {
            return SpriteManager.GetSprite(this.GetType(), true);
        }

        /// <summary>
        /// Maximum size, or default size (full size)
        /// </summary>
        /// <returns>Maximum size, or default size (full size)</returns>
        protected override double BuildSize()
        {
            return BuildMaximumSize();
        }
        #endregion

        #region Abstract Methods
        /// <summary>
        /// Get list of entity type that can be eaten by this animal (null if none)
        /// </summary>
        /// <returns>list of entity type that can be eaten by this animal (null if none)</returns>
        protected abstract HashSet<Type> BuildPreyTypeList();

        /// <summary>
        /// Get list of entity type that can eat this animal (null if none)
        /// </summary>
        /// <returns>list of entity type that can eat this animal (null if none)</returns>
        protected abstract HashSet<Type> BuildPredatorTypeList();

        /// <summary>
        /// Size at birth
        /// </summary>
        /// <returns>size at birth</returns>
        protected abstract double BuildSizeAtBirth();

        /// <summary>
        /// Minimum size for reproduction (must be smaller or equal to default size)
        /// </summary>
        /// <returns>Minimum size for reproduction (must be smaller or equal to default size)</returns>
        protected abstract double BuildMinimumSizeForReproduction();

        /// <summary>
        /// Minimum food reserve for reproduction (must be smaller or equal to default size)
        /// </summary>
        /// <returns>Minimum food reserve for reproduction (must be smaller or equal to default size)</returns>
        protected abstract double BuildMinimumFoodReserveForReproduction();

        /// <summary>
        /// Minimum food reserve for growth (must be smaller or equal to default size)
        /// </summary>
        /// <returns>Minimum food reserve for growth (must be smaller or equal to default size)</returns>
        protected abstract double BuildMinimumFoodReserveForGrowth();

        /// <summary>
        /// Maximum size
        /// </summary>
        /// <returns>Maximum size</returns>
        protected abstract double BuildMaximumSize();

        /// <summary>
        /// Build reproduction cycle time (seconds)
        /// </summary>
        /// <returns>reproduction cycle time (seconds) (0 = never)</returns>
        protected abstract double BuildReproductionCycleTime();

        /// <summary>
        /// Build maximum food reserve value
        /// </summary>
        /// <returns>maximum food reserve value</returns>
        protected abstract double BuildMaximumFoodReserve();

        /// <summary>
        /// Build growth rate
        /// </summary>
        /// <returns>growth rate</returns>
        protected abstract double BuildGrowthRate();

        /// <summary>
        /// Get reproduction spore for entity
        /// (null if there is no reproduction spore)
        /// </summary>
        /// <returns>reproduction spore for entity (null if there is no reproduction spore)</returns>
        protected abstract AbstractAnimalEntity GetOffspringEntity();

        /// <summary>
        /// Build walking speed
        /// </summary>
        /// <returns>walking speed</returns>
        protected abstract double BuildSpeed();

        /// <summary>
        /// Build view range (radius)
        /// </summary>
        /// <returns>view range (radius)</returns>
        protected abstract double BuildViewRangeRadius();

        /// <summary>
        /// How much decrement of prey integrity by eating shot
        /// </summary>
        protected abstract double BuildEatingRate();
        #endregion

        #region Properties
        /// <summary>
        /// Speed
        /// </summary>
        public double Speed
        {
            get { return speed; }
        }

        /// <summary>
        /// Food reserve
        /// </summary>
        public double FoodReserve
        {
            get{return foodReserve;}
            set { foodReserve = value; }
        }

        /// <summary>
        /// Prey type list
        /// </summary>
        public HashSet<Type> PreyTypeList
        {
            get{return preyTypeList;}
        }

        /// <summary>
        /// Predator type list
        /// </summary>
        public HashSet<Type> PredatorTypeList
        {
            get { return predatorTypeList; }
        }

        /// <summary>
        /// View range radius
        /// </summary>
        public double ViewRangeRadius
        {
            get { return viewRangeRadius; }
        }

        /// <summary>
        /// How much decrement of prey integrity by eating shot
        /// </summary>
        public double EatingRate
        {
            get { return eatingRate; }
        }
        #endregion
    }
}