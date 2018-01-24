using MCTS.DST.WorldModels;
using MCTS.Math;

namespace MCTS.DST.Actions
{
    internal class WalktoAction : Action
    {
        protected Vector2i TargetPosition;

        public WalktoAction(Vector2i position) : base("WALKTO")
        {
           // _guid = guid;
            this.TargetPosition = position;
        }

        public WalktoAction(Vector2i position,string targetGuid,string entityType) : base("WALKTO",targetGuid,entityType) {
            this.TargetPosition = position;
        }

        public override double GetDuration(WorldModel worldModel)
        {
            worldModel.Walter.WalterPosition = TargetPosition;
            return worldModel.getRealDistanceToWalter(TargetPosition);
            //return base.GetDuration(worldModel);
        }

        //public override void ApplyActionEffects(WorldModel worldModel)
        //{
        //    //worldModel.RemovePickableObject(_guid);
        //    //worldModel.walkedDistanced(TargetPosition);
        //}

        public override string GetDstInterpretableAction()
        {
            if (TargetGuid == null || TargetGuid != "-")
            {
                return "Action(" + Name + ", -, " + TargetPosition.x + ", " + TargetPosition.y + ", -)";
            }
            else
            {
                return "Action(" + Name + ", -, -, -, -)";
            }
        }

        public override string GetTarget() {
            return TargetGuid ?? "-";
        }

    }

}