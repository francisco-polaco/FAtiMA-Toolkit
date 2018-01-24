using System;
using MCTS.Math;

namespace MCTS.DST.Actions
{
    internal class WanderAction : WalktoAction
    {
        public override string GetXmlName()
        {
            return "WANDER";
        }

        public WanderAction(Vector2i walterPosition) : base(walterPosition)
        {
            //FIXME: acho que as coordenadas estao bue grandes
            var r = new Random();
            var x = r.Next(100) >= 50 ? -r.Next(150) : r.Next(150);
            var y = r.Next(100) >= 50 ? -r.Next(150) : r.Next(150);
            TargetPosition = walterPosition + new Vector2i(x, y);
        }

        
    }
}