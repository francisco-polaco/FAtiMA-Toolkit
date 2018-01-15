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

        public override void ApplyActionEffects(WorldModel worldModel)
        {
            //worldModel.RemovePickableObject(_guid);
            worldModel.walkedDistanced(TargetPosition);
        }

        public override string GetDstInterpretableAction()
        {
            if (TargetGuid == null)
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