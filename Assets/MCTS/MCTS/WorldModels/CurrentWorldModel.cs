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
        public CurrentWorldModel(KB knowledgeBase)
        {
            guid = null;
            try
            {       
                var beliefs = knowledgeBase.GetAllBeliefs();
                foreach (var belief in beliefs)
                {
                    //Console.WriteLine(belief.Name + " - " + belief.Value);
                    if (belief.Value.Equals((Name)"True"))
                    {
                        var properties = GetBeliefProperties(belief.Name.ToString());
                        if (properties.Item1.Equals("Pickable"))
                        {
                            if (guid == null) {
                                Console.WriteLine("setting guid " + guid);
                                guid = properties.Item2;
                            }
                        }
                        else if (belief.Value.Equals((Name)"False"))
                        {
                            //Ignore the False
                            guid = null;

                        }
                        else
                        {
                            if (guid != null) {
                                if (belief.Name.ToString().Contains(guid) && belief.Name.ToString().Contains("Entity")) {
                                    Console.WriteLine(belief.Name + " - " + belief.Value);
                                }
                            }
                            //Some other stuff, may be relevant
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void Initialize() {
            //this.ActionEnumerator.Reset();
        }

        Pair<string, string> GetBeliefProperties(string belief)
        {
            int index1 = belief.IndexOf("(", StringComparison.Ordinal);
            int index2 = belief.IndexOf(")", StringComparison.Ordinal);
            var name = belief.Substring(0, index1);
            var guid = belief.Substring(index1 + 1, (index2 - 1) - index1);
            return new Pair<string, string>(name, guid);
        }

    }
}
