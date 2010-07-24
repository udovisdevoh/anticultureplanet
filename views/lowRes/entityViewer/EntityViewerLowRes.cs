using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using SdlDotNet.Graphics;
using SdlDotNet.Core;
using SdlDotNet.Graphics.Primitives;

namespace AntiCulturePlanet
{
    /// <summary>
    /// To view entities as 2D sprites from top
    /// </summary>
    internal class EntityViewerLowRes
    {
        /// <summary>
        /// To view entities
        /// </summary>
        /// <param name="surface">surface</param>
        /// <param name="entityCollection">entities to view</param>
        /// <param name="screenHeight">screen height</param>
        /// <param name="screenWidth">screen width</param>
        /// <param name="viewedTileX">viewed tile x (view position)</param>
        /// <param name="viewedTileY">viewed tile y (view position)</param>
        /// <param name="tilePixelWidth">tile width</param>
        /// <param name="tilePixelHeight">tile height</param>
        internal void Update(Surface surface, EntityCollection entityCollection, int screenWidth, int screenHeight, int viewedTileX, int viewedTileY, int tilePixelWidth, int tilePixelHeight)
        {
            foreach (AbstractEntity entity in entityCollection)
            {
                int surfaceWidth = (int)Math.Round(entity.Size * tilePixelWidth);
                int surfaceHeight = (int)Math.Round(entity.Size * tilePixelHeight);

                Surface spriteSurface = entity.EntitySprite.GetSurface(surfaceWidth, surfaceHeight);

                int spriteLeftCornerX = (int)Math.Round(entity.X * tilePixelWidth) - surfaceWidth / 2 + tilePixelWidth / 2;
                int spriteLeftCornerY = (int)Math.Round(entity.Y * tilePixelHeight) - surfaceHeight / 2 + tilePixelHeight / 2;

                surface.Blit(spriteSurface, new Point(spriteLeftCornerX, spriteLeftCornerY));
            }
        }
    }
}