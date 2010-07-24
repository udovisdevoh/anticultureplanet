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
        /// <param name="surfaceToDrawOn">surface to draw on</param>
        /// <param name="entityCollection">entities to view</param>
        /// <param name="screenHeight">screen height</param>
        /// <param name="screenWidth">screen width</param>
        /// <param name="viewedTileX">viewed tile x (view position)</param>
        /// <param name="viewedTileY">viewed tile y (view position)</param>
        /// <param name="tilePixelWidth">tile width</param>
        /// <param name="tilePixelHeight">tile height</param>
        internal void Update(Surface surfaceToDrawOn, EntityCollection entityCollection, int screenWidth, int screenHeight, int viewedTileX, int viewedTileY, int tilePixelWidth, int tilePixelHeight)
        {
            foreach (AbstractEntity entity in entityCollection)
            {
                int spriteWidth = (int)Math.Round(entity.Size * tilePixelWidth);
                int spriteHeight = (int)Math.Round(entity.Size * tilePixelHeight);

                int absolutePositionX = (int)Math.Round(entity.X * tilePixelWidth) - spriteWidth / 2 + tilePixelWidth / 2;
                int absolutePositionY = (int)Math.Round(entity.Y * tilePixelHeight) - spriteHeight / 2 + tilePixelHeight / 2;

                int screenRelativePositionX = absolutePositionX - viewedTileX * tilePixelWidth;
                int screenRelativePositionY = absolutePositionY - viewedTileY * tilePixelHeight;

                if (screenRelativePositionX > screenWidth || screenRelativePositionY > screenHeight || screenRelativePositionX + spriteWidth <= 0 || screenRelativePositionY + spriteHeight <= 0)
                    continue;

                Surface spriteSurface = entity.EntitySprite.GetSurface(spriteWidth, spriteHeight);

                surfaceToDrawOn.Blit(spriteSurface, new Point(screenRelativePositionX, screenRelativePositionY));
            }
        }
    }
}