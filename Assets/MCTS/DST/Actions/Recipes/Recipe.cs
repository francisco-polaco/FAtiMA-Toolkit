using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MCTS.DST.WorldModels;
using Utilities;

namespace MCTS.DST.Actions.Recipes {
    public abstract class Recipe {
        //entityType,quantity
        public Pair<string,int>[] Ingredients { get; protected set; }
        public string PrefabName { get; protected set; }
    }
}
