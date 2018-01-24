using System;
using System.Collections.Generic;
using MCTS.DST.WorldModels;
using Action = MCTS.DST.Actions.Action;

namespace MCTS.DST
{
    public class MCTSNode
    {
        public MCTSNode(WorldModel state)
        {
            State = state;
            ChildNodes = new List<MCTSNode>();
        }

        public WorldModel State { get; }
        public MCTSNode Parent { get; set; }
        public Action Action { get; set; }
        public int PlayerID { get; set; }
        public List<MCTSNode> ChildNodes { get; }
        public int N { get; set; }
        public float Q { get; set; }

        public string ToXML(int depth)
        {
            //if (ChildNodes.Count > 0) {
            //    toReturn += tabSpaces + " <Number_Childs> " + ChildNodes.Count + " </Number_Childs>";
            //}
            var tabSpaces = "\n";
            for (var i = 0; i < depth; i++) tabSpaces += " ";

            var toReturn = tabSpaces + "<Node>";
            if (Action != null) toReturn += tabSpaces + " <Action> " + Action.GetXmlName() + " </Action>";
            toReturn += tabSpaces + " <N>" + N + "</N>";
            toReturn += tabSpaces + " <Q>" + Q + "</Q>";
            toReturn += tabSpaces + " <Q_N_div>" + Q / N + "</Q_N_div>";
            toReturn += tabSpaces + " <Terminal>" + State.IsTerminal() + "</Terminal>";
            if (Parent != null)
            {
                var firstPart = Q / N;
                var secondPart = MCTSAlgorithm.C * System.Math.Sqrt(System.Math.Log(Parent.N) / N);
                toReturn += tabSpaces + " <BestUTC>" + (firstPart + secondPart) + "</BestUTC>";
            }
            else
            {
                toReturn += tabSpaces + " <BestUTC>" + 0 + "</BestUTC>";
            }

            foreach (var node in ChildNodes) toReturn += node.ToXML(depth + 1);
            toReturn += tabSpaces + "</Node>";
            return toReturn;
        }

        internal int RecursiveNumberOfChilds()
        {
            var toReturn = 1;
            foreach (var node in ChildNodes) toReturn += node.RecursiveNumberOfChilds();
            return toReturn;
        }
    }
}