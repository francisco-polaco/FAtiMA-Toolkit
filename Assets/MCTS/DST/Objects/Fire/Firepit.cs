using MCTS.Math;

namespace MCTS.DST.Objects.Fire
{
    class Firepit : LightSource {

        public Firepit(Vector2i vector2i, int timeToBurn, DSTObject obj) : base(obj){
            this.SourcePosition = vector2i;
            this.SecondsRemaining = timeToBurn;

        }

        public Firepit(LightSource lightSource) : base(lightSource) {
        }

        public override bool CanAddFuel() {
            return true;
        }
        public override void AddFuel(Fuel fuel) {
            SecondsRemaining += fuel.FuelTime*2;
        }

        public override LightSource generateClone()
        {
            return new Firepit(this);
        }
    }
}