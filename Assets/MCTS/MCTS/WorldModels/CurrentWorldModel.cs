using System;
using System.Collections.Generic;
using KnowledgeBase;
using Utilities;
using WellFormedNames;

namespace MCTS.MCTS.WorldModels {
    public class CurrentWorldModel : WorldModel
    {
        public static string guid;
        public CurrentWorldModel(KB knowledgeBase)
        {
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
                            guid = properties.Item2;
                            Console.WriteLine("This one is " + properties.Item2.ToString());
                        }
                        else if (belief.Value.Equals((Name)"False"))
                        {
                            //Ignore the False
                        }
                        else
                        {
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
