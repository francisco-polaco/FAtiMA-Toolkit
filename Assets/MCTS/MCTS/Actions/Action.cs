using System.Collections.Specialized;
using System.Numerics;
using MCTS.MCTS.WorldModels;

namespace MCTS.MCTS.Actions {
    public class Action {
        public string xmlName = "";
        private static int ActionID = 0;
        public string Name { get; set; }
        public int ID { get; set; }
        public float Duration { get; set; }

        public Action(string name) {
            this.ID = Action.ActionID++;
            this.Name = name;

        }

        public virtual float GetDuration() {
            return this.Duration;
        }

        public virtual float GetDuration(WorldModel worldModel)
        {
            //TODO FIXME 
            //TODO FIXME 
            string guid = "1";
            //TODO FIXME 
            //TODO FIXME 

            var objPosition = worldModel.GetPosition(guid);
            var walterPosition = worldModel.GetWalterPosition();
            return (walterPosition - objPosition).Length();
        }

        public virtual bool CanExecute(WorldModel worldModel) {
            return true;
        }

        public virtual bool CanExecute() {
            return true;
        }

        public virtual void Execute() {
        }

        public virtual void ApplyActionEffects(WorldModel worldModel) {
        }
    }

    public abstract class GameObject
    {
        protected string _guid;
        protected Vector3 _position;
        public string Guid => _guid;
        public Vector3 Position => _position;

        protected GameObject(string guid)
        {
            _guid = guid;
        }

    }
}

