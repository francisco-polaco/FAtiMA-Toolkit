using System;
using MCTS.DST.WorldModels;
using MCTS.Math;

namespace MCTS.DST.Actions
{
    internal class PickupAction : WalktoAction
    {

        public PickupAction(Vector2i position, string guid, string entityType) : base(position, guid, entityType)
        {
        }

        public override void ApplyActionEffects(WorldModel worldModel)
        {

            base.ApplyActionEffects(worldModel);
            worldModel.RemovePickableObject(EntityType, TargetGuid);
            //DO NOT SEND GUID TO INVENTORY, ALWAYS ENTITYTYPE
            worldModel.Walter.AddToInventory(EntityType);
        }


        public override bool CanExecute(WorldModel worldModel)
        {
            return base.CanExecute(worldModel) && !worldModel.Walter.IsInventoryFull(base.EntityType);
        }

        //public override bool CanExecute()
        //{
        //    return base.CanExecute();
        //}

    
        public override int GetDuration(WorldModel worldModel)
        {
            return base.GetDuration(worldModel);
        }
         
        public override string GetDstInterpretableAction() {
                return "Action(" + "PICKUP" + ", -, -, -, -)";
        }
        public override string GetXmlName() {
            return "PickUp " + (EntityType ?? "") + " -> " + TargetPosition;
        }

    }

    //internal class PickableObject : GameObject
    //{
    //    public PickableObject(string guid) : base(guid)
    //    {
    //    }
    //}
}