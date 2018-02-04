using System;
using System.Collections.Generic;
using MCTS.DST.WorldModels;
using MCTS.Math;
using Utilities;
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

        public virtual int GetDuration(WorldModel worldModel)
        {
            //MCTS time to process
            //TESTING 
            return 15; 
        }

        public virtual bool CanExecute(WorldModel worldModel)
        {
            return true;
        }


        //Should not be needed
        //public virtual bool CanExecute() {
        //    return true;
        //}


        public virtual void ApplyActionEffects(WorldModel worldModel)
        {
            var actionDuration = this.GetDuration(worldModel);
            //Console.WriteLine("Apply Action");
            //Console.WriteLine("ACTION: " + actionDuration);
            worldModel.advanceTime(actionDuration,this);
            worldModel.Walter.WalkedDistance += actionDuration;
        }

        public virtual string GetDstInterpretableAction() {
            //return "Action("+Name+", "+invobject + ", " + posx + ", " + posz + ", " + recipe+")";
            return "Action("+Name+", -, -, -, -)";
        }
        
        public virtual string GetTarget() {
            return TargetGuid;
        }

        public virtual List<Pair<string, string>> SaveToKb()
        {
            return new List<Pair<string, string>>();
        }

        public virtual Vector2i GetTargetPosition() {
            return new Vector2i(Int32.MinValue,Int32.MinValue);
        }
    }
}