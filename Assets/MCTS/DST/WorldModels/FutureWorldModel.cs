using System;
using System.Collections.Generic;
using MCTS.DST.Objects;
using MCTS.Math;
using Action = MCTS.DST.Actions.Action;
using MCTS.DST.Actions;

namespace MCTS.DST.WorldModels
{
    public class FutureWorldModel : WorldModel {

        private int depth = 0;

        public FutureWorldModel() {

        }

        public FutureWorldModel(FutureWorldModel wm) : base (wm)
        {
            //Console.WriteLine("DEPTH: "+wm.depth);
            depth = wm.depth + 1;
        }

        public override WorldModel GenerateChildWorldModel()
        {
            return new FutureWorldModel(this);
        }


        //Stops Playout
        public override bool IsTerminal()
        {
            return depth > 1;
        }

        public override float GetScore()
        {
            return 10 / (float) Walter.WalkedDistance;
        }


        //public virtual int GetNextPlayer()
        //{
        //    return 0;
        //}

        //public virtual void CalculateNextPlayer()
        //{
        //}
    }
}