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
            int blue = 0;
            int red = (tile.Altitude - planet.WaterThresholdAltitude) * 255 / (planet.MaxAltitude - planet.WaterThresholdAltitude);
            red += tile.Temperature - planet.MinTemperature;
           

            if (tile.Temperature < 0)
            {
                blue = 255;
                red += 64;
                green += 64;
            }

            if (red < 0)
                red = 0;
            if (red > 255)
                red = 255;

            if (green < 0)
                green = 0;
            if (green > 255)
                green = 255;

            Color color = Color.FromArgb(255, red, green, blue);

            Rectangle rectangle = new Rectangle(tile.X * tilePixelWidth, tile.Y * tilePixelHeight, tilePixelWidth, tilePixelHeight);
            surface.Fill(rectangle, color);


            if (tile.Temperature >= 0)
            {
                int randomPixelCount = (tilePixelWidth * tilePixelHeight) / 8;
                for (int i = 0; i < randomPixelCount; i++)
                {
                    int redLine = red + random.Next(-32, 32);
                    int greenLine = green + random.Next(-32, 32);

                    redLine = Math.Max(0, redLine);
                    greenLine = Math.Max(0, greenLine);
                    redLine = Math.Min(255, redLine);
                    greenLine = Math.Min(255, greenLine);

                    color = Color.FromArgb(255, redLine, greenLine, blue);
                    int x = random.Next(0, tilePixelWidth);
                    int y = random.Next(0, tilePixelHeight);
                    Point point = new Point(tile.X * tilePixelWidth + x, tile.Y * tilePixelHeight + y);

                    int lineDistanceX = random.Next(tilePixelWidth / (-4), tilePixelWidth / 4);
                    int lineDistanceY = random.Next(tilePixelWidth / (-4), tilePixelWidth / 4);

                    Point otherPoint = new Point(point.X + lineDistanceX, point.Y + lineDistanceY);

                    Line line = new Line(point, otherPoint);

                    surface.Draw(line, color);
                }
            }
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
            int blue = (tile.Altitude - planet.MinAltitude) * 512 / (planet.WaterThresholdAltitude - planet.MinAltitude) - 300;

            blue = Math.Max(0, blue);
            blue = Math.Min(255, blue);

            Color color;

            int red, green;
            red = green = 0;

            color = Color.FromArgb(255, red, green, blue);
            Rectangle rectangle = new Rectangle(tile.X * tilePixelWidth, tile.Y * tilePixelHeight, tilePixelWidth, tilePixelHeight);
            surface.Fill(rectangle, color);

            if (tile.Temperature >= 0)
            {
                int randomPixelCount = (tilePixelWidth * tilePixelHeight) / 16;
                for (int i = 0; i < randomPixelCount; i++)
                {
                    int brightness = random.Next(0, 128);
                    color = Color.FromArgb(255, brightness, brightness, 255);
                    int x = random.Next(0, tilePixelWidth);
                    int y = random.Next(0, tilePixelHeight);
                    Point point = new Point(tile.X * tilePixelWidth + x, tile.Y * tilePixelHeight + y);

                    int lineDistance = random.Next(tilePixelWidth / (-2), tilePixelWidth / 2);

                    Point otherPoint = new Point(point.X + lineDistance, point.Y);

                    Line line = new Line(point, otherPoint);

                    surface.Draw(line, color);
                }
            }
        }
        #endregion
    }
}
