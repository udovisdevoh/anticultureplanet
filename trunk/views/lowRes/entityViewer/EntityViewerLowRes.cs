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
        /// Update the surface for joined ground and sprite
        /// </summary>
        /// <param name="groundSurcace">ground surface</param>
        /// <param name="groundAndSpriteSurface">joined ground surface (to update)</param>
        /// <param name="spatialHashTable">spatial hash table</param>
        /// <param name="screenWidth">screen width</param>
        /// <param name="screenHeight">screen height</param>
        /// <param name="viewedTileX">viewed tile X</param>
        /// <param name="viewedTileY">viewed tile Y</param>
        /// <param name="tilePixelWidth">tile pixel width</param>
        /// <param name="tilePixelHeight">tile pixel height</param>
        /// <returns>joined ground surface (updated)</returns>
        internal Surface UpdateGroundAndSpriteSurface(Surface groundSurcace, Surface groundAndSpriteSurface, SpatialHashTable spatialHashTable, int screenWidth, int screenHeight, double viewedTileX, double viewedTileY, int tilePixelWidth, int tilePixelHeight)
        {
            for (int x = 0; x < spatialHashTable.ColumnCount; x++)
            {
                for (int y = 0; y < spatialHashTable.ColumnCount; y++)
                {
                    Bucket bucket = spatialHashTable[x, y];
                    if (bucket.IsNeedRedraw)
                    {
                        int rectangleX = spatialHashTable.BucketSize * tilePixelWidth * x;
                        int rectangleY = spatialHashTable.BucketSize * tilePixelHeight * y;
                        int rectangleWidth = spatialHashTable.BucketSize * tilePixelWidth;
                        int rectangleHeight = spatialHashTable.BucketSize * tilePixelHeight;

                        Rectangle bucketPosition = new Rectangle(rectangleX, rectangleY, rectangleWidth, rectangleHeight);
                        groundAndSpriteSurface.Blit(groundSurcace, bucketPosition, bucketPosition);

                        foreach (AbstractEntity entity in bucket)
                        {
                            int spriteWidth = (int)Math.Round(entity.Size * tilePixelWidth);
                            int spriteHeight = (int)Math.Round(entity.Size * tilePixelHeight);
                            Surface spriteSurface = entity.EntitySprite.GetSurface(spriteWidth, spriteHeight);
                            int absolutePositionX = (int)Math.Round(entity.X * tilePixelWidth) - spriteWidth / 2 + tilePixelWidth / 2;
                            int absolutePositionY = (int)Math.Round(entity.Y * tilePixelHeight) - spriteHeight / 2 + tilePixelHeight / 2;

                            groundAndSpriteSurface.Blit(spriteSurface, new Point(absolutePositionX, absolutePositionY));
                        }
                        bucket.IsNeedRedraw = false;
                    }
                }
            }
            return groundAndSpriteSurface;
        }
    }
}