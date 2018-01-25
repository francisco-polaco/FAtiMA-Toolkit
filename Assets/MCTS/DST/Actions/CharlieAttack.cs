using MCTS.DST.WorldModels;

namespace MCTS.DST.Actions
{
    class CharlieAttack : Action
    {
        public CharlieAttack() : base("CHARLIE_ATTACK")
        {
        }

        public override void ApplyActionEffects(WorldModel worldModel)
        {
            worldModel.Walter.Sanity -= 20;
            worldModel.Walter.ReceiveDamage(100);
            //base.ApplyActionEffects(worldModel);
        }

        public override bool CanExecute(WorldModel worldModel) {
            throw new System.NotImplementedException();
        }

        public override string GetDstInterpretableAction() {
            throw new System.NotImplementedException();
        }

        public override int GetDuration(WorldModel worldModel) {
            throw new System.NotImplementedException();
        }

        public override string GetTarget() {
            throw new System.NotImplementedException();
        }

        public override string GetXmlName()
        {
            throw new System.NotImplementedException();
        }
    }
}