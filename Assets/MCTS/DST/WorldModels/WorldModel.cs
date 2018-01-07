using System;
using System.Collections.Generic;
using MCTS.DST.Objects;
using MCTS.Math;
using Action = MCTS.DST.Actions.Action;

namespace MCTS.DST.WorldModels
{
    public class WorldModel
    {
        protected Dictionary<string, List<PickableObject>> _knownObjects;
        protected Vector2d walterPosition = new Vector2d();

        public WorldModel()
        {
            _knownObjects = new Dictionary<string, List<PickableObject>>();
        }

        public WorldModel(WorldModel wm)
        {
            _knownObjects = wm._knownObjects;
        }

        public virtual WorldModel GenerateChildWorldModel()
        {
            return new WorldModel(this);
        }

        public virtual Action[] GetExecutableActions()
        {
            return null;
        }

        public virtual Action GetNextAction()
        {
            return null;
        }

        public virtual bool IsTerminal()
        {
            return true;
        }

        internal Vector2d GetWalterPosition()
        {
            throw new NotImplementedException();
        }

        public virtual float GetScore()
        {
            return 0.0f;
        }

        public virtual int GetNextPlayer()
        {
            return 0;
        }

        public virtual void CalculateNextPlayer()
        {
        }

        public void RemoveObject(string guid)
        {
            _knownObjects.Remove(guid);
        }

        public Vector2d GetPosition(string guid)
        {
            return new Vector2d(0, 0);
            //return _knownObjects[guid].Position;
        }
    }

    public class Stats
    {
    }
}