using System;
using MCTS.DST.WorldModels;
using WellFormedNames;

namespace MCTS.DST.Actions
{
    public class BuildAction : Action
    {
        //Action(BUILD, -, [posx], [posz], [recipe]) = [target]

        public BuildAction(string actionName, string targetGuid, string entityType)
        {
            Id = _actionId++;
            Name = actionName;
            this.TargetGuid = targetGuid;
            this.EntityType = entityType;
        }

        public Action(string actionName) : this(actionName, "-", "-"){ }

        public string Name { get; set; }
        public int Id { get; set; }
        public double Duration { get; set; }

        public virtual double GetDuration()
        {
            return Duration;
        }

        public virtual double GetDuration(WorldModel worldModel)
        {


            return 3;
        }

        public virtual bool CanExecute(WorldModel worldModel)
        {
            return true;
        }


        //Should not be needed
        //public virtual bool CanExecute() {
        //    return true;
        //}

        //public virtual void Execute()
        //{
        //}

        public virtual void ApplyActionEffects(WorldModel worldModel)
        {
        }

        public virtual string GetDstInterpretableAction() {
            //return "Action("+Name+", "+invobject + ", " + posx + ", " + posz + ", " + recipe+")";
            return "Action("+Name+", -, -, -, -)";
        }
        
        public virtual string GetTarget() {
            return "-";
        }
    }
}