using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Numerics;
using MCTS.DST.Actions;
using MCTS.DST.Objects;

namespace MCTS.DST.WorldModels {
    public class WorldModel {
        protected Vector2 walterPosition = new Vector2(); 
        protected Dictionary<string, List<PickableObject>> _knownObjects;

        public WorldModel() {
            _knownObjects = new Dictionary<string, List<PickableObject>>();
        }

        public WorldModel(WorldModel wm)
        {
            _knownObjects = wm._knownObjects;
        }

        public virtual WorldModel GenerateChildWorldModel() {
            return new WorldModel(this);
        }

        public virtual Actions.Action[] GetExecutableActions() {
            return null;
        }

        public virtual Actions.Action GetNextAction() {
            return null;
        }

        public virtual bool IsTerminal() {
            return true;
        }

        internal Vector3 GetWalterPosition() {
            throw new NotImplementedException();
        }

        public virtual float GetScore() {
            return 0.0f;
        }

        public virtual int GetNextPlayer() {
            return 0;
        }

        public virtual void CalculateNextPlayer() {
        }

        public void RemoveObject(string guid)
        {
            _knownObjects.Remove(guid);
        }

        public Vector3 GetPosition(string guid)
        {
            return _knownObjects[guid].Position;
        }
    }

    public class Stats {

    }
 
}
