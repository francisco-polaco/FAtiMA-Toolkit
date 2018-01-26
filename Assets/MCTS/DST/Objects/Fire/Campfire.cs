namespace MCTS.DST.Objects.Fire
{
    class Campfire : LightSource
    {
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