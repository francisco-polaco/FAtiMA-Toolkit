﻿using System;
using System.Collections.Generic;
using KnowledgeBase;
using MCTS.DST.Actions;
using MCTS.DST.Objects;
using Utilities;
using WellFormedNames;
using System.Linq;
using MCTS.DST.Objects.Fire;

namespace MCTS.DST.WorldModels
{
    public class CurrentWorldModel : FutureWorldModel
    {
        public static string guid;

        //Key is GUID
        private readonly Dictionary<string, DSTObject>
            _temporaryHolders = new Dictionary<string, DSTObject>();

        private List<GameObject> _woodObjects = new List<GameObject>();

        public CurrentWorldModel(KB knowledgeBase)
        {
            //guid = null;
            try {
                var walterZ = knowledgeBase.AskProperty((Name)"PosZ(Walter)");
                updateWalterZ(walterZ.ToString());
                var walterX = knowledgeBase.AskProperty((Name)"PosX(Walter)");
                updateWalterX(walterX.ToString());
#if GOOD_FATIMA_PARSE
                Console.WriteLine("aaaaheya!");
                var array = knowledgeBase.AskPossibleProperties((Name) "Entity([x],[y])", Name.SELF_SYMBOL, null)
                    .SelectMany(p => p.Item2).ToArray();
                foreach (var a in array) {
                    Console.WriteLine("A - " + a.ToString());

                    foreach (var sub in a) {
                        Console.Write("sub - ");
                        Console.WriteLine(sub.SubValue.Value.GetNTerm(0));
                    }
                }
                //knowledgeBase.AskPossibleProperties((Name)"PosX([x])"
                Console.WriteLine("aaaaheya222222!");
#endif

                //Console.WriteLine("Clock7");

                
                var currentSegmentProperty = knowledgeBase.AskProperty((Name)"World(CurrentSegment)");
                var currentDayProperty  = knowledgeBase.AskProperty((Name)"World(Cycle)");

                int segment = 0;
                int dia = 0;

                if (currentSegmentProperty != null)
                {
                    var segValue = currentSegmentProperty.Value.GetValue();
                    Console.WriteLine("segmento: " + currentSegmentProperty.ToString() + " - " + segValue.ToString());
                    segment = int.Parse(segValue.ToString());
                } else
                {
                    Console.WriteLine("FUCKING Seg NULL");
                }

                if (currentDayProperty != null)
                {
                    var dayValue = currentDayProperty.Value.GetValue();
                    Console.WriteLine("     dia: " + currentDayProperty.ToString() + " - " + dayValue.ToString());
                    dia = int.Parse(dayValue.ToString());
                } else
                {
                    Console.WriteLine("FUCKING DAY NULL");
                }
                this.clock.setTime(segment,dia);
                
                #region GET_STATS
                var walterHealth = knowledgeBase.AskProperty((Name)"Health(Walter)");
                var walterHunger = knowledgeBase.AskProperty((Name)"Hunger(Walter)");
                var walterSanity = knowledgeBase.AskProperty((Name)"Sanity(Walter)");
                Walter.SetStats((walterHealth != null ? int.Parse(walterHealth.Value.GetValue().ToString()) : -1),
                    (walterHunger != null ? int.Parse(walterHunger.Value.GetValue().ToString()) : -1),
                    (walterSanity != null ? int.Parse(walterSanity.Value.GetValue().ToString()) : -1));
                #endregion

                var walterEquiped = knowledgeBase.AskPossibleProperties((Name)"IsEquipped([x], hands)", Name.SELF_SYMBOL, null).ToList();
                if (walterEquiped.Count > 1)
                {
                    //throw new NotImplementedException();
                }

                var equiped_guid = "";
                foreach (var pair in walterEquiped)
                {
                    var belief_value = pair.Item1.Value.GetValue();
                    //string belief_property = "";
                    if (!Boolean.Parse(belief_value.ToString()))
                    {
                        //belief is false
                        continue;
                    }
                    foreach (var sub_array in pair.Item2)
                    {
                        foreach (var sub in sub_array)
                        {
                            //Console.WriteLine("new sub: " + sub.SubValue.Value.GetNTerm(0));
                            equiped_guid = sub.SubValue.Value.GetNTerm(0).ToString();
                        }
                    }
                    //var pairBeliefName_Parentisis = GetBeliefName_InsideParentesis(belief_property.ToString());
                    //if (pairBeliefName_Parentisis.Item1.Equals("IsEquipped"))
                    //{
                    //    //ITEM1 -> "IsEquipped"
                    //    //ITEM2 -> type,guid
                    //    var pairEntityTypeGuid = GetPairEntityNameGuid(pairBeliefName_Parentisis.Item2);
                    //    equiped_guid = pairEntityTypeGuid.Item2;
                    //}
                }

                //GET ENTITY BELIEF
                if (equiped_guid != "")
                {
                    var equipedGUID = knowledgeBase.AskPossibleProperties((Name) ("Entity(" + equiped_guid + ", [x])"),
                        Name.SELF_SYMBOL, null).ToList();
                    var equipedType = "";
                    foreach (var pair in equipedGUID)
                    {
                        var belief_value = pair.Item1.Value.GetValue();
                        string belief_property = "";
                        foreach (var sub_array in pair.Item2)
                        {
                            foreach (var sub in sub_array)
                            {
                                //Console.WriteLine("new sub: " + sub.SubValue.Value.GetNTerm(0));
                                equipedType = sub.SubValue.Value.GetNTerm(0).ToString();
                                break;
                            }

                            break;
                        }
                    }
                    this.Walter.Equip(equipedType);
                    Console.WriteLine("Equiped: " + equipedType + " " + equiped_guid);
                }


                //var pair = knowledgeBase.AskPossibleProperties((Name)"World([x])", Name.SELF_SYMBOL, null).ToList();//.SelectMany(p => p.Item2).ToArray();
                //foreach (var single_pair in pair)
                //{
                //    var belief_value = single_pair.Item1.Value.GetValue();
                //    string belief_property = "";
                //    foreach (var sub_array in single_pair.Item2) {
                //        foreach (var sub in sub_array)
                //        {
                //            //Console.WriteLine("new sub: " + sub.SubValue.Value.GetNTerm(0));
                //            belief_property = sub.SubValue.Value.GetNTerm(0).ToString();
                //            break;
                //        }
                //        break;
                //    }

                //    Console.WriteLine("World[x] Property: " + belief_property + "Value: " + belief_value);
                //}

                //Below Does not Work TODO
                //Console.WriteLine("Walter Atempt");
                //var array2 = knowledgeBase.AskPossibleProperties((Name)"[x](Walter)", Name.SELF_SYMBOL, null).SelectMany(p => p.Item2).ToArray();
                //foreach (var a in array2) {
                //    Console.WriteLine("Walter - " + a.ToString());

                //    foreach (var sub in a) {
                //        Console.Write("sub - ");
                //        Console.WriteLine(sub.SubValue.Value.GetNTerm(0));
                //    }
                //}

                var beliefs = knowledgeBase.GetAllBeliefs();
                foreach (var belief in beliefs) {
#if _PRINT_ALL_BELIEFS
                        //Console.WriteLine(belief.Name + " - " + belief.Value);
#endif
                    if (belief.Value.Equals((Name)"True")) {
                        var properties = GetBeliefName_InsideParentesis(belief.Name.ToString());
                        if (properties.Item1.Equals("Pickable")) {
                            var pickable = FindOrCreateDSTObject(properties.Item2);
                            pickable.PickWorkable = true;
                        }
                        if (properties.Item1.Equals("Collectable"))
                        { 
                            var pickable = FindOrCreateDSTObject(properties.Item2);
                            pickable.CollectWorkable = true;
                        }
                        if (properties.Item1.Equals("ChopWorkable")) {
                            var pickable = FindOrCreateDSTObject(properties.Item2);
                            pickable.ChopWorkable  = true;
                        }
                        if (properties.Item1.Equals("MineWorkable")) {
                            var pickable = FindOrCreateDSTObject(properties.Item2);
                            pickable.MineWorkable = true;
                        }
                        if (properties.Item1.Equals("InInventory")) {
                            var pickable = FindOrCreateDSTObject(properties.Item2);
                            pickable.InInventory = true;
                        }
                        if (properties.Item1.Equals("Equippable"))
                        {
                            var pickable = FindOrCreateDSTObject(properties.Item2);
                            pickable.Equippable = true;
                        }
                        if (properties.Item1.Equals("Edible"))
                        {
                            var pickable = FindOrCreateDSTObject(properties.Item2);
                            pickable.IsEatable = true;
                        }
                    } else if (belief.Value.Equals((Name)"False")) {
                        //Ignore the False
                        //guid = null;
                    } else {
                        var pairBeliefName_Parentisis = GetBeliefName_InsideParentesis(belief.Name.ToString());
                        if (pairBeliefName_Parentisis.Item1.Equals("Entity")) {
                            //ITEM1 -> "Entity"
                            //ITEM2 -> type,guid
                            var pairEntityTypeGuid = GetPairEntityNameGuid(pairBeliefName_Parentisis.Item2);
                            Console.WriteLine(pairEntityTypeGuid); 

                            //HACK FOR COLLECTABLE STUFF
                            var entityTypeName = pairEntityTypeGuid.Item1;
                            if (entityTypeName.Equals("grass"))
                            {
                                pairEntityTypeGuid.Item1 = "cutgrass";
                            }else if (entityTypeName.Equals("sapling"))
                            {
                                pairEntityTypeGuid.Item1 = "twigs";
                            }
                            Console.WriteLine("After: "+pairEntityTypeGuid); 

                            DSTObject pickable = FindOrCreateDSTObject(pairEntityTypeGuid.Item2);

                            if (pairEntityTypeGuid.Item1.Equals("campfire"))
                            {
                                pickable.IsLightSource = true;
                                pickable.TimeToBurn = 120;
                                _temporaryHolders[pickable.Guid] = pickable;
                            } 
                            //else if ()
                            //{

                            //} else if ()
                            //{

                            //}

                            pickable.SetEntityType(pairEntityTypeGuid.Item1);
                            var quantity = int.Parse(belief.Value.ToString());
                            pickable.quantity = quantity;

                        } else if (pairBeliefName_Parentisis.Item1.Equals("PosX")) {
                            var guid = pairBeliefName_Parentisis.Item2;
                            //"PosX(117209)": "212, 1",
                            //ITEM1 -> "PosX"
                            //ITEM2 -> guid
                            var pickable = FindOrCreateDSTObject(guid);
                            pickable.SetPosX(int.Parse(belief.Value.ToString()));
                        } else if (pairBeliefName_Parentisis.Item1.Equals("PosZ")) {
                            var guid = pairBeliefName_Parentisis.Item2;

                            //"PosZ(117209)": "212, 1",
                            //ITEM1 -> "PosZ"
                            //ITEM2 -> guid
                            var pickable = FindOrCreateDSTObject(guid);
                            pickable.SetPosZ(int.Parse(belief.Value.ToString()));
                        } else if (pairBeliefName_Parentisis.Item1.Equals("InSight"))
                        {
                            var guid = pairBeliefName_Parentisis.Item2;
                            var pickable = FindOrCreateDSTObject(guid);
                            pickable.inSight = true;
                        }
                    }
                }
                //var campfires = knowledgeBase.AskPossibleProperties((Name)"campfire([x])", Name.SELF_SYMBOL, null).ToList();//.SelectMany(p => p.Item2).ToArray();
                //foreach (var campfire in campfires)
                //{
                //    var 
                //}
                //--------------
                //--PARSE DONE--
                //--------------
                foreach (var pair_key_value in _temporaryHolders) {
                    var holder = pair_key_value.Value;
                    var flagIsAnything = false;
                    if (holder.isComplete())
                    {
                        if (holder.GetEntityType().Equals("robin") || holder.GetEntityType().Equals("crow") || holder.GetEntityType().Equals("butterfly") || holder.GetEntityType().Equals("mole")
                            || holder.GetEntityType().Equals("fireflies") || holder.GetEntityType().Equals("bee") || holder.GetEntityType().Equals("rabbit"))
                        {
                            continue;
                        }
                        if (holder.PickWorkable) {
                            toBeNamed(holder, _knownPickableObjects);
                            flagIsAnything = true;
                        }
                        if (holder.CollectWorkable) {
                            toBeNamed(holder, _knownCollectableObjects);
                            flagIsAnything = true;
                        }
                        if (holder.ChopWorkable) {
                            toBeNamed(holder, _knownChopableObjects);
                            flagIsAnything = true;
                        }
                        if (holder.HammerWorkable) {
                            toBeNamed(holder, _knownHammerableObjects);
                            flagIsAnything = true;
                        }
                        if (holder.DigWorkable) {
                            toBeNamed(holder, _knownDiggableObjects);
                            flagIsAnything = true;
                        }
                        if (holder.MineWorkable) {
                            toBeNamed(holder, _knownMineableObjects);
                            flagIsAnything = true;
                        }
                        if (holder.InInventory) {
                            this.Walter.AddToInventoryKB(holder.GetEntityType(),holder.quantity);
                            //toBeNamed(holder, _knownInInventoryObjects);
                            flagIsAnything = true;
                        }
                        if (holder.Equippable)
                        {
                            toBeNamed(holder, _knownEquippableObjects);
                            flagIsAnything = true;
                        }
                        if (holder.IsEatable)
                        {
                            toBeNamed(holder, _knownEatableObjects);
                            flagIsAnything = true;
                        } 
                        if (holder.IsLightSource && holder.inSight) {
                            //get our detailed info

                            _lightsManager.addNewLightSource(holder);
                            flagIsAnything = true;
                        }


                        if (!flagIsAnything)
                        {

                            //Console.WriteLine("ERROR: OBJ Aint Nothing: " + holder.ToString());


                            //////Console.WriteLine("..Enter to continue execution..");
                            //////Console.ReadLine();
                        }
                    }
                    else
                    {
                        Console.WriteLine("ERROR: OBJ not Complete");
                        //Console.WriteLine("..Enter to continue execution..");
                        //Console.ReadLine();
                    }
                }
            } catch (Exception e) {
                Console.WriteLine(e);
                Console.WriteLine("..Enter to continue execution..");
                Console.ReadLine();
                throw;
            }

            //put stuff in inventory
            //foreach (var VARIABLE in COLLECTION)
            //{
            //    
            //}
            
        }

        private void toBeNamed(DSTObject objToInsert, Dictionary<string, List<DSTObject>> dictionaryWhereToInsert) {
            var objType = objToInsert.GetEntityType();
            dictionaryWhereToInsert.TryGetValue(objType, out var objsList);
            if (objsList == null) objsList = new List<DSTObject>();
            objToInsert.calculateDistanceToChar(Walter.WalterPosition);
            insertSorted(objsList, objToInsert);
            dictionaryWhereToInsert[objType] = objsList;
        }


        private void insertSorted(List<DSTObject> list, DSTObject toInsert)
        {
            for (var i = 0; i < list.Count; i++)
            {
                var obj = list[i];
                if (obj.SquaredDistance > toInsert.SquaredDistance)
                {
                    list.Insert(i, toInsert);
                    return;
                }
            }

            list.Add(toInsert);
        }

        public DSTObject FindOrCreateDSTObject(string guid)
        {
            DSTObject holder = null;
            _temporaryHolders.TryGetValue(guid, out holder);
            if (holder == null)
            {
                holder = new DSTObject(guid);
                _temporaryHolders[guid] = holder;
            }

            return holder;
        }

        public void Initialize()
        {
            //this.ActionEnumerator.Reset();
        }

        private Pair<string, string> GetBeliefName_InsideParentesis(string belief)
        {
            var index1 = belief.IndexOf("(", StringComparison.Ordinal);
            var index2 = belief.IndexOf(")", StringComparison.Ordinal);
            var name = belief.Substring(0, index1);
            var guid = belief.Substring(index1 + 1, index2 - 1 - index1);
            return new Pair<string, string>(name, guid);
        }

        private Pair<string, string> GetPairEntityNameGuid(string pair)
        {
            var commaIndex = pair.IndexOf(",", StringComparison.Ordinal);
            var guid = pair.Substring(0, commaIndex);
            var entityName = pair.Substring(commaIndex + 2);

            return new Pair<string, string>(entityName, guid);
        }

        private string getPositionFromString(string pos)
        {
            //return pos;
            var stringEnd = pos.IndexOf(",", StringComparison.Ordinal);
            return pos.Substring(0, stringEnd);
        }

        private void updateWalterZ(string posZ)
        {
            Walter.WalterPosition.y = int.Parse(getPositionFromString(posZ));
        }

        private void updateWalterX(string posX)
        {
            Walter.WalterPosition.x = int.Parse(getPositionFromString(posX));
        }
    }
}