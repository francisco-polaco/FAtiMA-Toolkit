using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCTS.DST.Actions.Recipes {
    class RecipesManager {
        public readonly List<Recipe> CraftRecipes = new List<Recipe>();
        public RecipesManager()
        {
            CraftRecipes.Add(new Pickaxe());
            CraftRecipes.Add(new Torch());
        }

    }
}
