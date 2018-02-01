using System;
using MCTS.DST.Actions.Recipes;
using MCTS.DST.WorldModels;
using WellFormedNames;

namespace MCTS.DST.Actions
{
    public class EatAction : Action
    {
        //Action(EAT, -, -, -, -) = [target]


        public EatAction(string guid, string entityType) : base("EAT", guid, entityType)
        {
        }

        //public Action(string actionName) : this(actionName, "-", "-"){ }

        public override string GetXmlName()
        {
            return "EAT ";
        }
          
        public override int GetDuration(WorldModel worldModel) {
            return base.GetDuration(worldModel)+1; 
        }
        public override bool CanExecute(WorldModel worldModel)
        {
            //Has Slot in Inventory
            return base.CanExecute(worldModel) && worldModel.Walter.InventoryHasObject(EntityType);
        }



        public override void ApplyActionEffects(WorldModel worldModel)
        {
            base.ApplyActionEffects(worldModel);
            worldModel.Walter.RemoveFromInventory(EntityType);
            worldModel.Walter.Eat(EntityType);
        }

        public override string GetDstInterpretableAction() {
            //return "Action("+Name+", "+invobject + ", " + posx + ", " + posz + ", " + recipe+")";
            return "Action("+Name+", "+"-"+", -, -, -)";
        }

        public override string GetTarget()
        {
            return TargetGuid;
        }
    }
}