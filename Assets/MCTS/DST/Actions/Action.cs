using System;
using MCTS.DST.WorldModels;
using WellFormedNames;

namespace MCTS.DST.Actions
{
    public class Action
    {
        private static int _actionId;
        public string xmlName = "";
        protected string TargetGuid;
        protected string EntityType;

        public Action(string actionName, string targetGuid, string entityType)
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


        //Should not be needed
        //public virtual bool CanExecute() {
        //    return true;
        //}

        public virtual void Execute()
        {
        }

        public virtual void ApplyActionEffects(WorldModel worldModel)
        {
            worldModel.Walter.WalkedDistance += this.GetDuration(worldModel);
        }

        public virtual string GetDstInterpretableAction() {
            //return "Action("+Name+", "+invobject + ", " + posx + ", " + posz + ", " + recipe+")";
            return "Action("+Name+", -, -, -, -)";
        }
        
        public virtual string GetTarget() {
            return TargetGuid;
        }
    }
}