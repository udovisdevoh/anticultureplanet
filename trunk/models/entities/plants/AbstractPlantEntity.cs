using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AntiCulturePlanet
{
    /// <summary>
    /// Represents seeds, plants and trees
    /// </summary>
    abstract class AbstractPlantEntity : AbstractEntity
    {
        protected override PositionCriteria BuildPositionCriteria()
        {
            return PositionCriteria.Ground;
        }

        protected override ZIndexLayer BuildZIndexLayer()
        {
            return ZIndexLayer.OnFloor;
        }

        protected override bool BuildIsKeepSpriteOfPreviousEntity()
        {
            return false;
        }

        protected override bool BuildIsKeepMassOfPreviousEntity()
        {
            return false;
        }

        protected override bool BuildIsKeepSizeOfPreviousEntity()
        {
            return false;
        }

        protected override bool BuildIsAffectedByCollision()
        {
            return true;
        }
    }
}
