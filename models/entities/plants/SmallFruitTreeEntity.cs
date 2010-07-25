using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AntiCulturePlanet
{
    /// <summary>
    /// Represents a small tree
    /// </summary>
    class SmallFruitTreeEntity : AbstractPlantEntity
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
    }
}
