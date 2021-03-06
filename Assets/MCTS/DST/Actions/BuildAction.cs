﻿using System;
using System.Collections.Generic;
using System.Linq;
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
            //MEGA HACK
            if (EntityType.Equals("campfire"))
            {
                return "BUILD-STRUCTURE " + ToBuild.PrefabName;
            }
            return "BUILD " + ToBuild.PrefabName; 
        }

        public override int GetDuration(WorldModel worldModel) {
            return 1;
        }

        public override bool CanExecute(WorldModel worldModel)
        {

            if (ToBuild.Ingredients.All(ingredient =>
                worldModel.Walter.InventoryHasObject(ingredient.Item1, ingredient.Item2)))
            {
                //MEGA HACK
                if (EntityType.Equals("campfire"))
                {
                    return true;
                }

                var newChar = worldModel.Walter.GenerateClone();
                foreach (var ingredient in ToBuild.Ingredients)
                {
                    newChar.RemoveFromInventory(ingredient.Item1, ingredient.Item2);
                }

                if (newChar.IsObjectEquipable(EntityType))
                {
                    if (newChar.IsUnequiped())
                    {
                        return true;
                    }
                    else if (newChar.CanUnequip())
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }else if (!newChar.IsInventoryFull(EntityType))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
             
            return false;
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
            base.ApplyActionEffects(worldModel);
            actionTimestamp = worldModel.clock.GetTimestamp();
            walterPosition = worldModel.Walter.WalterPosition;
            foreach (var ingredient in ToBuild.Ingredients)
            {
                worldModel.Walter.RemoveFromInventory(ingredient.Item1, ingredient.Item2);
            }

            if (EntityType.Equals("campfire"))
            {
                var lightDST = new DSTObject(Guid.NewGuid().ToString());
                lightDST.IsLightSource = true;
                lightDST.SetEntityType("campfire");
                var WalterPosition = worldModel.Walter.WalterPosition;;
                lightDST.SetPosX(WalterPosition.x);
                lightDST.SetPosZ(WalterPosition.y);

                worldModel.AddLightSource(lightDST);
                return;
            }

             
            if (worldModel.Walter.IsObjectEquipable(EntityType)) {
                if (worldModel.Walter.IsUnequiped())
                {
                    worldModel.Walter.Equip(EntityType);
                }
                else
                {
                    worldModel.Walter.Unequip();
                    worldModel.Walter.Equip(EntityType);
                }
            } 
            else
            {
                worldModel.Walter.AddToInventory(EntityType);
            }

        }

        public override string GetDstInterpretableAction() {
            //return "Action("+Name+", "+invobject + ", " + posx + ", " + posz + ", " + recipe+")";
            if (EntityType.Equals("campfire"))
            {
                return "Action(" + Name + ", -, "+walterPosition.x+","+walterPosition.y+", " + ToBuild.PrefabName + ")";
            }
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
            } else if(ToBuild.PrefabName.Equals("firepit")) {
                timeToAdd = 135;
                //135 sec
            } else if (ToBuild.PrefabName.Equals("torch"))
            {
                timeToAdd = 75;
                //75 seconds
            }

            if (timeToAdd > 0)
            {
               var belief_name = ToBuild.PrefabName + "(" + walterPosition.x +"_" +walterPosition.y+"_"+timeToAdd+")";
                var belief_value = DateTime.Now+"";
               var belief = new Pair<string, string>(belief_name,belief_value) ;
            }
            return toReturn;
        } 
    }
}