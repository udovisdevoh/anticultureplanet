using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AntiCulturePlanet
{
    /// <summary>
    /// Represents a small tree
    /// </summary>
    class SmallFruitTreeEntity : AbstractEntity
    {
        protected override double BuildDecayTime()
        {
            return 50;
        }

        protected override double BuildSize()
        {
            return 1;
        }

        protected override double BuildMass()
        {
            return 0.25;
        }

        protected override EntitySprite BuildEntitySprite()
        {
            return SpriteManager.GetSprite(this.GetType());
        }

        protected override IEnumerable<AbstractEntity> GetDecayEntities(Planet planet, EntityCollection entityCollection)
        {
            return new AbstractEntity[] { new TrunkEntity() };
        }

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
