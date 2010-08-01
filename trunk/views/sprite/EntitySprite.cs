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

        /// <summary>
        /// Whether the sprite rotates
        /// </summary>
        private bool isRotate;
        #endregion

        #region Consturctor
        /// <summary>
        /// Build entity sprite
        /// </summary>
        /// <param name="imageFileName">image file name</param>
        /// <param name="isRotate">whether the sprite rotates</param>
        public EntitySprite(string imageFileName, bool isRotate)
        {
            this.isRotate = isRotate;
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
        /// <param name="angle">angle</param>
        /// <returns>surface of specified size</returns>
        internal Surface GetSurface(int width, int height, int angle)
        {
            Surface surface;

            if (isRotate)
            {
                angle = ((int)Math.Round(angle / 8.0)) * 8;
            }
            else
            {
                angle = 0;
            }

            if (!scallingCache.TryGetValue(width * 128 + height + angle * 46080, out surface))
            {
                double scaleX = (double)(width) / (double)(originalSurface.Width);
                double scaleY = (double)(height) / (double)(originalSurface.Height);
                if (width == originalSurface.Width && height == originalSurface.Height)
                    surface = originalSurface;
                else
                    surface = originalSurface.CreateScaledSurface(scaleX, scaleY, true);

                int rotation = 270 - angle;
                while (rotation >= 360)
                    rotation -= 360;
                while (rotation < 0)
                    rotation += 360;

                if (isRotate)
                {
                    surface = surface.CreateRotatedSurface(270 - angle);
                }

                scallingCache.Add(width * 128 + height + angle * 46080, surface);
            }

            return surface;
        }
        #endregion
    }
}