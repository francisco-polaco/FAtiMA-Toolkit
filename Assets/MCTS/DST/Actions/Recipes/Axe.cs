﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utilities;

namespace MCTS.DST.Actions.Recipes {
    class Axe : Recipe{
        public Axe()
        {
            PrefabName = "axe";
            Ingredients = new Pair<string, int>[] {new Pair<string, int>("flint", 1), new Pair<string, int>("twigs", 1) };

        }
    }
}
