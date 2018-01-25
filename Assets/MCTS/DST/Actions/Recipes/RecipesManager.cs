using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MCTS.DST.WorldModels.CharacterModel;


namespace MCTS.DST.Actions.Recipes {
    class RecipesManager {
        public readonly List<Recipe> CraftRecipes = new List<Recipe>();
        public RecipesManager()
        {

            foreach (var type in StackTable.GetDerivedTypesFor(typeof(Recipe)))
            {
                if (Activator.CreateInstance(type) is Recipe obj)
                    CraftRecipes.Add(obj);
            }
            //CraftRecipes.Add(new Pickaxe());
            //CraftRecipes.Add(new Torch());
        }

    }
}
