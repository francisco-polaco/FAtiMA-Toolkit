using MCTS.DST.WorldModels;
using MCTS.Math;

namespace MCTS.DST.Actions
{
    internal class WalktoAction : Action
    {
        //private readonly string _guid;
        protected Vector2i _position;

        public WalktoAction(Vector2i position) : base("WALKTO")
        {
           // _guid = guid;
            this._position = position;
        }

        public override void ApplyActionEffects(WorldModel worldModel)
        {
            //worldModel.RemovePickableObject(_guid);
            worldModel.walkedDistanced(_position);
        }

        public override string getDSTInterpretableAction()
        {
            return "Action(" + Name + ", -, " + _position.x + ", " + _position.y + ", -)";
        }

    }

}