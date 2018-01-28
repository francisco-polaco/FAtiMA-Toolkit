using System;
using System.Collections.Generic;
using MCTS.DST.Actions.Recipes;
using MCTS.DST.Objects;
using MCTS.DST.WorldModels;
using MCTS.Math;
using Utilities;
using WellFormedNames;

namespace MCTS.DST.Actions
{
    public class BuildAction : Action
    {
        //Action(BUILD, -, [posx], [posz], [recipe]) = [target]

        private Recipe ToBuild;
        private ClockDate actionTimestamp;
        private Vector2i walterPosition;
        public BuildAction(Recipe toBuild) : base("BUILD","-",toBuild.PrefabName)
        {
            ToBuild = toBuild;
        }

        //public Action(string actionName) : this(actionName, "-", "-"){ }

        public override string GetXmlName()
        {
            return "BUILD " + ToBuild.PrefabName; 
        }

        public override int GetDuration(WorldModel worldModel) {
            return 1;
        }

        public override bool CanExecute(WorldModel worldModel)
        {
            //Has Slot in Inventory
            if (worldModel.Walter.IsInventoryFull(ToBuild.PrefabName,1))
            {
                return false;
            }
            foreach (var ingredient in ToBuild.Ingredients)
            {
                if (!worldModel.Walter.InventoryHasObject(ingredient.Item1, ingredient.Item2))
                {
                    return false;
                }
            }
            return true;
        }


        //Should not be needed
        //public virtual bool CanExecute() {
        //    return true;
        //}

        //public virtual void Execute()
        //{
        //}

        public override void ApplyActionEffects(WorldModel worldModel)
        {

            actionTimestamp = worldModel.clock.GetTimestamp();
            walterPosition = worldModel.Walter.WalterPosition;
            foreach (var ingredient in ToBuild.Ingredients)
            {
                worldModel.Walter.RemoveFromInventory(ingredient.Item1, ingredient.Item2);
            }
            worldModel.Walter.AddToInventory(EntityType);
            if(worldModel.Walter.EquipedObject == EquipableObject.None)
            {
                worldModel.Walter.Equip(EntityType);
            }

            base.ApplyActionEffects(worldModel);
        }

        public override string GetDstInterpretableAction() {
            //return "Action("+Name+", "+invobject + ", " + posx + ", " + posz + ", " + recipe+")";
            return "Action("+Name+", -, -, -, "+ToBuild.PrefabName+")";
        }

        public override List<Pair<string, string>> SaveToKb()
        {
            var toReturn = base.SaveToKb();
            var timeToAdd = 0;
            if (ToBuild.PrefabName.Equals("campfire"))
            {
                timeToAdd = 180;
                //180 sec
            } else if(ToBuild.PrefabName.Equals("firepit"))
            {
                timeToAdd = 135;
                //135 sec
            } else if (ToBuild.PrefabName.Equals("torch"))
            {
                timeToAdd = 75;
                //75 seconds
            }

            if (timeToAdd > 0)
            {
               // var belief = new Pair<string, string>();
            }
            return toReturn;
        }
    }
}