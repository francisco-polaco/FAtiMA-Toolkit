using System;
using System.Collections.Generic;
using System.Linq;
using MCTS.DST.Objects;
using MCTS.Math;
using Action = MCTS.DST.Actions.Action;
using MCTS.DST.Actions;
using MCTS.DST.Actions.Recipes;
using MCTS.DST.WorldModels.CharacterModel;

namespace MCTS.DST.WorldModels
{
    public class WorldModel
    {
        private const int CHARLIE_ATTACK_FREQUENCY = 6;//every x segunds

        protected Dictionary<string, List<DSTObject>> _knownPickableObjects;
        protected Dictionary<string, List<DSTObject>> _knownCollectableObjects;
        protected Dictionary<string, List<DSTObject>> _knownChopableObjects;
        protected Dictionary<string, List<DSTObject>> _knownHammerableObjects;
        protected Dictionary<string, List<DSTObject>> _knownDiggableObjects;
        protected Dictionary<string, List<DSTObject>> _knownMineableObjects;
        protected Dictionary<string, List<DSTObject>> _knownInInventoryObjects;

        private int _actionIndex = 0;
        private List<Action> _canExecuteActions = null;
        private Action[] _possibleActions = null;
        private readonly RecipesManager _recipesManager = new RecipesManager();

        public Character Walter { get; }
        public Clock clock { get; }

        public WorldModel()
        {
            _knownPickableObjects = new Dictionary<string, List<DSTObject>>();
            _knownCollectableObjects = new Dictionary<string, List<DSTObject>>();
            _knownChopableObjects = new Dictionary<string, List<DSTObject>>();
            _knownHammerableObjects = new Dictionary<string, List<DSTObject>>();
            _knownDiggableObjects = new Dictionary<string, List<DSTObject>>();
            _knownMineableObjects = new Dictionary<string, List<DSTObject>>();
            _knownInInventoryObjects = new Dictionary<string, List<DSTObject>>();
            Walter = new Character();
            clock = new MCTS.DST.WorldModels.Clock();
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
            Walter = wm.Walter.GenerateClone();
            clock = wm.clock.deepCopy();
            //Walter.WalterPosition = wm.Walter.WalterPosition;
            //Walter.EquipedObject = wm.Walter.EquipedObject;
            //Console.WriteLine("WorldModel creation: knownObjects size " + _knownPickableObjects.Keys.Count + " Walter position: " + Walter.WalterPosition);
        }

        public virtual WorldModel RecycleWorldModel()
        {
            return this;
        }

        public virtual WorldModel GenerateChildWorldModel()
        {
            return new WorldModel(this);
        }

        public virtual Action[] GetExecutableActions()
        {
            //Console.WriteLine("All possible actions: size " + _possibleActions.Length);
            if (_canExecuteActions == null)
            {
                if (_possibleActions == null)
                {
                    calculateActions();
                }

                _canExecuteActions = new List<Action>();
                foreach (Action a in _possibleActions)
                {
                    if (a.CanExecute(this))
                    {
                        _canExecuteActions.Add(a);
                    }
                }

                foreach (Action a in _canExecuteActions.ToArray())
                {
                    Console.WriteLine(a);
                }


                if (_canExecuteActions.ToArray().Length == 0)
                {
                    // lets wonder a bit
                    // TODO AMARAL E VICENTE isto provavelmente nao e assim, mas queria fazer algo mais fixe
                    // agr so anda ao calhas e vai para narnia
                    Console.WriteLine("no action -> lets wonder");
                    _canExecuteActions.Add(new WanderAction(Walter.WalterPosition));
                }
            }

            return _canExecuteActions.ToArray();
        }

        private void calculateActions()
        {
            var possibleActions = new List<Action>();
            //_possibleActions = new Action[numberActions];
            //var i = 0;
            foreach (var objHolder in _knownPickableObjects)
            {
                //Console.WriteLine("Action - " + objHolder.Key + " :pos: " + objHolder.Value[0].GetPosition() + " :guid: " + objHolder.Value[0].Guid );
                var actionTempHolder = new PickupAction(objHolder.Value[0].GetPosition(), objHolder.Value[0].Guid,
                    objHolder.Value[0].GetEntityType());
                possibleActions.Add(actionTempHolder);
                //_possibleActions[i] = actionTempHolder;
                //i++;
            }

            foreach (var objHolder in _knownCollectableObjects)
            {
                //Console.WriteLine("Action - " + objHolder.Key + " :pos: " + objHolder.Value[0].GetPosition() + " :guid: " + objHolder.Value[0].Guid );
                var actionTempHolder = new CollectAction(objHolder.Value[0].GetPosition(), objHolder.Value[0].Guid,
                    objHolder.Value[0].GetEntityType());
                possibleActions.Add(actionTempHolder);
                //_possibleActions[i] = actionTempHolder;
                //i++;
            }

            foreach (var objHolder in _knownChopableObjects)
            {
                //Console.WriteLine(objHolder.Key + " " + objHolder.Value[0].GetEntityType());
                var actionTempHolder = new ChopAction(objHolder.Value[0].GetPosition(), objHolder.Value[0].Guid,
                    objHolder.Value[0].GetEntityType());
                possibleActions.Add(actionTempHolder);
                //_possibleActions[i] = actionTempHolder;
                //i++;
            }

            foreach (var objHolder in _knownMineableObjects)
            {
                Console.WriteLine(objHolder.Key + " " + objHolder.Value[0].GetEntityType());
                var actionTempHolder = new MineAction(objHolder.Value[0].GetPosition(), objHolder.Value[0].Guid,
                    objHolder.Value[0].GetEntityType());
                possibleActions.Add(actionTempHolder);

                //_possibleActions[i] = actionTempHolder;
                //i++;
            }

            foreach (var recipe in _recipesManager.CraftRecipes)
            {
                var actionTempHolder = new BuildAction(recipe);
                possibleActions.Add(actionTempHolder);
            }

            //possibleActions.Add(new StaySamePlace(Walter.WalterPosition));

            _possibleActions = possibleActions.ToArray();
        }

        //For Selection
        public virtual Action GetNextAction()
        {
            //Console.WriteLine("GetNextAction");
            Action action = null;
            var actions = GetExecutableActions();
            foreach (Action a in actions)
            {
                //  Console.WriteLine(a);
            }

            //Console.WriteLine(_actionIndex);
            //Console.WriteLine(actions.Length);
            if (_actionIndex < actions.Length)
            {
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



        public void RemovePickableObject(string entityType, string guid)
        {
            RemoveGuidFromKnownObject(_knownPickableObjects, entityType, guid);
        }

        public void RemoveChopableObject(string entityType, string guid)
        {
            RemoveGuidFromKnownObject(_knownChopableObjects, entityType, guid);
        }

        public void RemoveMineableObject(string entityType, string guid)
        {
            RemoveGuidFromKnownObject(_knownMineableObjects, entityType, guid);
        }

        public void RemoveCollectableObject(string entityType, string guid)
        {
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

        private void RemoveGuidFromKnownObject(Dictionary<string, List<DSTObject>> listOfObjects, string entityType,
            string guid)
        {
            listOfObjects.TryGetValue(entityType, out var lista);
            if (lista == null)
            {
                // Pickable herda de Collectable por isso esta condicao as vezes acontece #HACK
                // Nao precisa de herdar, mas e preciso refazer a classe
                return;
            }

            RemoveFromListGuid(lista, guid);


            if (lista.Count == 0)
            {
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

        public int getSquaredDistanceToWalter(Vector2i obj)
        {
            var x = (int) Walter.WalterPosition.x - obj.x;
            var y = (int) Walter.WalterPosition.y - obj.y;
            return x * x + y * y;
        }

        public double getRealDistanceToWalter(Vector2i obj)
        {
            return System.Math.Sqrt(getSquaredDistanceToWalter(obj));
        }

        public virtual void walkedDistanced(Vector2i positionWalkedTo)
        {
            Walter.WalkedDistance += getRealDistanceToWalter(positionWalkedTo);
            Walter.WalterPosition = positionWalkedTo;
        }

        public void EquipObject(EquipableObject equipable)
        {
            Walter.EquipedObject = equipable;
        }

        public bool GotAxeEquiped()
        {
            return false;
            //return _equipedObject == EquipableObject.Axe;
        }

        public void AddPickableObject(DSTObject obj)
        {
            _knownPickableObjects.TryGetValue(obj.GetEntityType(), out var orderedList);
            if (orderedList == null)
            {
                orderedList = new List<DSTObject>();
            }

            orderedList.Insert(0, obj);
        }

        public void advanceTime(int actionDuration)
        {
            //verify stats are below max values
            Walter.makeSureMax();

            var amounts = this.clock.advanceTime(actionDuration);
            var previousHunger = this.Walter.Hunger;

            this.Walter.Hunger = this.Walter.Hunger - actionDuration * (9.375f/60);
            if (this.Walter.Hunger <= 0)
            {
                var secondsInHunger = (float)actionDuration;
                if (previousHunger > 0)
                {
                    //calcutate time
                    var timeToZero = previousHunger / (9.375f / 60);
                    secondsInHunger = actionDuration - timeToZero;
                }
                this.Walter.Health -= 1.25f * secondsInHunger;
            }

            this.Walter.Sanity = this.Walter.Sanity - amounts[1] * (5.0f / 60);

            //LIGHT STUFF :D
            //assumindo sem fontes de luz
            var noLight = true;
            if (noLight)//aka no light)
            {
                this.Walter.Sanity -= amounts[2] * (50.0f / 60);
                var number_night_attacks = amounts[2] / CHARLIE_ATTACK_FREQUENCY;
                if (number_night_attacks > 0)
                {
                    CharlieAttack attack = new CharlieAttack();
                    while (number_night_attacks > 0) 
                    {
                        attack.ApplyActionEffects(this);
                        number_night_attacks--;
                    }
                }
            } 

            //verify stats are on min (above 0)
            Walter.makeSureMin();
        }

    }

    public class Clock
    {
        private const int SEGMENT_TIME = 30;
        private const int HALF_SEGMENT = 15;
        private const int NUMBER_SEGMENT = 16;
        private const int SECONDS_IN_DAY = NUMBER_SEGMENT * SEGMENT_TIME;

        private static readonly int[][] DAYS_CONFIGURATION = new int[][]
        {
            new int[] {8 * SEGMENT_TIME, 6 * SEGMENT_TIME},
            new int[] {8 * SEGMENT_TIME, 6 * SEGMENT_TIME},
            new int[] {8 * SEGMENT_TIME, 6 * SEGMENT_TIME},
            new int[] {8 * SEGMENT_TIME, 6 * SEGMENT_TIME},
            new int[] {8 * SEGMENT_TIME, 6 * SEGMENT_TIME},
            new int[] {8 * SEGMENT_TIME, 6 * SEGMENT_TIME},
            new int[] {8 * SEGMENT_TIME, 6 * SEGMENT_TIME},
            new int[] {8 * SEGMENT_TIME, 6 * SEGMENT_TIME},
            new int[] {8 * SEGMENT_TIME, 6 * SEGMENT_TIME},
            new int[] {8 * SEGMENT_TIME, 6 * SEGMENT_TIME},
            new int[] {8 * SEGMENT_TIME, 5 * SEGMENT_TIME},
            new int[] {7 * SEGMENT_TIME, 6 * SEGMENT_TIME},
            new int[] {7 * SEGMENT_TIME, 6 * SEGMENT_TIME},
            new int[] {7 * SEGMENT_TIME, 6 * SEGMENT_TIME},
            new int[] {7 * SEGMENT_TIME, 6 * SEGMENT_TIME},
            new int[] {7 * SEGMENT_TIME, 6 * SEGMENT_TIME},
            new int[] {7 * SEGMENT_TIME, 6 * SEGMENT_TIME},
            new int[] {7 * SEGMENT_TIME, 5 * SEGMENT_TIME},
            new int[] {7 * SEGMENT_TIME, 5 * SEGMENT_TIME},
            new int[] {7 * SEGMENT_TIME, 5 * SEGMENT_TIME},
            new int[] {7 * SEGMENT_TIME, 5 * SEGMENT_TIME},
            new int[] {6 * SEGMENT_TIME, 6 * SEGMENT_TIME},
            new int[] {6 * SEGMENT_TIME, 5 * SEGMENT_TIME},
            new int[] {6 * SEGMENT_TIME, 5 * SEGMENT_TIME},
            new int[] {5 * SEGMENT_TIME, 6 * SEGMENT_TIME},
            new int[] {5 * SEGMENT_TIME, 5 * SEGMENT_TIME},
            new int[] {5 * SEGMENT_TIME, 5 * SEGMENT_TIME},
            new int[] {5 * SEGMENT_TIME, 5 * SEGMENT_TIME},
            new int[] {5 * SEGMENT_TIME, 5 * SEGMENT_TIME},
            new int[] {5 * SEGMENT_TIME, 5 * SEGMENT_TIME},
            new int[] {5 * SEGMENT_TIME, 5 * SEGMENT_TIME},
            new int[] {5 * SEGMENT_TIME, 5 * SEGMENT_TIME},
            new int[] {5 * SEGMENT_TIME, 6 * SEGMENT_TIME},
            new int[] {5 * SEGMENT_TIME, 6 * SEGMENT_TIME},
            new int[] {5 * SEGMENT_TIME, 6 * SEGMENT_TIME},
            new int[] {5 * SEGMENT_TIME, 6 * SEGMENT_TIME},
            new int[] {5 * SEGMENT_TIME, 7 * SEGMENT_TIME},
            new int[] {5 * SEGMENT_TIME, 7 * SEGMENT_TIME},
            new int[] {5 * SEGMENT_TIME, 7 * SEGMENT_TIME},
            new int[] {5 * SEGMENT_TIME, 7 * SEGMENT_TIME}
        };

        private Season season = Season.Autaumn;
        private MoonPhase moonPhase = MoonPhase.WaxingNewMoon;
        private DayPhase dayPhase;

        private int _currentDay;

        //DuskStart //_currentDuskStart //_currentNightStart
        //private int[] dayConfiguration = new int[3];


        private int _currentDuskStart; //numeroSegments*30sec
        private int _currentNightStart; //numeroSegments*30sec

        private int _currentSecond;

        public void setTime(int currentTick, int currentDay)
        {
            _currentDay = currentDay;
            _currentSecond = currentTick * SEGMENT_TIME + HALF_SEGMENT;
            setupDayConfiguration(currentDay);
        }

        public Clock deepCopy()
        {
            var toReturn = new Clock();
            toReturn.season = this.season;
            toReturn.moonPhase = this.moonPhase;
            toReturn.dayPhase = this.dayPhase;
            toReturn._currentDay = this._currentDay;
            toReturn._currentDuskStart = this._currentDuskStart;
            toReturn._currentNightStart = this._currentNightStart;
            toReturn._currentSecond = this._currentSecond;

            return toReturn;
        }

        private void setupDayConfiguration(int currentDay)
        {
            if (_currentDay > 40)
            {
                throw new NotImplementedException();
            }

            var day = DAYS_CONFIGURATION[_currentDay];
            _currentDuskStart = day[0];
            _currentNightStart = day[1];
        }

        public int[] advanceTime(int numberSeconds)
        {
            var toReturn = new int[3] {0, 0, 0};

            //var numberDays = numberSeconds / SECONDS_IN_DAY;
            var previous = _currentSecond;
            var nextSec = _currentSecond + numberSeconds;
            _currentSecond = nextSec % SECONDS_IN_DAY;
            while (nextSec > 0)
            {
                if (nextSec > _currentDuskStart)
                {
                    toReturn[0] += _currentDuskStart - previous;
                    previous = _currentDuskStart;
                    dayPhase = DayPhase.Dusk;
                }

                if (nextSec > _currentNightStart)
                {
                    toReturn[1] += _currentNightStart - previous;
                    previous = _currentNightStart;
                    dayPhase = DayPhase.Night;
                }

                if (nextSec > SECONDS_IN_DAY)
                {
                    toReturn[2] += SECONDS_IN_DAY - previous;
                    previous = 0;
                    _currentDay++;
                    //MOON++?
                    //Season++?
                    dayPhase = DayPhase.Day;
                }

                nextSec -= SECONDS_IN_DAY;
            }

            return toReturn;

        }

        //internal array DaysConfig { }
        internal enum Season
        {
            Autaumn
        }

        internal enum MoonPhase
        {
            WaxingNewMoon,
            WaxingCrescent,
            WaxingHalfMoon,
            WaxingGibbous,
            FullMoon,
            WaningGibous,
            WaningHalf,
            WaningCrescent,
            WaningNewMoon
        }

        internal enum DayPhase
        {
            Day,
            Dusk,
            Night
        }
    }
}