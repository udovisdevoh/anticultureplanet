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
        /// <param name="mapSurfaceWidth">map surface width (tiles)</param>
        /// <param name="mapSurfaceHeight">map surface height (tiles)</param>
        internal void Update(Surface surfaceToDrawOn, EntityCollection entityCollection, int screenWidth, int screenHeight, double viewedTileX, double viewedTileY, int tilePixelWidth, int tilePixelHeight, int mapWidth, int mapHeight)
        {
            int totalMapSurfaceWidth = mapWidth * tilePixelWidth;
            int totalMapSurfaceHeight = mapHeight * tilePixelHeight;
            
            int spriteWidth;
            int spriteHeight;

            int absolutePositionX;
            int absolutePositionY;

            int screenRelativePositionX;
            int screenRelativePositionY;

            foreach (AbstractEntity entity in entityCollection)
            {
                spriteWidth = (int)Math.Round(entity.Size * tilePixelWidth);
                spriteHeight = (int)Math.Round(entity.Size * tilePixelHeight);

                absolutePositionX = (int)Math.Round(entity.X * tilePixelWidth) - spriteWidth / 2 + tilePixelWidth / 2;
                absolutePositionY = (int)Math.Round(entity.Y * tilePixelHeight) - spriteHeight / 2 + tilePixelHeight / 2;

                screenRelativePositionX = (int)(absolutePositionX - viewedTileX * (double)(tilePixelWidth));
                screenRelativePositionY = (int)(absolutePositionY - viewedTileY * (double)(tilePixelHeight));
                
                Update(entity, surfaceToDrawOn, screenWidth, screenHeight, screenRelativePositionX, screenRelativePositionY, spriteWidth, spriteHeight, totalMapSurfaceWidth, totalMapSurfaceHeight, -1, -1, totalMapSurfaceWidth, totalMapSurfaceHeight);
                Update(entity, surfaceToDrawOn, screenWidth, screenHeight, screenRelativePositionX, screenRelativePositionY, spriteWidth, spriteHeight, totalMapSurfaceWidth, totalMapSurfaceHeight, -1, 0, totalMapSurfaceWidth, totalMapSurfaceHeight);
                Update(entity, surfaceToDrawOn, screenWidth, screenHeight, screenRelativePositionX, screenRelativePositionY, spriteWidth, spriteHeight, totalMapSurfaceWidth, totalMapSurfaceHeight, -1, 1, totalMapSurfaceWidth, totalMapSurfaceHeight);

                Update(entity, surfaceToDrawOn, screenWidth, screenHeight, screenRelativePositionX, screenRelativePositionY, spriteWidth, spriteHeight, totalMapSurfaceWidth, totalMapSurfaceHeight, 0, -1, totalMapSurfaceWidth, totalMapSurfaceHeight);
                Update(entity, surfaceToDrawOn, screenWidth, screenHeight, screenRelativePositionX, screenRelativePositionY, spriteWidth, spriteHeight, totalMapSurfaceWidth, totalMapSurfaceHeight, 0, 0, totalMapSurfaceWidth, totalMapSurfaceHeight);
                Update(entity, surfaceToDrawOn, screenWidth, screenHeight, screenRelativePositionX, screenRelativePositionY, spriteWidth, spriteHeight, totalMapSurfaceWidth, totalMapSurfaceHeight, 0, 1, totalMapSurfaceWidth, totalMapSurfaceHeight);

                Update(entity, surfaceToDrawOn, screenWidth, screenHeight, screenRelativePositionX, screenRelativePositionY, spriteWidth, spriteHeight, totalMapSurfaceWidth, totalMapSurfaceHeight, 1, -1, totalMapSurfaceWidth, totalMapSurfaceHeight);
                Update(entity, surfaceToDrawOn, screenWidth, screenHeight, screenRelativePositionX, screenRelativePositionY, spriteWidth, spriteHeight, totalMapSurfaceWidth, totalMapSurfaceHeight, 1, 0, totalMapSurfaceWidth, totalMapSurfaceHeight);
                Update(entity, surfaceToDrawOn, screenWidth, screenHeight, screenRelativePositionX, screenRelativePositionY, spriteWidth, spriteHeight, totalMapSurfaceWidth, totalMapSurfaceHeight, 1, 1, totalMapSurfaceWidth, totalMapSurfaceHeight);
            }
        }

        private void Update(AbstractEntity entity, Surface surfaceToDrawOn, int screenWidth, int screenHeight, int screenRelativePositionX, int screenRelativePositionY, int spriteWidth, int spriteHeight, int mapSurfaceWidth, int mapSurfaceHeight, int totalMapWidthOffset, int totalMapHeightOffset, int totalMapSurfaceWidth, int totalMapSurfaceHeight)
        {
            screenRelativePositionX += (totalMapWidthOffset * totalMapSurfaceWidth);
            screenRelativePositionY += (totalMapHeightOffset * totalMapSurfaceHeight);

            bool isSpriteOnViewableXAxis = (screenRelativePositionX <= screenWidth) && (screenRelativePositionX + spriteWidth >= 0);
            bool isSpriteOnViewableYAxis = (screenRelativePositionY <= screenHeight) && (screenRelativePositionY + spriteHeight >= 0);

            if (isSpriteOnViewableXAxis && isSpriteOnViewableYAxis)
            {
                Surface spriteSurface = entity.EntitySprite.GetSurface(spriteWidth, spriteHeight);
                surfaceToDrawOn.Blit(spriteSurface, new Point(screenRelativePositionX, screenRelativePositionY));
            }
        }
    }
}