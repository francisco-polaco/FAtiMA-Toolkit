using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCTS.DST.WorldModels.CharacterModel
{
    class StackPickaxe : StackQuantity
    {
        public StackPickaxe()
        {
            PrefabName = "pickaxe";
            StackSize = 1;

        }
    }
}