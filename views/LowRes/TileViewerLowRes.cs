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
        /// <summary>
        /// Redraw tile and mark as IsNeedRefresh = false
        /// </summary>
        /// <param name="tile">tile</param>
        /// <param name="surface">surface to draw to</param>
        /// <param name="tilePixelWidth">tile's width (pixels)</param>
        /// <param name="tilePixelHeight">tile's height (pixels)</param>
        internal void Update(Tile tile, Surface surface, int tilePixelWidth, int tilePixelHeight)
        {
            Color color = Color.Green;
            if (tile.IsWater)
                color = Color.Blue;
            else if (tile.Altitude > 15)
                color = Color.Yellow;

            Rectangle rectangle = new Rectangle(tile.X * tilePixelWidth, tile.Y * tilePixelHeight, tilePixelWidth, tilePixelHeight);

            surface.Fill(rectangle, color);

            tile.IsNeedRefresh = false;
        }
    }
}
