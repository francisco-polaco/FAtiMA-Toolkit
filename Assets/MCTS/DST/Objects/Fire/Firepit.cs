namespace MCTS.DST.Objects.Fire
{
    class Firepit : LightSource {
        public override bool CanAddFuel() {
            return true;
        }
        public override void AddFuel(Fuel fuel) {
            SecondsRemaining += fuel.FuelTime*2;
        }
    }
}