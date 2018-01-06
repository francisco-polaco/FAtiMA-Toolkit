using System;
using System.Collections.Generic;
using KnowledgeBase;
using MCTS.MCTS.Actions;
using Utilities;
using WellFormedNames;

namespace MCTS.MCTS.WorldModels {
    public class CurrentWorldModel : WorldModel
    {
        public static string guid;
        private List<GameObject> _woodObjects = new List<GameObject>();

        //Key is GUID
        private Dictionary<string, PickableObject> _temporaryHolders = new Dictionary<string, PickableObject>();

        public CurrentWorldModel(KB knowledgeBase)
        {
            var walterZ = knowledgeBase.AskProperty((Name)"PosZ(Walter)");
            this.updateWalterZ(walterZ.ToString());
            var walterX = knowledgeBase.AskProperty((Name)"PosX(Walter)");
            this.updateWalterX(walterX.ToString());

            //guid = null;
            try {       
                var beliefs = knowledgeBase.GetAllBeliefs();
                foreach (var belief in beliefs)
                {
                    //Console.WriteLine(belief.Name + " - " + belief.Value);
                    if (belief.Value.Equals((Name)"True")) {
                        var properties = GetBeliefNameGuid(belief.Name.ToString());
                        if (properties.Item1.Equals("Pickable")) {
                            var pickable = FindOrCreatePickable(properties.Item2);
                            pickable.Pickable = true;
                        }
                    } else if (belief.Value.Equals((Name)"False")) {
                        //Ignore the False
                        //guid = null;

                    } else {
                        if (belief.Name.ToString().Contains("Entity")) {
                            var properties = GetBeliefNameGuid(belief.Name.ToString());
                            //ITEM1 -> Entity
                            //ITEM2 -> type,guid
                            var pairEntityTypeGuid = GetPairEntityNameGuid(properties.Item2);
                            var pickable = FindOrCreatePickable(pairEntityTypeGuid.Item2);
                            pickable.SetEntityType(pairEntityTypeGuid.Item1);
                            
                            //checkPickableReady();
                        }
                        if (guid != null) {
                            if (belief.Name.ToString().Contains(guid) && belief.Name.ToString().Contains("Entity")) {
                                Console.WriteLine(belief.Name + " - " + belief.Value);
                            }
                        }
                        //Some other stuff, may be relevant
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
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

        Pair<string, string> GetBeliefNameGuid(string belief)
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
        public class PickableObject {

            public Boolean Pickable { get; set; } = false;

            private Boolean PosXSet { get; set; } = false;
            private Boolean PosZSet { get;  set; } = false;
            private Boolean EntitySet {  get;  set; } = false;
                                       
            public string Guid { get; }

            private int posX;

            public int GetPosX() {
                return posX;
            }

            public void SetPosX(int value) {
                PosXSet = true;
                posX = value;
            }

            private int posZ;

            public int GetPosZ() {
                return posZ;
            }

            public void SetPosZ(int value) {
                PosZSet = true;
                posZ = value;
            }

            private string entityType;

            public string GetEntityType() {
                return entityType;
            }

            public void SetEntityType(string value) {
                EntitySet = true;
                entityType = value;
            }

            public PickableObject(string guid) {
                Guid = guid;
            }

            public bool isPickableComplete() {
                return Pickable && EntitySet && PosXSet && PosZSet;
            }

        }

    }
}
