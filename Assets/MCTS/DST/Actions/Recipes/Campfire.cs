using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utilities;

namespace MCTS.DST.Actions.Recipes {
    class Campfire : Recipe{
        public Campfire()
        {
            PrefabName = "campfire";
            Ingredients = new Pair<string, int>[] {new Pair<string, int>("cutgrass", 3), new Pair<string, int>("log", 2) };

        }
    }
}
