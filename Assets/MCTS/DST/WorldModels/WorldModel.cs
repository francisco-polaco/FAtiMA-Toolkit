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
        protected Dictionary<string, List<DSTObject>> _knownPickableObjects;
        protected Dictionary<string, List<DSTObject>> _knownCollectableObjects;
        protected Dictionary<string, List<DSTObject>> _knownChopableObjects;
        protected Dictionary<string, List<DSTObject>> _knownHammerableObjects;
        protected Dictionary<string, List<DSTObject>> _knownDiggableObjects;
        protected Dictionary<string, List<DSTObject>> _knownMineableObjects;
        protected Dictionary<string, List<DSTObject>> _knownInInventoryObjects;

        private int _actionIndex = 0;
        private Action[] _possibleActions = null;


        public Character Walter { get; } = new Character();


        public WorldModel()
        {
            _knownPickableObjects = new Dictionary<string, List<DSTObject>>();
            _knownCollectableObjects = new Dictionary<string, List<DSTObject>>();
            _knownChopableObjects = new Dictionary<string, List<DSTObject>>();
            _knownHammerableObjects = new Dictionary<string, List<DSTObject>>();
            _knownDiggableObjects = new Dictionary<string, List<DSTObject>>();
            _knownMineableObjects = new Dictionary<string, List<DSTObject>>();
            _knownInInventoryObjects = new Dictionary<string, List<DSTObject>>();

        }

        public WorldModel(WorldModel wm)
        {
            _actionIndex = 0;
            _knownPickableObjects = wm._knownPickableObjects;
            _knownCollectableObjects = wm._knownCollectableObjects;
            _knownChopableObjects = wm._knownChopableObjects;
            _knownHammerableObjects = wm._knownHammerableObjects;
            _knownDiggableObjects = wm._knownDiggableObjects;
            _knownMineableObjects = wm._knownMineableObjects;
            _knownInInventoryObjects = wm._knownInInventoryObjects;

            Walter.WalterPosition = wm.Walter.WalterPosition;
            Walter.EquipedObject = wm.Walter.EquipedObject;
            Console.WriteLine("WorldModel creation: knownObjects size " + _knownPickableObjects.Keys.Count +
                              " Walter position: " + Walter.WalterPosition);
        }

        public virtual WorldModel GenerateChildWorldModel()
        {
            return new WorldModel(this);
        }

        public virtual Action[] GetExecutableActions()
        {
            if (_possibleActions!=null) {
                return _possibleActions;
            }
            calculateActions();
            Console.WriteLine("All possible actions: size " + _possibleActions.Length );
            
            //foreach(var i in possibleActions) {
            //    Console.WriteLine(i.getDSTInterpretableAction() + " "+ i.getTarget());
            //}
            //Console.WriteLine(possibleActions);
            if (_possibleActions.Length == 0)
            {
                // lets wonder a bit
                // TODO AMARAL E VICENTE isto provavelmente nao e assim, mas queria fazer algo mais fixe
                // agr so anda ao calhas e vai para narnia
                Console.WriteLine("no action -> lets wonder");
                return new Action[] { new WanderAction(Walter.WalterPosition) };
            }
            return _possibleActions;
        }

        private void calculateActions()
        {
            //var numberActions = _knownPickableObjects.Keys.Count + _knownChopableObjects.Keys.Count;
            var possibleActions = new List<Action>();

            //_possibleActions = new Action[numberActions];
            //var i = 0;
            foreach (var objHolder in _knownPickableObjects)
            {
                //Console.WriteLine("Action - " + objHolder.Key + " :pos: " + objHolder.Value[0].GetPosition() + " :guid: " + objHolder.Value[0].Guid );
                var actionTempHolder = new PickupAction(objHolder.Value[0].GetPosition(), objHolder.Value[0].Guid, objHolder.Value[0].GetEntityType());
                possibleActions.Add(actionTempHolder);
                //_possibleActions[i] = actionTempHolder;
                //i++;
            }
            foreach (var objHolder in _knownCollectableObjects) {
                //Console.WriteLine("Action - " + objHolder.Key + " :pos: " + objHolder.Value[0].GetPosition() + " :guid: " + objHolder.Value[0].Guid );
                var actionTempHolder = new CollectAction(objHolder.Value[0].GetPosition(), objHolder.Value[0].Guid, objHolder.Value[0].GetEntityType());
                possibleActions.Add(actionTempHolder);
                //_possibleActions[i] = actionTempHolder;
                //i++;
            }
            foreach (var objHolder in _knownChopableObjects)
            {
                //Console.WriteLine(objHolder.Key + " " + objHolder.Value[0].GetEntityType());
                var actionTempHolder = new ChopAction(objHolder.Value[0].GetPosition(),  objHolder.Value[0].Guid, objHolder.Value[0].GetEntityType());
                possibleActions.Add(actionTempHolder);
                //_possibleActions[i] = actionTempHolder;
                //i++;
            }
            foreach (var objHolder in _knownMineableObjects) {
                Console.WriteLine(objHolder.Key + " " + objHolder.Value[0].GetEntityType());
                var actionTempHolder = new MineAction(objHolder.Value[0].GetPosition(), objHolder.Value[0].Guid, objHolder.Value[0].GetEntityType());
                possibleActions.Add(actionTempHolder);

                //_possibleActions[i] = actionTempHolder;
                //i++;
            }

            _possibleActions = possibleActions.ToArray();
        }

        //For Selection
        public virtual Action GetNextAction()
        {
            Action action = null;
            var actions = GetExecutableActions();
            if (_actionIndex < actions.Length) {
                action = actions[_actionIndex];
                _actionIndex++;
                //Console.WriteLine("Action: " + action.getDSTInterpretableAction() + " " + action.getTarget());
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
             return Walter.WalterPosition;
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



        public void RemovePickableObject(string entityType, string guid) {
            RemoveGuidFromKnownObject(_knownPickableObjects,entityType,guid);
        }
        public void RemoveChopableObject(string entityType, string guid) {
            RemoveGuidFromKnownObject(_knownChopableObjects, entityType, guid);
        }
        public void RemoveMineableObject(string entityType, string guid) {
            RemoveGuidFromKnownObject(_knownMineableObjects, entityType, guid);
        }
        public void RemoveCollectableObject(string entityType, string guid) {
            RemoveGuidFromKnownObject(_knownCollectableObjects, entityType, guid);
        }
        //public void RemovePickableObject(string entityType, string guid) {
        //    RemoveGuidFromKnownObject(_knownPickableObjects, entityType, guid);
        //}
        //public void RemovePickableObject(string entityType, string guid) {
        //    RemoveGuidFromKnownObject(_knownPickableObjects, entityType, guid);
        //}
        //public void RemovePickableObject(string entityType, string guid) {
        //    RemoveGuidFromKnownObject(_knownPickableObjects, entityType, guid);
        //}

        private void RemoveGuidFromKnownObject(Dictionary<string, List<DSTObject>> listOfObjects, string entityType, string guid)
        {
            listOfObjects.TryGetValue(entityType, out var lista);
            if (lista == null)
            {
                Console.WriteLine(entityType + " does not exist in here");
                return;
            }
            RemoveFromListGuid(lista, guid);


            if (lista.Count == 0) {
                listOfObjects.Remove(entityType);
            }
        }


        public void RemoveFromListGuid(List<DSTObject> list, string guid)
        {
            for (var i = 0; i < list.Count; i++)
            {
                if (!list[i].Guid.Equals(guid)) continue;
                list.RemoveAt(i);
                break;
            }
        }

        public Vector2i GetPosition(string guid)
        {
            return new Vector2i(0, 0);
            //return _knownPickableObjects[guid][0].GetPosition();
        }

        public int getSquaredDistanceToWalter(Vector2i obj) {
            var x = (int)Walter.WalterPosition.x - obj.x;
            var y = (int)Walter.WalterPosition.y - obj.y;
            return x * x + y * y;
        }

        public double getRealDistanceToWalter(Vector2i obj) {
            return System.Math.Sqrt(getSquaredDistanceToWalter(obj));
        }

        public virtual void walkedDistanced(Vector2i positionWalkedTo) {
            Walter.WalkedDistance += getRealDistanceToWalter(positionWalkedTo);
            Walter.WalterPosition = positionWalkedTo;
        }

        public void EquipObject(EquipableObject equipable)
        {
            Walter.EquipedObject = equipable;
        }

        public bool GotAxeEquiped()
        {
            return true;
            //return _equipedObject == EquipableObject.Axe;
        }

        public void AddPickableObject(DSTObject obj)
        {
            _knownPickableObjects.TryGetValue(obj.GetEntityType(), out var orderedList);
            if (orderedList == null)
            {
                orderedList = new List<DSTObject>();
            }
            orderedList.Insert(0,obj);
        }
    }

    public class Stats
    {
    }
}