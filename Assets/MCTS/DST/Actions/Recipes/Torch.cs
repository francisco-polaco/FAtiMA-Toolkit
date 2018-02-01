using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utilities;

namespace MCTS.DST.Actions.Recipes {
    public class Torch : Recipe{
        public Torch()
        {
            PrefabName = "torch";
            Ingredients = new Pair<string, int>[] {new Pair<string, int>("twigs", 2), new Pair<string, int>("cutgrass", 2) };

        }
    }
}
