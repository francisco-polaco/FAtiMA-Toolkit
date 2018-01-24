using System;
using MCTS.DST.WorldModels;
using WellFormedNames;

namespace MCTS.DST.Actions
{
    public abstract class Action
    {
        private static int _actionId;
        private string xmlName = "";

        public abstract string GetXmlName();
        //{
        //    return GetDstInterpretableAction() + GetTarget();
        //}

        public void SetXmlName(string value) {
            xmlName = value;
        }

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
            //var guid = "1";
            //TODO FIXME 
            //TODO FIXME 
            return 0;//worldModel.getRealDistanceToWalter(objPosition);
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
            Console.WriteLine("Apply Action");
            Console.WriteLine("ACTION: " + this.GetDuration(worldModel));
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