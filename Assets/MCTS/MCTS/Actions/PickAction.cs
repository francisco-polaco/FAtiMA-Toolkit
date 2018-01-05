using MCTS.MCTS.WorldModels;

namespace MCTS.MCTS.Actions
{
    class PickAction : Action
    {
        public PickAction(string name) : base(name)
        {
        }

        public override void ApplyActionEffects(WorldModel worldModel)
        {
            base.ApplyActionEffects(worldModel);
        }

        public override bool CanExecute(WorldModel woldModel)
        {
            return base.CanExecute(woldModel);
        }

        public override bool CanExecute()
        {
            return base.CanExecute();
        }

        public override void Execute()
        {
            base.Execute();
        }

        public override float GetDuration()
        {
            return base.GetDuration();
        }

        public override float GetDuration(WorldModel worldModel)
        {
            return base.GetDuration(worldModel);
        }
    }
}