using System;
using KnowledgeBase;
using SerializationUtilities;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using MCTS.DST;
using MCTS.DST.WorldModels;
using WellFormedNames;

namespace MCTS
{
    public class MCTSAsset
    {
        private static readonly Name MCTS_DYNAMIC_PROPERTY_NAME = Name.BuildName("MCTS");

        private KB m_kb;


        public MCTSAsset()
        {
            m_kb = null;
        }

        /// <summary>
        /// Binds a KB to this asset.
        /// </summary>
        /// <param name="kb">The Knowledge Base to be binded to this asset.</param>
        public void RegisterKnowledgeBase(KB kB)
        {
            if (m_kb != null)
            {
                //Unregist bindings
                UnbindToRegistry(m_kb);
                m_kb = null;
            }

            m_kb = kB;
            BindToRegistry(kB);
        }

        private void BindToRegistry(IDynamicPropertiesRegistry registry)
        {
            registry.RegistDynamicProperty(MCTS_DYNAMIC_PROPERTY_NAME, MCTSSearch);
        }

        public void UnbindToRegistry(IDynamicPropertiesRegistry registry)
        {
            registry.UnregistDynamicProperty(MCTS_DYNAMIC_PROPERTY_NAME);
        }

        //This is where the main body of the MCTS Search must be implemented
        private IEnumerable<DynamicPropertyResult> MCTSSearch(IQueryContext context, Name actionVar, Name targetVar)
        {
            //How to clone the KB with our JSON serializer
            var jsonSerializer = new JSONSerializer();
            var memStream = new MemoryStream();
            var json = jsonSerializer.SerializeToJson(this.m_kb);
            var kbCloned = jsonSerializer.DeserializeFromJson<KB>(json);

            #if PRINTKB
                Console.WriteLine(json.ToString());
                Console.ReadLine();
            #endif

            //var deepKbClone = DeepClone(m_kb);

            DST.Actions.Action action = null;
            try {

                var mctsAlgorithm = new MCTSAlgorithm();
                mctsAlgorithm.InitializeDecisionMakingProcess(kbCloned);
                //var action = "PICK(" + CurrentWorldModel.guid + ")";
                // var action = mctsAlgorithm.ChooseAction();

                action = mctsAlgorithm.ChooseAction();

            } catch (Exception e) {
                Console.WriteLine(e);
                Console.WriteLine("..Enter to continue execution..");
                Console.ReadLine();
                throw e;


            }

            //This is just an example of how to always return the action "Pick" with target "Wood1"
            //var actionSub = new Substitution(actionVar, new ComplexValue(Name.BuildName("Action(PICK, -, -, -, -)")));
            var actionSub = new Substitution(actionVar, new ComplexValue(Name.BuildName(action.GetDstInterpretableAction())));
            //var targetSub = new Substitution(targetVar, new ComplexValue(Name.BuildName(CurrentWorldModel.guid)));
            var targetSub = new Substitution(targetVar, new ComplexValue(Name.BuildName(action.GetTarget())));

            foreach (var subSet in context.Constraints)
            {
                subSet.AddSubstitution(actionSub);
                subSet.AddSubstitution(targetSub);

                yield return new DynamicPropertyResult(new ComplexValue(Name.BuildName(true),1.0f), subSet);
            }
        }

        /// <summary>
        /// Deep copies an object in memory
        /// </summary>
        /// <param name="obj">Object to be copied.</param>
        private static T DeepClone<T>(T obj)
        {
            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, obj);
                ms.Position = 0;

                return (T)formatter.Deserialize(ms);
            }
        }

    }
}
