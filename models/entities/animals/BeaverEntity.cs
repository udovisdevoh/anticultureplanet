using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AntiCulturePlanet
{
    /// <summary>
    /// Beaver entity
    /// </summary>
    class BeaverEntity : AbstractAnimalEntity
    {
        #region Static
        private static HashSet<Type> preyTypeList;

        static BeaverEntity()
        {
            preyTypeList = new HashSet<Type>();
            preyTypeList.Add(new LargeCocoTreeEntity().GetType());
            preyTypeList.Add(new LargeFruitTreeEntity().GetType());
            preyTypeList.Add(new LargePineTreeEntity().GetType());
            preyTypeList.Add(new MediumCocoTreeEntity().GetType());
            preyTypeList.Add(new MediumFruitTreeEntity().GetType());
            preyTypeList.Add(new MediumPineTreeEntity().GetType());
            preyTypeList.Add(new SeedCocoTreeEntity().GetType());
            preyTypeList.Add(new SeedPineTreeEntity().GetType());
            preyTypeList.Add(new TrunkEntity().GetType());
        }
        #endregion

        protected override double BuildDecayTime()
        {
            return 600;
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
            return 0.5;
        }

        protected override double BuildMaximumSize()
        {
            return 2.5;
        }

        protected override AbstractAnimalEntity GetOffspringEntity()
        {
            return new BeaverEntity();
        }

        protected override double BuildReproductionCycleTime()
        {
            return 12;
        }

        protected override double BuildMaximumFoodReserve()
        {
            return 12;
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
            return 6;
        }

        protected override double BuildEatingRate()
        {
            return 0.6;
        }
    }
}
