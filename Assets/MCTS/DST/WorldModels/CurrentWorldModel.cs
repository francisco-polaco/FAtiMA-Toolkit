using System;
using System.Collections.Generic;
using KnowledgeBase;
using MCTS.DST.Actions;
using Utilities;
using WellFormedNames;
using MCTS.DST.Objects;

namespace MCTS.DST.WorldModels {
    public partial class CurrentWorldModel : WorldModel
    {
        public static string guid;
        private List<GameObject> _woodObjects = new List<GameObject>();

        //Key is GUID
        private Dictionary<string, PickableObject> _temporaryHolders = new Dictionary<string, PickableObject>();

        public CurrentWorldModel(KB knowledgeBase)
        {

            //guid = null;
            try {       
                var walterZ = knowledgeBase.AskProperty((Name)"PosZ(Walter)");
                this.updateWalterZ(walterZ.ToString());
                var walterX = knowledgeBase.AskProperty((Name)"PosX(Walter)");
                this.updateWalterX(walterX.ToString());

                var beliefs = knowledgeBase.GetAllBeliefs();
                foreach (var belief in beliefs)
                {
                    //Console.WriteLine(belief.Name + " - " + belief.Value);
                    if (belief.Value.Equals((Name)"True")) {
                        var properties = GetBeliefName_InsideParentesis(belief.Name.ToString());
                        if (properties.Item1.Equals("Pickable")) {
                            var pickable = FindOrCreatePickable(properties.Item2);
                            pickable.Pickable = true;
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
                            var pickable = FindOrCreatePickable(pairEntityTypeGuid.Item2);
                            pickable.SetEntityType(pairEntityTypeGuid.Item1);

                            
                        } else if (pairBeliefName_Parentisis.Item1.Equals("PosX")) {
                            var guid = pairBeliefName_Parentisis.Item2;
                            //"PosX(117209)": "212, 1",
                            //ITEM1 -> "PosX"
                            //ITEM2 -> guid
                            var pickable = FindOrCreatePickable(guid);
                            pickable.SetPosX(Int32.Parse(getPositionFromString(belief.Value.ToString())));

                        } else if (pairBeliefName_Parentisis.Item1.Equals("PosZ")) {
                            var guid = pairBeliefName_Parentisis.Item2;
                            //"PosZ(117209)": "212, 1",
                            //ITEM1 -> "PosZ"
                            //ITEM2 -> guid
                            var pickable = FindOrCreatePickable(guid);
                            pickable.SetPosZ(Int32.Parse(getPositionFromString(belief.Value.ToString())));
                        }
                        //if (guid != null) {
                        //    if (belief.Name.ToString().Contains(guid) && belief.Name.ToString().Contains("Entity")) {
                        //        Console.WriteLine(belief.Name + " - " + belief.Value);
                        //    }
                        //}
                        //Some other stuff, may be relevant
                    }
                }
                foreach( var pair_key_value in _temporaryHolders) {
                    var holder = pair_key_value.Value;
                    if (holder.isPickableComplete()) {
                        var objType = holder.GetEntityType();
                        List<PickableObject> objsList = null;
                        _knownObjects.TryGetValue(objType, out objsList);
                        if (objsList == null) {
                            objsList = new List<PickableObject>();
                        }
                        holder.calculateDistanceToChar(walterPosition);
                        insertSorted(objsList, holder);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private void insertSorted(List<PickableObject> list, PickableObject toInsert) {

            for(var i = 0; i < list.Count; i++) {
                var obj = list[i];
                if (obj.SquaredDistance > toInsert.SquaredDistance) {
                    list.Insert(i, toInsert);
                    return;
                }
            }
            list.Add(toInsert);

        }

        public PickableObject FindOrCreatePickable(string guid) {
            PickableObject holder = null;
            _temporaryHolders.TryGetValue(guid, out holder);
            if(holder == null) {
                holder = new PickableObject(guid);
                _temporaryHolders[guid] = holder;
            }
            return holder;
        }

        public void Initialize() {
            //this.ActionEnumerator.Reset();
        }

        Pair<string, string> GetBeliefName_InsideParentesis(string belief)
        {
            int index1 = belief.IndexOf("(", StringComparison.Ordinal);
            int index2 = belief.IndexOf(")", StringComparison.Ordinal);
            var name = belief.Substring(0, index1);
            var guid = belief.Substring(index1 + 1, (index2 - 1) - index1);
            return new Pair<string, string>(name, guid);
        }

        Pair<string, string> GetPairEntityNameGuid(string pair) {
            int commaIndex = pair.IndexOf(",", StringComparison.Ordinal);
            var guid = pair.Substring(0, commaIndex);
            var entityName = pair.Substring(commaIndex+2);

            return new Pair<string, string>(entityName, guid);
        }

        private string getPositionFromString(string pos) {
            int stringEnd = pos.IndexOf(",", StringComparison.Ordinal);
            return pos.Substring(0, stringEnd);
        }

        private void updateWalterZ(string posZ) {
            walterPosition.Y = Int32.Parse(getPositionFromString(posZ));
        }
        private void updateWalterX(string posX) {
            walterPosition.X = Int32.Parse(getPositionFromString(posX));
        }

    }
}
