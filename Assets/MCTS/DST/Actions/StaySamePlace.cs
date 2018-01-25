using MCTS.Math;

namespace MCTS.DST.Actions
{
    class StaySamePlace : WalktoAction
    {
        public StaySamePlace(Vector2i position) : base(position) 
        {
        }
    }
}