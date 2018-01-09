using MCTS.Math;

namespace MCTS.DST.Actions
{
    public abstract class GameObject
    {
        protected string _guid;
        protected Vector2d _position;

        protected GameObject(string guid)
        {
            _guid = guid;
        }

        public string Guid => _guid;
        public Vector2d Position => _position;
    }
}