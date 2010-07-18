﻿using System;
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
    /// To display tiles
    /// </summary>
    internal class TileViewerLowRes
    {
        #region Fields
        /// <summary>
        /// Random number generator
        /// </summary>
        private Random random = new Random();
        #endregion

        #region Internal Methods
        /// <summary>
        /// Redraw tile and mark as IsNeedRefresh = false
        /// </summary>
        /// <param name="tile">tile</param>
        /// <param name="planet">planet</param>
        /// <param name="surface">surface to draw to</param>
        /// <param name="tilePixelWidth">tile's width (pixels)</param>
        /// <param name="tilePixelHeight">tile's height (pixels)</param>
        internal void Update(Tile tile, Planet planet, Surface surface, int tilePixelWidth, int tilePixelHeight)
        {            
            if (tile.IsWater)
            {
                DrawWaterTile(tile, planet, surface, tilePixelWidth, tilePixelHeight);
            }
            else
            {
                DrawSoilTile(tile, planet, surface, tilePixelWidth, tilePixelHeight);
            }

            tile.IsNeedRefresh = false;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Draw soil tile
        /// </summary>
        /// <param name="tile">tile</param>
        /// <param name="planet">planet</param>
        /// <param name="surface">surface to draw on</param>
        /// <param name="tilePixelWidth">tile width (pixels)</param>
        /// <param name="tilePixelHeight">tile height (pixels)</param>
        private void DrawSoilTile(Tile tile, Planet planet, Surface surface, int tilePixelWidth, int tilePixelHeight)
        {
            int green = 128;

            int red = (tile.Altitude - planet.WaterThresholdAltitude) * 255 / (planet.MaxAltitude - planet.WaterThresholdAltitude);

            Color color = Color.FromArgb(255, red, green, 0);

            Rectangle rectangle = new Rectangle(tile.X * tilePixelWidth, tile.Y * tilePixelHeight, tilePixelWidth, tilePixelHeight);
            surface.Fill(rectangle, color);
        }

        /// <summary>
        /// Draw water tile
        /// </summary>
        /// <param name="tile">tile</param>
        /// <param name="planet">planet</param>
        /// <param name="surface">surface to draw on</param>
        /// <param name="tilePixelWidth">tile width (pixels)</param>
        /// <param name="tilePixelHeight">tile height (pixels)</param>
        private void DrawWaterTile(Tile tile, Planet planet, Surface surface, int tilePixelWidth, int tilePixelHeight)
        {
            int blue = (tile.Altitude - planet.MinAltitude) * 255 / (planet.WaterThresholdAltitude - planet.MinAltitude);

            Color color = Color.FromArgb(255, 0, 0, blue);
            Rectangle rectangle = new Rectangle(tile.X * tilePixelWidth, tile.Y * tilePixelHeight, tilePixelWidth, tilePixelHeight);
            surface.Fill(rectangle, color);

            int randomPixelCount = (tilePixelWidth * tilePixelHeight) / 16;
            for (int i = 0; i < randomPixelCount; i++)
            {
                int brightness = random.Next(0, 128);
                color = Color.FromArgb(255, brightness, brightness, 255);
                int x = random.Next(0, tilePixelWidth);
                int y = random.Next(0, tilePixelHeight);
                Point point = new Point(tile.X * tilePixelWidth + x, tile.Y * tilePixelHeight + y);

                int lineDistance = random.Next(tilePixelWidth / (-4), tilePixelWidth / 4);

                if (x + lineDistance >= tilePixelWidth && !planet.GetRightTile(tile).IsWater)
                    continue;
                else if (x + lineDistance < 0 && !planet.GetLeftTile(tile).IsWater)
                    continue;

                Point otherPoint = new Point(point.X + lineDistance, point.Y);

                Line line = new Line(point, otherPoint);

                surface.Draw(line, color);
            }
        }
        #endregion
    }
}
