using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AntiCulturePlanet
{
    /// <summary>
    /// Represents all minerals
    /// </summary>
    abstract class AbstractMineralEntity : AbstractEntity
    {
        protected override double BuildDecayTime()
        {
            return -1;
        }

        protected override PositionCriteria BuildPositionCriteria()
        {
            return PositionCriteria.Ground;
        }

        protected override ZIndexLayer BuildZIndexLayer()
        {
            return ZIndexLayer.OnFloor;
        }

        protected override IEnumerable<AbstractEntity> GetDecayEntities(Planet planet)
        {
            return null;
        }

        protected override bool BuildIsKeepSpriteOfPreviousEntity()
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

        protected override EntitySprite BuildEntitySprite()
        {
            return SpriteManager.GetSprite(this.GetType(), false);
        }
    }
}
