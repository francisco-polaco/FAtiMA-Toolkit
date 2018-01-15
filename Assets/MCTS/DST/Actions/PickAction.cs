﻿using MCTS.DST.WorldModels;
using MCTS.Math;

namespace MCTS.DST.Actions
{
    internal class PickAction : WalktoAction
    {

        public PickAction(string entityType, string guid, Vector2i position) : base(position, guid, entityType)
        {
        }

        public override void ApplyActionEffects(WorldModel worldModel)
        {
            base.ApplyActionEffects(worldModel);
            worldModel.RemovePickableObject(EntityType, TargetGuid);
            worldModel.Walter.AddInventory(TargetGuid);
        }


        public override bool CanExecute(WorldModel worldModel)
        {
            return base.CanExecute(worldModel) && worldModel.Walter.IsInventoryFull();
        }

        public override bool CanExecute()
        {
            return base.CanExecute();
        }

        public override void Execute()
        {
            base.Execute();
        }

        public override double GetDuration()
        {
            return base.GetDuration();
        }

        public override double GetDuration(WorldModel worldModel)
        {
            return base.GetDuration(worldModel);
        }

        public override string GetDstInterpretableAction() {
                return "Action(" + "PICKUP" + ", -, -, -, -)";
        }
    }

    //internal class PickableObject : GameObject
    //{
    //    public PickableObject(string guid) : base(guid)
    //    {
    //    }
    //}
}