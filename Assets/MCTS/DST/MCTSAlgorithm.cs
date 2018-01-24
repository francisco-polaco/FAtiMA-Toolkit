using System;
using System.Collections.Generic;
using System.IO;
using KnowledgeBase;
using MCTS.DST.WorldModels;
using Action = MCTS.DST.Actions.Action;

namespace MCTS.DST
{
    public class MCTSAlgorithm
    {
        public const float C = 1.4f;

        public MCTSAlgorithm()
        {
            InProgress = false;
            // this.CurrentStateWorldModel = currentStateWorldModel;
            MaxIterations = 1000;
            MaxIterationsProcessedPerFrame = MaxIterations +1;
            RandomGenerator = new Random();
            TotalProcessingTime = 0;
        }

        public bool InProgress { get; private set; }
        public int MaxIterations { get; set; }
        public int MaxIterationsProcessedPerFrame { get; set; }
        public int MaxPlayoutDepthReached { get; private set; }
        public int MaxSelectionDepthReached { get; private set; }
        public float TotalProcessingTime { get; }

        public MCTSNode BestFirstChild { get; set; }
        public Action[] BestActionSequence { get; private set; }
        public Action BestAction { get; private set; }

        private int CurrentIterations { get; set; }
        private int CurrentIterationsInFrame { get; set; }
        private int CurrentDepth { get; set; }

        private CurrentWorldModel CurrentStateWorldModel { get; set; }
        private MCTSNode InitialNode { get; set; }
        protected Random RandomGenerator { get; set; }


        public void InitializeDecisionMakingProcess(KB knowledgeBase)
        {
            Console.WriteLine("InitializeDecisionMakingProcess");

            MaxPlayoutDepthReached = 0;
            MaxSelectionDepthReached = 0;
            CurrentIterations = 0;
            CurrentIterationsInFrame = 0;
            CurrentStateWorldModel = new CurrentWorldModel(knowledgeBase);
            CurrentStateWorldModel.Initialize();
            InitialNode = new MCTSNode(CurrentStateWorldModel.GenerateChildWorldModel())
            {
                Action = null,
                Parent = null,
                PlayerID = 0
            };
            //Console.WriteLine(InitialNode.State.depth);
            InProgress = true;
            BestFirstChild = null;
            //this.ParcialProcessingTime = 0;

            // this.BestActionSequence = new List<GOB.Action>();
        }

        public Action ChooseAction()
        {
            Console.WriteLine("ChooseAction");

            //var frameBegin = Time.realtimeSinceStartup;

            MCTSNode selectedNode;
            Reward reward;

            CurrentIterationsInFrame = 0;
            var rootNode = InitialNode;
            //MCTSNode rootNode =  new MCTSNode(CurrentStateWorldModel.GenerateChildWorldModel());
            //Console.WriteLine("0");
                while (CurrentIterationsInFrame < MaxIterationsProcessedPerFrame
                       && CurrentIterations < MaxIterations) {
                    //Console.WriteLine("1");
                    selectedNode = Selection(rootNode);
                    //Console.WriteLine("2");
                    reward = Playout(selectedNode.State);
                    //Console.WriteLine("3");
                    Backpropagate(selectedNode, reward);
                    CurrentIterations++;
                    CurrentIterationsInFrame++;
                    //Console.WriteLine("++");
                }
            
            //var frameEnd = Time.realtimeSinceStartup;
            //var thisFrameTime = frameEnd - frameBegin;

            //TotalProcessingTime += thisFrameTime;
            //ParcialProcessingTime += thisFrameTime;
            if (CurrentIterations >= MaxIterations)
            {
                InProgress = false;
                printXMLTree(rootNode);

                var temp = new List<Action>();
                var currNode = rootNode;
                var bestChild = BestChild(currNode);
                while (bestChild != null)
                {
                    temp.Add(bestChild.Action);
                    bestChild = BestChild(bestChild);
                }

                BestActionSequence = temp.ToArray();
                var toReturn = BestChild(rootNode);
                if (toReturn != null)
                {
                    BestAction = toReturn.Action;
                    return BestAction;
                }
                return null;
            }
            return null;
        }

        private MCTSNode Selection(MCTSNode initialNode)
        {
            Action nextAction;
            var currentNode = initialNode;
            MCTSNode bestChild;

            while (!currentNode.State.IsTerminal())
            {
                nextAction = currentNode.State.GetNextAction();
                if (nextAction != null) return Expand(currentNode, nextAction);

                if (currentNode.ChildNodes.Count == 0)
                {
                    //DEBUG CODE
                    var xmlTree = initialNode.ToXML(0);
                    var numero = initialNode.RecursiveNumberOfChilds();
                    File.WriteAllText(@"C:\treeXml\tree.xml", xmlTree);
                }
                else
                {
                    currentNode = BestUCTChild(currentNode);
                }

                
            }

            return currentNode;
        }

        private Reward Playout(WorldModel currentPlayoutState)
        {
            while (!currentPlayoutState.IsTerminal())
            {
                //this.PlayoutNodes++;
                var action = GuidedAction(currentPlayoutState);
                if (action == null)
                    return new Reward
                    {
                        PlayerID = currentPlayoutState.GetNextPlayer(),
                        Value = 0
                    };
                var childModel = currentPlayoutState.RecycleWorldModel();
                action.ApplyActionEffects(childModel);
                childModel.CalculateNextPlayer();
                currentPlayoutState = childModel;
            }

            return new Reward
            {
                PlayerID = currentPlayoutState.GetNextPlayer(),
                Value = currentPlayoutState.GetScore()
            };
        }

        protected virtual Action GuidedAction(WorldModel currentPlayoutState)
        {
            var possibleActions = currentPlayoutState.GetExecutableActions();

            var lenght = possibleActions.Length;

            //this.TotalPlayoutNodes += lenght;
            //this.PlayoutNodes += lenght;

            var number = RandomGenerator.Next(possibleActions.Length);
            return possibleActions[number];
        }

        private void Backpropagate(MCTSNode node, Reward reward)
        {
            while (node != null)
            {
                node.N++;
                node.Q += reward.Value;
                node = node.Parent;
            }
        }

        private MCTSNode Expand(MCTSNode parent, Action action)
        {
            var childModel = parent.State.GenerateChildWorldModel();
            action.ApplyActionEffects(childModel);
            childModel.CalculateNextPlayer();
            var child = new MCTSNode(childModel)
            {
                Parent = parent,
                PlayerID = childModel.GetNextPlayer(),
                Action = action,
                N = 0,
                Q = 0f
            };
            parent.ChildNodes.Add(child);
            return child;
        }

        //gets the best child of a node, using the UCT formula
        private MCTSNode BestUCTChild(MCTSNode node)
        {
            MCTSNode best = null;
            var bestUCT = double.MinValue;
            foreach (var nodeChildNode in node.ChildNodes)
            {
                var firstPart = nodeChildNode.Q / nodeChildNode.N;
                var secondPart = C * System.Math.Sqrt(System.Math.Log(nodeChildNode.Parent.N) / nodeChildNode.N);
                var sum = firstPart + secondPart;
                if (sum > bestUCT)
                {
                    bestUCT = sum;
                    best = nodeChildNode;
                }
            }
            return best;
        }

        // this method is very similar to the bestUCTChild,
        // but it is used to return the final action of the MCTS search, and so we do not care about
        // the exploration factor
        private MCTSNode BestChild(MCTSNode node)
        {
            MCTSNode best = null;
            var bestUCT = double.MinValue;
            foreach (var nodeChildNode in node.ChildNodes)
            {
                var firstPart = nodeChildNode.Q / nodeChildNode.N;
                if (bestUCT < firstPart)
                {
                    bestUCT = firstPart;
                    best = nodeChildNode;
                }
            }
            return best;
        }

        private void printXMLTree(MCTSNode initialNode)
        {
            //Guid uid = Guid.NewGuid();
            var xmlTree = initialNode.ToXML(0);
            var numero = initialNode.RecursiveNumberOfChilds();
            File.WriteAllText(@"C:\treeXml\tree.xml", xmlTree);
            //Debug.Log("Escrita Arvore");
        }
    }
}