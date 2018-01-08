using MCTS.DST.WorldModels;
using MCTS.Math;

namespace MCTS.DST.Actions
{
    internal class PickAction : Action
    {
        private readonly string _guid;
        private readonly Vector2i position;

        public PickAction(string guid, Vector2i position) : base("PICK")
        {
            _guid = guid;
            this.position = position;
        }

        public override void ApplyActionEffects(WorldModel worldModel)
        {
            worldModel.RemoveObject(_guid);
            worldModel.walkedDistanced(position);
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

        public override double GetDuration()
        {
            return base.GetDuration();
        }

        public override double GetDuration(WorldModel worldModel)
        {
            return base.GetDuration(worldModel);
        }

        public override string getTarget() {
            return _guid;
        }
    }

    //internal class PickableObject : GameObject
    //{
    //    public PickableObject(string guid) : base(guid)
    //    {
    //    }
    //}
}