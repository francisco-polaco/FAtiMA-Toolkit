using System;
using MCTS.DST.WorldModels;
using MCTS.Math;

namespace MCTS.DST.Actions
{
    internal class WalktoAction : Action
    {
        protected Vector2i TargetPosition;
        private const int Speed = 4;

        public WalktoAction(Vector2i position) : base("WALKTO")
        {
            this.TargetPosition = position;
        }

        public WalktoAction(Vector2i position,string targetGuid,string entityType) : base("WALKTO",targetGuid,entityType) {
            this.TargetPosition = position;
        }

        public override string GetXmlName()
        {
            return "WalktoAction " + (EntityType??"") + " -> " + TargetPosition;
        }
        
        public override int GetDuration(WorldModel worldModel)
        {
            var distance = worldModel.getRealDistanceToWalter(TargetPosition);
            int timeTaken = (int) (distance / Speed);
            return base.GetDuration(worldModel) + timeTaken;
        }

        public override void ApplyActionEffects(WorldModel worldModel)
        {
            Console.WriteLine("Apply WalkToAction");
            base.ApplyActionEffects(worldModel);
            //THE REST
            worldModel.Walter.WalterPosition = TargetPosition;
        }
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