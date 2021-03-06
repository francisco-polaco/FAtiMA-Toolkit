﻿using System;
using KnowledgeBase;
using SerializationUtilities;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using MCTS.DST;
using MCTS.DST.WorldModels;
using WellFormedNames;
using Action = MCTS.DST.Actions.Action;

namespace MCTS
{
    public class MCTSAsset
    {
        private static readonly Name MCTS_DYNAMIC_PROPERTY_NAME = Name.BuildName("MCTS");
        private KB m_kb;
        private MCTSAlgorithm mcts = null;
        private Action _lastAction = null;


        public MCTSAsset()
        {
            m_kb = null;
            mcts = new MCTSBiasedAlgorithm();   
#if DEBUG
            var version = Assembly.GetExecutingAssembly().GetName().Version;
            var buildDateTime = version.ToString();
            Console.WriteLine("BUILD_: " + buildDateTime);

            DateTime creationDate = new DateTime(2000, 1, 1).AddDays(version.Build).AddSeconds(version.MinorRevision * 2);
            Console.WriteLine("TIME_: " + creationDate);
#endif
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
            DST.Actions.Action action = null;

            if (!mcts.InProgress)
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

                try {


                    mcts.InitializeDecisionMakingProcess(kbCloned);
                    action = mcts.ChooseAction();

                } catch (Exception e) {
                    Console.WriteLine(e);
                    Console.WriteLine("..Enter to continue execution..");
                    Console.ReadLine();
                    throw e;
                }

            } else
            {
                try {
                    action = mcts.ChooseAction();
                } catch (Exception e) {
                    Console.WriteLine(e);
                    Console.WriteLine("..Enter to continue execution..");
                    Console.ReadLine();
                    throw e;
                }
            }

            if (action == null)
            {
                if (_lastAction == null) {
                    throw new NoActionException();
                } 
                //else
                //{
                //    action = _lastAction;
                //    _lastAction = null;
                //}
            }
            //else
            //{
            //    _lastAction = action;
            //} 



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

        public class NoActionException : Exception
        {
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
