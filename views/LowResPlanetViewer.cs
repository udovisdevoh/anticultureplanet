using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AntiCulturePlanet
{
    /// <summary>
    /// Very low quality planet view
    /// </summary>
    internal class LowResPlanetViewer : PlanetViewer
    {
        #region Fields
        /// <summary>
        /// Screen width
        /// </summary>
        private int screenWidth;

        /// <summary>
        /// Screen height
        /// </summary>
        private int screenHeight;
        #endregion

        #region Constructor
        /// <summary>
        /// Build planet view
        /// </summary>
        /// <param name="screenWidth">screen width</param>
        /// <param name="screenHeight">screen height</param>
        public LowResPlanetViewer(int screenWidth, int screenHeight)
        {
            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;
        }
        #endregion
    }
}