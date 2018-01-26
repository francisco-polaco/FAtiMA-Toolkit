namespace MCTS.DST.Objects.Fire
{
    public abstract class Fuel
    {
        public int FuelTime;

        protected Fuel(int time)
        {
            FuelTime = time;
        }
    }
}