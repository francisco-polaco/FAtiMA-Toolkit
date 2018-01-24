using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utilities;

namespace MCTS.DST.Actions.Recipes {
    class Pickaxe : Recipe{
        public Pickaxe()
        {
            PrefabName = "pickaxe";
            Ingredients = new Pair<string, int>[] {new Pair<string, int>("flint", 2), new Pair<string, int>("twigs", 2) };

        }
    }
}
