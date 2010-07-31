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
        #region Fields and parts
        /// <summary>
        /// Random number generator
        /// </summary>
        private Random random = new Random();

        /// <summary>
        /// Remembers water surfaces that were previously created
        /// </summary>
        private Dictionary<int, Surface> liquidWaterSurfaceCache;

        /// <summary>
        /// Remembers ice surfaces that were previously created
        /// </summary>
        private Dictionary<int, Surface> iceSurfaceCache;

        /// <summary>
        /// Remembers soil surfaces that were previously created
        /// </summary>
        private Dictionary<uint, Surface> soilSurfaceCache;

        /// <summary>
        /// Remembers snow surfaces that were previously created
        /// </summary>
        private Dictionary<uint, Surface> snowSurfaceCache;
        #endregion

        #region Constructor
        /// <summary>
        /// Tile viewer low res
        /// </summary>
        public TileViewerLowRes()
        {
            liquidWaterSurfaceCache = new Dictionary<int, Surface>();
            iceSurfaceCache = new Dictionary<int, Surface>();
            soilSurfaceCache = new Dictionary<uint, Surface>();
            snowSurfaceCache = new Dictionary<uint, Surface>();
        }
        #endregion

        #region Internal Methods
        /// <summary>
        /// Build or load tile's surface
        /// </summary>
        /// <param name="tile">tile</param>
        /// <param name="planet">planet</param>
        /// <param name="tilePixelWidth">tile width (pixels)</param>
        /// <param name="tilePixelHeight">tile height (pixels)</param>
        /// <returns>tile's surface</returns>
        internal Surface BuildOrLoadSurface(Tile tile, Planet planet, int tilePixelWidth, int tilePixelHeight)
        {
            if (tile.IsWater)
                return BuildOrLoadLiquidWaterOrIceSurface(tile, planet, tilePixelWidth, tilePixelHeight);
            else
                return BuildOrLoadSoilOrSnowSurface(tile, planet, tilePixelWidth, tilePixelHeight);
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Build or load soil or snow surface
        /// </summary>
        /// <param name="tile">tile</param>
        /// <param name="planet">planet</param>
        /// <param name="tilePixelWidth">tile width (pixels)</param>
        /// <param name="tilePixelHeight">tile height (pixels)</param>
        /// <returns>soil or snow surface</returns>
        private Surface BuildOrLoadSoilOrSnowSurface(Tile tile, Planet planet, int tilePixelWidth, int tilePixelHeight)
        {
            if (tile.Temperature >= 0)
                return BuildOrLoadSoilSurface(tile, planet, tilePixelWidth, tilePixelHeight);
            else
                return BuildOrLoadSnowSurface(tile, planet, tilePixelWidth, tilePixelHeight);
        }

        /// <summary>
        /// Build or load soil surface
        /// </summary>
        /// <param name="tile">tile</param>
        /// <param name="planet">planet</param>
        /// <param name="tilePixelWidth">tile width (pixels)</param>
        /// <param name="tilePixelHeight">tile height (pixels)</param>
        /// <returns>soil surface</returns>
        private Surface BuildOrLoadSnowSurface(Tile tile, Planet planet, int tilePixelWidth, int tilePixelHeight)
        {
            Surface surface;

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

            uint hash = (uint)(red + green * 256 + blue * 65536);

            if (!snowSurfaceCache.TryGetValue(hash, out surface))
            {
                surface = new Surface(tilePixelWidth, tilePixelHeight, Program.BitsPerPixel);
                
                Color color = Color.FromArgb(255, red, green, blue);
                Rectangle rectangle = new Rectangle(0, 0, tilePixelWidth, tilePixelHeight);
                surface.Fill(rectangle, color);

                snowSurfaceCache.Add(hash, surface);
            }
            return surface;
        }

        /// <summary>
        /// Build or load soil surface
        /// </summary>
        /// <param name="tile">tile</param>
        /// <param name="planet">planet</param>
        /// <param name="tilePixelWidth">tile width (pixels)</param>
        /// <param name="tilePixelHeight">tile height (pixels)</param>
        /// <returns>soil surface</returns>
        private Surface BuildOrLoadSoilSurface(Tile tile, Planet planet, int tilePixelWidth, int tilePixelHeight)
        {
            Surface surface;

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

            uint hash = (uint)(red + green * 256 + blue * 65536);

            if (!snowSurfaceCache.TryGetValue(hash, out surface))
            {
                surface = new Surface(tilePixelWidth, tilePixelHeight, Program.BitsPerPixel);

                Color color = Color.FromArgb(255, red, green, blue);
                Rectangle rectangle = new Rectangle(0, 0, tilePixelWidth, tilePixelHeight);
                surface.Fill(rectangle, color);


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
                    Point point = new Point(x, y);

                    int lineDistanceX = random.Next(tilePixelWidth / (-4), tilePixelWidth / 4);
                    int lineDistanceY = random.Next(tilePixelWidth / (-4), tilePixelWidth / 4);

                    Point otherPoint = new Point(point.X + lineDistanceX, point.Y + lineDistanceY);

                    Line line = new Line(point, otherPoint);

                    surface.Draw(line, color);
                }

                snowSurfaceCache.Add(hash, surface);
            }
            return surface;
        }

        /// <summary>
        /// Build or load liquid water or ice surface
        /// </summary>
        /// <param name="tile">tile</param>
        /// <param name="planet">planet</param>
        /// <param name="tilePixelWidth">tile width (pixels)</param>
        /// <param name="tilePixelHeight">tile height (pixels)</param>
        /// <returns>liquid water or ice surface</returns>
        private Surface BuildOrLoadLiquidWaterOrIceSurface(Tile tile, Planet planet, int tilePixelWidth, int tilePixelHeight)
        {
            if (tile.Temperature >= 0)
                return BuildOrLoadLiquidWaterSurface(tile, planet, tilePixelWidth, tilePixelHeight);
            else
                return BuildOrLoadIceSurface(tile, planet, tilePixelWidth, tilePixelHeight);
        }

        /// <summary>
        /// Build or load liquid water surface
        /// </summary>
        /// <param name="tile">tile</param>
        /// <param name="planet">planet</param>
        /// <param name="tilePixelWidth">tile width (pixels)</param>
        /// <param name="tilePixelHeight">tile height (pixels)</param>
        /// <returns>liquid water surface</returns>
        private Surface BuildOrLoadLiquidWaterSurface(Tile tile, Planet planet, int tilePixelWidth, int tilePixelHeight)
        {
            Surface surface;

            int blue = (tile.Altitude - planet.MinAltitude) * 512 / (planet.WaterThresholdAltitude - planet.MinAltitude) - 300;
            blue = Math.Max(0, blue);
            blue = Math.Min(255, blue);

            int hashValue = blue * 10 + random.Next(0, 10);

            if (!liquidWaterSurfaceCache.TryGetValue(hashValue, out surface))
            {
                surface = new Surface(tilePixelWidth, tilePixelHeight, Program.BitsPerPixel);
                Color color;
                int red, green;
                red = green = 0;
                color = Color.FromArgb(255, red, green, blue);
                Rectangle rectangle = new Rectangle(0, 0, tilePixelWidth, tilePixelHeight);
                surface.Fill(rectangle, color);


                int randomPixelCount = (tilePixelWidth * tilePixelHeight) / 16;
                for (int i = 0; i < randomPixelCount; i++)
                {
                    int brightness = random.Next(0, 128);
                    color = Color.FromArgb(255, brightness, brightness, 255);
                    int x = random.Next(0, tilePixelWidth);
                    int y = random.Next(0, tilePixelHeight);
                    Point point = new Point(x, y);

                    int lineDistance = random.Next(tilePixelWidth / (-2), tilePixelWidth / 2);

                    Point otherPoint = new Point(point.X + lineDistance, point.Y);

                    Line line = new Line(point, otherPoint);

                    surface.Draw(line, color);
                }

                liquidWaterSurfaceCache.Add(hashValue, surface);
            }
            return surface;
        }

        /// <summary>
        /// Build or load ice surface
        /// </summary>
        /// <param name="tile">tile</param>
        /// <param name="planet">planet</param>
        /// <param name="tilePixelWidth">tile width (pixels)</param>
        /// <param name="tilePixelHeight">tile height (pixels)</param>
        /// <returns>ice surface</returns>
        private Surface BuildOrLoadIceSurface(Tile tile, Planet planet, int tilePixelWidth, int tilePixelHeight)
        {
            Surface surface;

            int blue = (tile.Altitude - planet.MinAltitude) * 512 / (planet.WaterThresholdAltitude - planet.MinAltitude) - 300;
            blue = Math.Max(0, blue);
            blue = Math.Min(255, blue);

            if (!iceSurfaceCache.TryGetValue(blue, out surface))
            {
                surface = new Surface(tilePixelWidth, tilePixelHeight, Program.BitsPerPixel);
                Color color;
                int red, green;
                red = green = 0;
                color = Color.FromArgb(255, red, green, blue);
                Rectangle rectangle = new Rectangle(0, 0, tilePixelWidth, tilePixelHeight);
                surface.Fill(rectangle, color);
                iceSurfaceCache.Add(blue, surface);
            }
            return surface;
        }
        #endregion
    }
}
