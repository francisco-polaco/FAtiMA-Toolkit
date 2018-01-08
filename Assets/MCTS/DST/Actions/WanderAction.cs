using MCTS.DST.WorldModels;
using MCTS.Math;

namespace MCTS.DST.Actions
{
    internal class WanderAction : Action
    {
        //private readonly string _guid;
        private readonly Vector2d _position;

        public WanderAction(Vector2d position) : base("WALKTO")
        {
           // _guid = guid;
            this._position = position;
        }

        public override void ApplyActionEffects(WorldModel worldModel)
        {
            //worldModel.RemoveObject(_guid);
            worldModel.walkedDistanced(_position);
        }

        public override string getDSTInterpretableAction()
        {
            return "Action(" + Name + ", -, " + _position.x + ", " + _position.y + ", -)";
        }

    }

    //internal class PickableObject : GameObject
    //{
    //    public PickableObject(string guid) : base(guid)
    //    {
    //    }
    //}
}