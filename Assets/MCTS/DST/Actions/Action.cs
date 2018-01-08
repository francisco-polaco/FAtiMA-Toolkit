using System;
using MCTS.DST.WorldModels;
using MCTS.Math;
using WellFormedNames;

namespace MCTS.DST.Actions
{
    public class Action
    {
        private static int ActionID;
        public string xmlName = "";

        public Action(string name)
        {
            ID = ActionID++;
            Name = name;
        }

        public string Name { get; set; }
        public int ID { get; set; }
        public double Duration { get; set; }

        public virtual double GetDuration()
        {
            return Duration;
        }

        public virtual double GetDuration(WorldModel worldModel)
        {
            //TODO FIXME 
            //TODO FIXME 
            var guid = "1";
            //TODO FIXME 
            //TODO FIXME 

            var objPosition = worldModel.GetPosition(guid);
            //var walterPosition = worldModel.GetWalterPosition();


            return worldModel.getRealDistanceToWalter(objPosition);
        }

        public virtual bool CanExecute(WorldModel worldModel)
        {
            return true;
        }

        public virtual bool CanExecute()
        {
            return true;
        }

        public virtual void Execute()
        {
        }

        public virtual void ApplyActionEffects(WorldModel worldModel)
        {
        }

        public virtual string getDSTInterpretableAction() {
            //            return "Action("+Name+", "+invobject + ", " + [posx] + ", " + [posz] + ", " + [recipe]+")";
            return "Action("+Name+", -, -, -, -)";
        }

        public virtual string getTarget() {
            return "-";
        }
    }

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