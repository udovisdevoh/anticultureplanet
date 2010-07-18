using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using SdlDotNet.Graphics;
using SdlDotNet.Core;
using SdlDotNet.Input;
using SdlDotNet.Audio;
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
        private int tilePixelWidth = 32;

        /// <summary>
        /// Width of a tile (per pixel)
        /// </summary>
        private int tilePixelHeight = 32;

        /// <summary>
        /// Main drawing surface
        /// </summary>
        private Surface mainSurface;

        /// <summary>
        /// Ground surface
        /// </summary>
        private Surface groundSurcace;
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
            groundSurcace = new Surface(planet.Width * tilePixelWidth, planet.Height * tilePixelHeight, 32);
            this.mainSurface = mainSurface;
            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;
        }
        #endregion

        #region Internal Methods
        /// <summary>
        /// Redraw planet
        /// </summary>
        /// <param name="planet">planet</param>
        internal override void Update(Planet planet)
        {
            if (planet.IsNeedRefresh)
                for (int y = 0; y < planet.Height; y++)
                    for (int x = 0; x < planet.Width; x++)
                        if (planet[x,y].IsNeedRefresh)
                            Update(planet[x, y]);

            planet.IsNeedRefresh = false;

            mainSurface.Blit(groundSurcace);
        }

        internal void Update(Tile tile)
        {
            Color color = Color.Green;
            if (tile.IsWater)
                color = Color.Blue;
            else if (tile.Altitude > 15)
                color = Color.Yellow;

            Rectangle rectangle = new Rectangle(tile.X * tilePixelWidth, tile.Y * tilePixelHeight, tilePixelWidth, tilePixelHeight);

            groundSurcace.Fill(rectangle, color);

            tile.IsNeedRefresh = false;
        }
        #endregion
    }
}