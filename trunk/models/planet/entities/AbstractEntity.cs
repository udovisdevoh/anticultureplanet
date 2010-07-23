using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AntiCulturePlanet
{
    /// <summary>
    /// Represents entities on the planet (people, trees, animals, rocks, tools)
    /// </summary>
    internal abstract class AbstractEntity
    {
        #region Fields
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
        #endregion

        #region Constructor
        /// <summary>
        /// Build abstract entity
        /// </summary>
        public AbstractEntity()
        {
            mass = BuildMass();
            size = BuildSize();
            decayTime = BuildDecayTime();
        }
        #endregion

        #region Abstract Methods
        /// <summary>
        /// Build decay time
        /// </summary>
        /// <returns>Build decay time</returns>
        internal abstract double BuildDecayTime();

        /// <summary>
        /// Build size
        /// </summary>
        /// <returns>Build size</returns>
        internal abstract double BuildSize();

        /// <summary>
        /// Build mass
        /// </summary>
        /// <returns>Build mass</returns>
        internal abstract double BuildMass();
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
        #endregion
    }
}
