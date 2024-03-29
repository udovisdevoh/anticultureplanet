﻿using System;
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
    /// Very low quality planet view
    /// </summary>
    internal class PlanetViewerLowRes : PlanetViewer
    {
        #region Fields and parts
        /// <summary>
        /// Tile viewer
        /// </summary>
        private TileViewerLowRes tileViewer;

        /// <summary>
        /// Entity viewer
        /// </summary>
        private EntityViewerLowRes entityViewerLowRes;

        /// <summary>
        /// Screen width
        /// </summary>
        private int screenWidth;

        /// <summary>
        /// Screen height
        /// </summary>
        private int screenHeight;

        /// <summary>
        /// Width of a tile (per pixel)
        /// </summary>
        private int tilePixelWidth = 16;

        /// <summary>
        /// Width of a tile (per pixel)
        /// </summary>
        private int tilePixelHeight = 16;

        /// <summary>
        /// Main drawing surface
        /// </summary>
        private Surface mainSurface;

        /// <summary>
        /// Surface for joined ground and sprite layers
        /// </summary>
        private Surface groundAndSpriteSurface;

        /// <summary>
        /// Horizontal tile offset
        /// </summary>
        private int viewedTileX;

        /// <summary>
        /// Vertical tile offset
        /// </summary>
        private int viewedTileY;
        #endregion

        #region Constructor
        /// <summary>
        /// Build planet view
        /// </summary>
        /// <param name="screenWidth">screen width</param>
        /// <param name="screenHeight">screen height</param>
        /// <param name="planet">planet</param>
        /// <param name="mainSurface">main drawing surface</param>
        public PlanetViewerLowRes(Surface mainSurface, int screenWidth, int screenHeight, Planet planet)
        {
            viewedTileX = planet.Width / 2;
            viewedTileY = planet.Height / 2;

            tileViewer = new TileViewerLowRes();
            groundAndSpriteSurface = new Surface(planet.Width * tilePixelWidth, planet.Height * tilePixelHeight, Program.BitsPerPixel);
            this.mainSurface = mainSurface;
            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;

            entityViewerLowRes = new EntityViewerLowRes();
        }
        #endregion

        #region Internal Methods
        /// <summary>
        /// Redraw planet and mark as IsNeedRefresh = false
        /// </summary>
        /// <param name="planet">planet</param>
        internal override void Update(Planet planet)
        {
            if (planet.IsNeedRefresh)
            {
                for (int y = 0; y < planet.Height; y++)
                {
                    for (int x = 0; x < planet.Width; x++)
                    {
                        if (planet[x, y].IsNeedRefresh)
                        {
                            planet[x, y].Surface = tileViewer.BuildOrLoadSurface(planet[x, y], planet, tilePixelWidth, tilePixelHeight);
                            planet[x, y].IsNeedRefresh = false;
                        }
                    }
                }
                planet.IsNeedRefresh = false;
            }

            

            int pixelOffsetX = (int)(0 - (viewedTileX * (double)tilePixelWidth));
            int pixelOffsetY = (int)(0 - (viewedTileY * (double)tilePixelHeight));

            groundAndSpriteSurface = entityViewerLowRes.UpdateGroundAndSpriteSurface(planet, groundAndSpriteSurface, planet.EntityCollection.SpatialHashTable, screenWidth, screenHeight, viewedTileX, viewedTileY, tilePixelWidth, tilePixelHeight, planet.Width * tilePixelWidth, planet.Height * tilePixelHeight);

            mainSurface.Blit(groundAndSpriteSurface, new Point(pixelOffsetX, pixelOffsetY));
            mainSurface.Blit(groundAndSpriteSurface, new Point(pixelOffsetX + planet.Width * tilePixelWidth, pixelOffsetY));
            mainSurface.Blit(groundAndSpriteSurface, new Point(pixelOffsetX, pixelOffsetY + planet.Height * tilePixelHeight));
            mainSurface.Blit(groundAndSpriteSurface, new Point(pixelOffsetX + planet.Width * tilePixelWidth, pixelOffsetY + planet.Height * tilePixelHeight));
        }

        /// <summary>
        /// Move view
        /// </summary>
        /// <param name="tileOffsetX">horizontal tile offset</param>
        /// <param name="tileOffsetY">vertical tile offset</param>
        /// <param name="width">planet's width</param>
        /// <param name="height">planet's height</param>
        internal override void MoveView(int tileOffsetX, int tileOffsetY, int width, int height)
        {
            this.viewedTileX += tileOffsetX;
            this.viewedTileY += tileOffsetY;

            while (this.viewedTileX < 0)
                this.viewedTileX += width;
            while (this.viewedTileY < 0)
                this.viewedTileY += height;
            while (this.viewedTileX >= width)
                this.viewedTileX -= width;
            while (this.viewedTileY >= height)
                this.viewedTileY -= height;
        }
        #endregion
    }
}