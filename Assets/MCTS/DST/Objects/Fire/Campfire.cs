using MCTS.Math;

namespace MCTS.DST.Objects.Fire
{
    class Campfire : LightSource
    {


        public Campfire(Vector2i vector2i, int timeToBurn, FireDstObject obj) : base(obj) {
            this.SourcePosition = vector2i;
            this.SecondsRemaining = timeToBurn;
        }

        public override bool CanAddFuel()
        {
            return SecondsRemaining > 0;
        }

        public override void AddFuel(Fuel fuel)
        {
            SecondsRemaining += fuel.FuelTime;
        }
    }
}