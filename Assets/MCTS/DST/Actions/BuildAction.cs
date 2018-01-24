using System;
using MCTS.DST.Actions.Recipes;
using MCTS.DST.WorldModels;
using WellFormedNames;

namespace MCTS.DST.Actions
{
    public class BuildAction : Action
    {
        //Action(BUILD, -, [posx], [posz], [recipe]) = [target]

        private Recipe ToBuild;

        public BuildAction(Recipe toBuild) : base("BUILD","-",toBuild.PrefabName)
        {
            ToBuild = toBuild;
        }

        //public Action(string actionName) : this(actionName, "-", "-"){ }

        public override string GetXmlName()
        {
            return "BUILD " + ToBuild.PrefabName; 
        }

        public override int GetDuration(WorldModel worldModel) {
            //TODO
            return 0;
        }

        public override bool CanExecute(WorldModel worldModel)
        {
            //Has Slot in Inventory
            if (worldModel.Walter.IsInventoryFull(ToBuild.PrefabName,1))
            {
                return false;
            }
            foreach (var ingredient in ToBuild.Ingredients)
            {
                if (!worldModel.Walter.InventoryHasObject(ingredient.Item1, ingredient.Item2))
                {
                    return false;
                }
            }
            return true;
        }


        //Should not be needed
        //public virtual bool CanExecute() {
        //    return true;
        //}

        //public virtual void Execute()
        //{
        //}

        public override void ApplyActionEffects(WorldModel worldModel)
        {
            base.ApplyActionEffects(worldModel);
        }

        public override string GetDstInterpretableAction() {
            //return "Action("+Name+", "+invobject + ", " + posx + ", " + posz + ", " + recipe+")";
            return "Action("+Name+", -, -, -, "+ToBuild.PrefabName+")";
        }
    }
}