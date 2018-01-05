using MCTS.MCTS.Actions;

namespace MCTS.MCTS.WorldModels {
    public class WorldModel {
        public WorldModel() {

        }

        public WorldModel(WorldModel wm) {

        }

        public virtual WorldModel GenerateChildWorldModel() {
            return new WorldModel(this);
        }

        public virtual Action[] GetExecutableActions() {
            return null;
        }

        public virtual Action GetNextAction() {
            return null;
        }

        public virtual bool IsTerminal() {
            return true;
        }

        public virtual float GetScore() {
            return 0.0f;
        }

        public virtual int GetNextPlayer() {
            return 0;
        }

        public virtual void CalculateNextPlayer() {
        }

    }

    public class Stats {

    }
 
}
