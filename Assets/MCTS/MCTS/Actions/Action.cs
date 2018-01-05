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

        public virtual float GetDuration(WorldModel worldModel) {
            return this.Duration;
        }

        public virtual bool CanExecute(WorldModel woldModel) {
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
}

