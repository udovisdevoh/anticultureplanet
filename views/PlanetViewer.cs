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
        /// <summary>
        /// Refresh planet display and mark as IsNeedRefresh = false
        /// </summary>
        /// <param name="planet">planet</param>
        internal abstract void Update(Planet planet);
    }
}
