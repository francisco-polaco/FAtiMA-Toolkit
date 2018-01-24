using System;
using MCTS.DST.WorldModels;
using MCTS.Math;

namespace MCTS.DST.Actions
{
    internal class CollectAction : PickupAction {

        public CollectAction(Vector2i position, string guid, string entityType) : base(position, guid, entityType)
        {
        }

        public override void ApplyActionEffects(WorldModel worldModel)
        {
            Console.WriteLine("Apply Collect");
            base.ApplyActionEffects(worldModel);
            worldModel.RemoveCollectableObject(EntityType, TargetGuid);
        }

        public override string GetDstInterpretableAction() {
                return "Action(" + "PICK" + ", -, -, -, -)";
        }
    }

    //internal class PickableObject : GameObject
    //{
    //    public PickableObject(string guid) : base(guid)
    //    {
    //    }
    //}
}