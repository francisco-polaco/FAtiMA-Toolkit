using MCTS.Math;

namespace MCTS.DST.Objects.Fire
{
    class Campfire : LightSource
    {


        public Campfire(Vector2i vector2i, int timeToBurn, DSTObject obj) : base(obj) {
            this.SourcePosition = vector2i;
            this.SecondsRemaining = timeToBurn;
        }

        public Campfire(LightSource lightSource) : base(lightSource) {
        }

        public override bool CanAddFuel()
        {
            return SecondsRemaining > 0;
        }

        public override void AddFuel(Fuel fuel)
        {
            SecondsRemaining += fuel.FuelTime;
        }

        public override LightSource generateClone()
        {
            return new Campfire(this);
        }
    }
}