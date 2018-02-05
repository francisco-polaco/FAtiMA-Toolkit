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

        public override WorldModel RecycleWorldModel()
        {
            this.depth++;
            base.RecycleWorldModel();
            return this;
        }



        //Stops Playout
        public override bool IsTerminal()
        {
            return Walter.Health <= 0 || Walter.WalkedDistance > 480;//depth > 30;  //|| Walter.WalkedDistance > 30; //depth > 5;
        } 

        public override float GetScore()
        { 
            double score = 0;
            //if (Walter.Health > 0)
            //{
            //    score += 200;
            //}

            if (Walter.Hunger > 30)
            {
                score += 50;
            }

            if (Walter.Sanity > 50)
            {
                score += 50;
            }
             
            //if (Walter.WalkedDistance > 0)
            //{
            //    score += (-(Walter.WalkedDistance / 480) + 1) * 100;

            //}

            if (Walter.TimeInNight > 0)
            {
                score += 100;
                score += Convert.ToSingle(Walter.TimeInNight)/ 60 * 100;
            }

            if (Walter.TimeInNight >= 60)
            {
                score += 200;
            }

            if ((Convert.ToSingle(score) / 500.0f) > 1)
            {
                Console.WriteLine("Rip");
            } 

            return (Convert.ToSingle(score)/500);
            //return Walter.Health + Walter.Hunger + Walter.Sanity + Walter.WalkedDistance*;
            //return 10 / (float) Walter.WalkedDistance;
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