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
            worldModel.Walter.AddToInventory(TargetGuid);
        }


        public override bool CanExecute(WorldModel worldModel)
        {
            return base.CanExecute(worldModel) && worldModel.Walter.IsInventoryFull(base.EntityType);
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