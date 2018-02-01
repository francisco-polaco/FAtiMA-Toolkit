using System;
using MCTS.DST.Actions.Recipes;
using MCTS.DST.WorldModels;
using WellFormedNames;

namespace MCTS.DST.Actions
{
    public class EquipAction : Action
    {
        //Action(EQUIP, -, -, -, -) = [target]


        public EquipAction(string guid, string entityType) : base("EQUIP", guid, entityType)
        {
        }

        //public Action(string actionName) : this(actionName, "-", "-"){ }

        public override string GetXmlName()
        {
            return "EQUIP ";
        }

        public override int GetDuration(WorldModel worldModel) {
            if(worldModel.Walter.InventoryHasObject(EntityType))
                return base.GetDuration(worldModel);
            else
            {
                
                return int.MaxValue;
            }
        }

        public override bool CanExecute(WorldModel worldModel)
        {
            //Has Slot in Inventory
            //return false;
            return worldModel.Walter.CanEquip(EntityType);
            //CanEquip(EntityType);
        }



        public override void ApplyActionEffects(WorldModel worldModel)
        {
            base.ApplyActionEffects(worldModel);
            worldModel.Walter.Equip(EntityType);
            worldModel.Walter.RemoveFromInventory(EntityType);

        }

        public override string GetDstInterpretableAction() {
            //return "Action("+Name+", "+invobject + ", " + posx + ", " + posz + ", " + recipe+")";
            return "Action("+Name+", "+TargetGuid+", -, -, -)";
        }

        public override string GetTarget()
        {
            return "-";
        }
    }
}