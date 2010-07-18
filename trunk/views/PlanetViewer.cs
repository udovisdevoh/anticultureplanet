using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AntiCulturePlanet
{
    /// <summary>
    /// Planet's view
    /// </summary>
    internal abstract class PlanetViewer
    {
        #region Internal Methods
        /// <summary>
        /// Refresh planet display and mark as IsNeedRefresh = false
        /// </summary>
        /// <param name="planet">planet</param>
        internal abstract void Update(Planet planet);

        /// <summary>
        /// Move view
        /// </summary>
        /// <param name="tileOffsetX">horizontal tile offset</param>
        /// <param name="tileOffsetY">vertical tile offset</param>
        internal abstract void MoveView(int tileOffsetX, int tileOffsetY);
        #endregion
    }
}
