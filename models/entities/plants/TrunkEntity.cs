﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AntiCulturePlanet
{
    class TrunkEntity : AbstractEntity
    {
        protected override double BuildDecayTime()
        {
            return 30;
        }

        protected override double BuildSize()
        {
            return 3;
        }

        protected override IEnumerable<AbstractEntity> GetDecayEntities(Planet planet)
        {
            return null;
        }

        protected override EntitySprite BuildEntitySprite()
        {
            return SpriteManager.GetSprite(this.GetType(), false);
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

        protected override bool BuildIsKeepSizeOfPreviousEntity()
        {
            return true;
        }

        protected override bool BuildIsAffectedByCollision()
        {
            return true;
        }
    }
}
