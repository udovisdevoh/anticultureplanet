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
        private static List<Type> preyTypeList;

        static SmallCritterEntity()
        {
            preyTypeList = new List<Type>();
            preyTypeList.Add(new SmallCocoTreeEntity().GetType());
            preyTypeList.Add(new SmallFruitTreeEntity().GetType());
            preyTypeList.Add(new SmallPineTreeEntity().GetType());
            preyTypeList.Add(new PlantEntity().GetType());
            preyTypeList.Add(new FruitEntity().GetType());
        }
        #endregion

        protected override double BuildDecayTime()
        {
            return 200;
        }

        protected override double BuildSize()
        {
            return 1.5;
        }

        protected override double BuildMass()
        {
            return 0.46;
        }

        public override IEnumerable<Type> GetPreyTypeList()
        {
            return preyTypeList;
        }

        public override IEnumerable<Type> GetPredatorTypeList()
        {
            #warning Implement
            throw new NotImplementedException();
        }
    }
}
