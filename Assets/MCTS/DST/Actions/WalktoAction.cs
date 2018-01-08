using System;
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
            //worldModel.RemoveObject(_guid);
            worldModel.walkedDistanced(_position);
        }

        public override string getDSTInterpretableAction()
        {
            return "Action(" + Name + ", -, " + _position.x + ", " + _position.y + ", -)";
        }

    }

    class WanderAction : WalktoAction
    {
        public WanderAction(Vector2i walterPosition) : base(walterPosition)
        {
            var r = new Random();
            var x = r.Next(100) >= 50 ? r.Next(10) : -r.Next(10);
            var y = r.Next(100) >= 50 ? r.Next(10) : -r.Next(10);
            _position = walterPosition + new Vector2i(x, y);
        }
    }

    //internal class PickableObject : GameObject
    //{
    //    public PickableObject(string guid) : base(guid)
    //    {
    //    }
    //}
}