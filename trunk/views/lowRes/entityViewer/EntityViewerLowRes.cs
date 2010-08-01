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
        /// <param name="planet">planet</param>
        /// <param name="groundAndSpriteSurface">joined ground surface (to update)</param>
        /// <param name="spatialHashTable">spatial hash table</param>
        /// <param name="screenWidth">screen width</param>
        /// <param name="screenHeight">screen height</param>
        /// <param name="viewedTileX">viewed tile X</param>
        /// <param name="viewedTileY">viewed tile Y</param>
        /// <param name="tilePixelWidth">tile pixel width</param>
        /// <param name="tilePixelHeight">tile pixel height</param>
        /// <param name="totalMapWidth">total map width</param>
        /// <param name="totalMapHeight">total map height</param>
        /// <returns>joined ground surface (updated)</returns>
        internal Surface UpdateGroundAndSpriteSurface(Planet planet, Surface groundAndSpriteSurface, SpatialHashTable spatialHashTable, int screenWidth, int screenHeight, double viewedTileX, double viewedTileY, int tilePixelWidth, int tilePixelHeight, int totalMapWidth, int totalMapHeight)
        {
            for (int bucketX = 0; bucketX < spatialHashTable.ColumnCount; bucketX++)
            {
                for (int bucketY = 0; bucketY < spatialHashTable.RowCount; bucketY++)
                {
                    Bucket bucket = spatialHashTable[bucketX, bucketY];
                    if (bucket.IsNeedRedraw)
                    {
                        int rectangleLeft = spatialHashTable.BucketSize * tilePixelWidth * bucketX;
                        int rectangleTop = spatialHashTable.BucketSize * tilePixelHeight * bucketY;
                        int rectangleWidth = spatialHashTable.BucketSize * tilePixelWidth;
                        int rectangleHeight = spatialHashTable.BucketSize * tilePixelHeight;
                        int rectangleBottom = rectangleTop + rectangleHeight;
                        int rectangleRight = rectangleLeft + rectangleWidth;

                        int viewedPixelLeft = (int)(viewedTileX * tilePixelWidth);
                        int viewedPixelTop = (int)(viewedTileY * tilePixelHeight);
                        int viewedPixelRight = viewedPixelLeft + screenWidth;
                        int viewedPixelBottom = viewedPixelTop + screenHeight;

                        if ((viewedPixelLeft <= rectangleRight
                            && viewedPixelTop <= rectangleBottom
                            && viewedPixelRight >= rectangleLeft
                            && viewedPixelBottom >= rectangleTop)
                            ||
                            (viewedPixelLeft <= rectangleRight + totalMapWidth
                            && viewedPixelTop <= rectangleBottom + totalMapHeight
                            && viewedPixelRight >= rectangleLeft + totalMapWidth
                            && viewedPixelBottom >= rectangleTop + totalMapHeight)
                            ||
                            (viewedPixelLeft <= rectangleRight + totalMapWidth
                            && viewedPixelTop <= rectangleBottom
                            && viewedPixelRight >= rectangleLeft + totalMapWidth
                            && viewedPixelBottom >= rectangleTop)
                            ||
                            (viewedPixelLeft <= rectangleRight
                            && viewedPixelTop <= rectangleBottom + totalMapHeight
                            && viewedPixelRight >= rectangleLeft
                            && viewedPixelBottom >= rectangleTop + totalMapHeight))
                        {

                            //We draw the tiles
                            for (int tileX = 0; tileX < spatialHashTable.BucketSize; tileX++)
                            {
                                for (int tileY = 0; tileY < spatialHashTable.BucketSize; tileY++)
                                {
                                    Tile tile = planet[bucketX * spatialHashTable.BucketSize + tileX, bucketY * spatialHashTable.BucketSize + tileY];
                                    groundAndSpriteSurface.Blit(tile.Surface, new Point(bucketX * spatialHashTable.BucketSize * tilePixelWidth + tileX * tilePixelWidth, bucketY * spatialHashTable.BucketSize * tilePixelHeight + tileY * tilePixelHeight));
                                }
                            }

                            //We draw the sprites
                            foreach (AbstractEntity entity in bucket)
                            {
                                int spriteWidth = (int)Math.Round(entity.Size * tilePixelWidth);
                                int spriteHeight = (int)Math.Round(entity.Size * tilePixelHeight);
                                Surface spriteSurface = entity.EntitySprite.GetSurface(spriteWidth, spriteHeight, (int)entity.AngleDegree);
                                int absolutePositionX = (int)Math.Round(entity.X * tilePixelWidth) - spriteWidth / 2 + tilePixelWidth / 2;
                                int absolutePositionY = (int)Math.Round(entity.Y * tilePixelHeight) - spriteHeight / 2 + tilePixelHeight / 2;

                                groundAndSpriteSurface.Blit(spriteSurface, new Point(absolutePositionX, absolutePositionY));
                            }
                            bucket.IsNeedRedraw = false;
                        }
                    }
                }
            }
            return groundAndSpriteSurface;
        }
    }
}