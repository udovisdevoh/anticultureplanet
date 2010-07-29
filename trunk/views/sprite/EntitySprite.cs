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
        #region Fields and parts
        /// <summary>
        /// Image file name
        /// </summary>
        private string imageFileName;

        /// <summary>
        /// Orignal surface
        /// </summary>
        private Surface originalSurface;

        /// <summary>
        /// To cache scalled sprites
        /// </summary>
        private Dictionary<int, Surface> scallingCache;
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
            scallingCache = new Dictionary<int, Surface>();
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
            Surface surface;
            if (!scallingCache.TryGetValue(width * 128 + height, out surface))
            {
                double scaleX = (double)(width) / (double)(originalSurface.Width);
                double scaleY = (double)(height) / (double)(originalSurface.Height);
                surface = originalSurface.CreateScaledSurface(scaleX, scaleY, true);
                scallingCache.Add(width * 128 + height, surface);
            }

            return surface;
        }
        #endregion
    }
}