using MCTS.MCTS.WorldModels;

namespace MCTS.MCTS.Actions
{
    class PickAction : Action
    {
        private string _guid;
        public PickAction(string name, string guid) : base(name)
        {
            _guid = guid;
        }

        public override void ApplyActionEffects(WorldModel worldModel)
        {
            worldModel.RemoveObject(_guid);
        }

        public override bool CanExecute(WorldModel worldModel)
        {

            return base.CanExecute(worldModel);
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

    internal class PickableObject : GameObject
    {
        public PickableObject(string guid) : base(guid)
        {
        }
    }
}