using System;
using MCTS.Math;

namespace MCTS.DST.Actions
{
    internal class WanderAction : WalktoAction
    {
        public WanderAction(Vector2i walterPosition) : base(walterPosition)
        {
            //FIXME: acho que as coordenadas estao bue grandes
            var r = new Random();
            var x = r.Next(100) >= 50 ? r.Next(10) : -r.Next(10);
            var y = r.Next(100) >= 50 ? r.Next(10) : -r.Next(10);
            _position = walterPosition + new Vector2i(x, y);
        }
    }
}