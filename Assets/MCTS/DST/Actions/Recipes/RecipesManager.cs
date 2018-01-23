using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCTS.DST.Actions.Recipes {
    class RecipesManager {
        List<Recipe> craftRecipes = new List<Recipe>();
        public RecipesManager()
        {
            craftRecipes.Add(new Pickaxe());
        }
    }
}
