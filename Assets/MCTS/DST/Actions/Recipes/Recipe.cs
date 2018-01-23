using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utilities;

namespace MCTS.DST.Actions.Recipes {
    abstract class Recipe {
        //entityType,quantity
        protected Pair<string,int>[] ingredients;
        protected string prefabName;
    }
}
