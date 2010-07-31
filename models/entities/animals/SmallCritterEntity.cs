using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AntiCulturePlanet
{
    /// <summary>
    /// Represents some kind of rodant
    /// </summary>
    class SmallCritterEntity : AbstractAnimalEntity
    {
        #region Static
        private static HashSet<Type> preyTypeList;

        static SmallCritterEntity()
        {
            preyTypeList = new HashSet<Type>();
            preyTypeList.Add(new SmallCocoTreeEntity().GetType());
            preyTypeList.Add(new SmallFruitTreeEntity().GetType());
            preyTypeList.Add(new SmallPineTreeEntity().GetType());
            preyTypeList.Add(new PlantEntity().GetType());
            preyTypeList.Add(new FruitEntity().GetType());
            preyTypeList.Add(new SeedPlantEntity().GetType());
        }
        #endregion

        protected override double BuildDecayTime()
        {
            return 200;
        }

        public override HashSet<Type> BuildPreyTypeList()
        {
            return preyTypeList;
        }

        public override HashSet<Type> BuildPredatorTypeList()
        {
            #warning Implement
            return null;
        }

        protected override double BuildDefaultIntegrity()
        {
            return 3;
        }

        public override double BuildSizeAtBirth()
        {
            return 0.5;
        }

        public override double BuildMinimumSizeForReproduction()
        {
            return 1.3;
        }

        public override double BuildMinimumFoodReserveForReproduction()
        {
            return 1;
        }

        public override double BuildMinimumFoodReserveForGrowth()
        {
            return 0.5;
        }

        public override double BuildMaximumSize()
        {
            return 1.5;
        }

        protected override AbstractAnimalEntity GetOffspringEntity()
        {
            return new SmallCritterEntity();
        }

        protected override double BuildReproductionCycleTime()
        {
            return 7;
        }

        protected override double BuildMaximumFoodReserve()
        {
            return 3;
        }

        protected override double BuildGrowthRate()
        {
            return 1.01;
        }

        protected override double BuildSpeed()
        {
            return 0.2;
        }
    }
}
