using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AntiCulturePlanet
{
    /// <summary>
    /// Tiger entity
    /// </summary>
    class TigerEntity : AbstractAnimalEntity
    {
        #region Static
        private static HashSet<Type> preyTypeList;

        static TigerEntity()
        {
            preyTypeList = new HashSet<Type>();
            preyTypeList.Add(new BeaverEntity().GetType());
            preyTypeList.Add(new RatEntity().GetType());
            preyTypeList.Add(new CorpseEntity().GetType());
        }
        #endregion

        protected override double BuildDecayTime()
        {
            return 1200;
        }

        protected override HashSet<Type> BuildPreyTypeList()
        {
            return preyTypeList;
        }

        protected override HashSet<Type> BuildPredatorTypeList()
        {
            #warning Implement
            return null;
        }

        protected override double BuildSizeAtBirth()
        {
            return 0.5;
        }

        protected override double BuildMinimumSizeForReproduction()
        {
            return 2.1;
        }

        protected override double BuildMinimumFoodReserveForReproduction()
        {
            return 1;
        }

        protected override double BuildMinimumFoodReserveForGrowth()
        {
            return 1;
        }

        protected override double BuildMaximumSize()
        {
            return 2;
        }

        protected override AbstractAnimalEntity GetOffspringEntity()
        {
            return new BeaverEntity();
        }

        protected override double BuildReproductionCycleTime()
        {
            return 3;
        }

        protected override double BuildMaximumFoodReserve()
        {
            return 120;
        }

        protected override double BuildGrowthRate()
        {
            return 1.01;
        }

        protected override double BuildSpeed()
        {
            return 0.3;
        }

        protected override double BuildViewRangeRadius()
        {
            return 20;
        }

        protected override double BuildEatingRate()
        {
            return 2.4;
        }
    }
}
