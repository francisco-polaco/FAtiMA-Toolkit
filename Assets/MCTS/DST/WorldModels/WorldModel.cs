using System;
using System.Collections.Generic;
using System.Linq;
using MCTS.DST.Objects;
using MCTS.Math;
using Action = MCTS.DST.Actions.Action;
using MCTS.DST.Actions;

namespace MCTS.DST.WorldModels
{
    public class WorldModel
    {
        protected Dictionary<string, List<PickableObject>> _knownPickableObjects;
        protected Vector2i walterPosition = new Vector2i();
        private int actionIndex = 0;
        private bool possibleActionsCalculated = false;
        private Action[] possibleActions = null;
        protected double walkedDistance = 0;
        protected EquipableObject _equipedObject = EquipableObject.None;


        public WorldModel()
        {
            _knownPickableObjects = new Dictionary<string, List<PickableObject>>();
        }

        public WorldModel(WorldModel wm)
        {
            actionIndex = 0;
            _knownPickableObjects = wm._knownPickableObjects;
            walterPosition = wm.walterPosition;
            _equipedObject = wm._equipedObject;
            Console.WriteLine("WorldModel creation: knownObjects size " + _knownPickableObjects.Keys.Count + " Walter position: " + walterPosition);
        }

        public virtual WorldModel GenerateChildWorldModel()
        {
            return new WorldModel(this);
        }

        public virtual Action[] GetExecutableActions()
        {
            if (possibleActions!=null) {
                return possibleActions;
            }
            calculateActions();
            Console.WriteLine("All possible actions: size " + possibleActions.Length );
            
            foreach(var i in possibleActions) {
                Console.WriteLine(i.getDSTInterpretableAction() + " "+ i.getTarget());
            }
            Console.WriteLine(possibleActions);
            if (possibleActions.Length == 0)
            {
                // lets wonder a bit
                // TODO AMARAL E VICENTE isto provavelmente nao e assim, mas queria fazer algo mais fixe
                // agr so anda ao calhas e vai para narnia
                Console.WriteLine("no action lets wonder");
                return new Action[] { new WanderAction(walterPosition) };
            }
            return possibleActions;
        }

        private void calculateActions() {
             
            possibleActions = new Action[_knownPickableObjects.Keys.Count];
            var i = 0;
            foreach (var objHolder in _knownPickableObjects) {
                Console.WriteLine("Action - " + objHolder.Key + " :pos: " + objHolder.Value[0].GetPosition() + " :guid: " + objHolder.Value[0].Guid );
                var actionTempHolder = new PickAction(objHolder.Value[0].Guid, objHolder.Value[0].GetPosition());
                possibleActions[i] = actionTempHolder;
                i++;
            }
        }

        //For Selection
        public virtual Action GetNextAction()
        {
            Action action = null;
            var actions = GetExecutableActions();
            if (actionIndex < actions.Length) {
                action = actions[actionIndex];
                actionIndex++;
                Console.WriteLine("Action: " + action.getDSTInterpretableAction() + " " + action.getTarget());
            }
            return action;

        }

        //Stops Playout
        public virtual bool IsTerminal()
        {
            return false;
        }

        internal Vector2i GetWalterPosition()
        {
             return walterPosition;
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

        public void RemovePickableObject(string guid)
        {
            _knownPickableObjects.Remove(guid);
        }

        public Vector2i GetPosition(string guid)
        {
            return new Vector2i(0, 0);
            //return _knownPickableObjects[guid].Position;
        }

        public int getSquaredDistanceToWalter(Vector2i obj) {
            var x = (int)walterPosition.x - obj.x;
            var y = (int)walterPosition.y - obj.y;
            return x * x + y * y;
        }

        public double getRealDistanceToWalter(Vector2i obj) {
            return System.Math.Sqrt(getSquaredDistanceToWalter(obj));
        }

        public virtual void walkedDistanced(Vector2i positionWalkedTo) {
            walkedDistance += getRealDistanceToWalter(positionWalkedTo);
            walterPosition = positionWalkedTo;
        }

        public void EquipObject(EquipableObject equipable)
        {
            _equipedObject = equipable;
        }

        public bool GotAxeEquiped()
        {
            return _equipedObject == EquipableObject.Axe;
        }

        public void AddPickableObject(PickableObject obj)
        {
            _knownPickableObjects[obj.GetEntityType()].Add(obj);
        }
    }

    public class Stats
    {
    }
}