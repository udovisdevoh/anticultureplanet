using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SdlDotNet.Graphics;
using SdlDotNet.Core;
using SdlDotNet.Graphics.Primitives;

namespace AntiCulturePlanet
{
    /// <summary>
    /// Represents an entity's sprite definition
    /// </summary>
    internal class EntitySprite
    {
        #region Fields
        /// <summary>
        /// Image file name
        /// </summary>
        private string imageFileName;

        /// <summary>
        /// Orignal surface
        /// </summary>
        private Surface originalSurface;
        #endregion

        #region Consturctor
        /// <summary>
        /// Build entity sprite
        /// </summary>
        /// <param name="imageFileName">image file name</param>
        public EntitySprite(string imageFileName)
        {
            this.imageFileName = imageFileName;
            originalSurface = new Surface("assets/graphics/entities/" + imageFileName);
        }
        #endregion

        #region Internal Methods
        /// <summary>
        /// Get or create surface of specified size
        /// </summary>
        /// <param name="width">width</param>
        /// <param name="height">height</param>
        /// <returns>surface of specified size</returns>
        internal Surface GetSurface(int width, int height)
        {
            double scaleX = (double)(width) / (double)(originalSurface.Width);
            double scaleY = (double)(height) / (double)(originalSurface.Height);
            return originalSurface.CreateScaledSurface(scaleX, scaleY, true);
        }
        #endregion
    }
}
