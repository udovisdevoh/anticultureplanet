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
                Surface spriteSurface = entity.EntitySprite.GetSurface((int)Math.Round(entity.Size * tilePixelWidth), (int)Math.Round(entity.Size * tilePixelHeight));


                surface.Blit(spriteSurface, new Point(viewedTileX * tilePixelWidth, viewedTileY * tilePixelHeight));
            }
        }
    }
}